using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;
using IndividualProject.UI;
using IndividualProject.DB;

namespace IndividualProject.Manager
{
	public class AutoManager
	{
		public static bool IsEmptyDB()
		{
			int UserSize = DBUser.ReadUsers().Count();
			int AssignmentSize = DBAssignment.ReadAssignments().Count();
			int StudentSize = DBStudent.ReadStudents().Count();
			int CourseSize = DBCourse.ReadCourses().Count();
			int TrainerSize = DBTrainer.ReadTrainers().Count();

			bool basicEntities = UserSize == 0 
									&& AssignmentSize == 0
									&& StudentSize == 0
									&& CourseSize == 0
									&& TrainerSize == 0;


			return basicEntities;
		}

		public static void AutoGenerate() {

			ConsoleUI.ShowLine("Creating headmaster with username: 'hm' and password: '12345'");

			string encryptedPassword = CryptoManager.EncryptPassword("12345", out string encryptedSalt);

			try
			{
				int headMasterSaved = DBUser.CreateUser("hm", encryptedPassword, encryptedSalt, "headmaster", out int id);
				if (headMasterSaved == 0)
				{
					throw new Exception("head master NOT saved");
				}
			}
			catch (Exception e)
			{
				ConsoleUI.ShowLine(e.Message);
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.ShowLine("head master created");

			ConsoleUI.ShowLine("creating students");

			List<int> studentIDs = new List<int>();
			List<int> trainerIDs = new List<int>();
			List<int> assignmentIDs = new List<int>();
			List<int> courseIDs = new List<int>();

			int studentID;
			string studentUsername;
			string plainTextPassword;
			string studentFName;
			string studentLName;

			// 3 students
			for (int i = 0; i < 3; i++)
			{
				studentUsername = "studentUsername" + i;
				plainTextPassword = "studentPass" + i;

				encryptedPassword = CryptoManager.EncryptPassword(plainTextPassword, out encryptedSalt);

				DBUser.CreateUser(studentUsername, encryptedPassword, encryptedSalt, "student", out studentID);

				ConsoleUI.ShowLine($"student user created u:{studentUsername} p:{plainTextPassword}");

				studentFName = "studentFirstName" + i;

				studentLName = "studentLastName" + i;

				DBStudent.CreateStudent(studentFName, studentLName, new DateTime(2000, 1, 1), 20000, studentID);

				ConsoleUI.ShowLine($"student {studentFName} {studentLName} created");

				studentIDs.Add(studentID);

				ConsoleUI.ChangeLine();
			}

			int trainerID;
			string trainerUsername;
			string trainerFName;
			string trainerLName;
			string trainerSubject;

			// 2 trainers
			for (int i = 0; i < 2; i++)
			{
				trainerUsername = "trainerUsername" + i;
				plainTextPassword = "trainerPass" + i;

				encryptedPassword = CryptoManager.EncryptPassword(plainTextPassword, out encryptedSalt);

				DBUser.CreateUser(trainerUsername, encryptedPassword, encryptedSalt, "trainer", out trainerID);

				ConsoleUI.ShowLine($"trainer user created u:{trainerUsername} p:{plainTextPassword}");

				trainerFName = "trainerFirstName" + i;

				trainerLName = "trainerLastName" + i;

				trainerSubject = "trainerSubject" + i;

				DBTrainer.CreateTrainer(trainerFName, trainerLName, trainerSubject, trainerID);

				ConsoleUI.ShowLine($"trainer {trainerFName} {trainerLName} created");

				trainerIDs.Add(trainerID);

				ConsoleUI.ChangeLine();
			}

			string title;
			string description;

			// 2 assignments
			for (int i = 0; i < 2; i++)
			{
				title = "assignmentTitle" + i;
				description = "assignmentDescription" + i;

				DBAssignment.CreateAssignment(title, description, new DateTime(2019, 1, 1), 100, 100, out int assignmentID);

				assignmentIDs.Add(assignmentID);

				ConsoleUI.ShowLine($"assignment {title} with id: {assignmentID} created");

				ConsoleUI.ChangeLine();
			}

			// 3 courses
			for (int i = 0; i < 3; i++)
			{
				title = "courseTitle" + i;

				DBCourse.CreateCourse(title, "C#", "Full time", new DateTime(2019, 1, 1), new DateTime(2019, 2, 2), out int courseID);

				courseIDs.Add(courseID);

				ConsoleUI.ShowLine($"course {title} with id: {courseID} created");

				ConsoleUI.ChangeLine();
			}

			AssignmentPerStudentManager a = new AssignmentPerStudentManager();

			//assignments-courses 
			//all assignments to course 1
			#region
			DBAssignmentsPerCourse.CreateAssignmentPerCourse(assignmentIDs[0], courseIDs[0]);
			a.CreateFromNewAssignment(assignmentIDs[0], courseIDs[0]);

			DBAssignmentsPerCourse.CreateAssignmentPerCourse(assignmentIDs[1], courseIDs[0]);
			a.CreateFromNewAssignment(assignmentIDs[1], courseIDs[0]);

			#endregion

			//first 3 assignments to course 1
			#region
			DBAssignmentsPerCourse.CreateAssignmentPerCourse(assignmentIDs[0], courseIDs[1]);
			a.CreateFromNewAssignment(assignmentIDs[0], courseIDs[1]);

			DBAssignmentsPerCourse.CreateAssignmentPerCourse(assignmentIDs[1], courseIDs[1]);
			a.CreateFromNewAssignment(assignmentIDs[1], courseIDs[1]);

			#endregion

			//student courses
			//all students to course 1
			#region
			DBStudentsPerCourse.CreateStudentPerCourse(studentIDs[0], courseIDs[0]);
			a.CreateFromNewStudent(studentIDs[0], courseIDs[0]);

			DBStudentsPerCourse.CreateStudentPerCourse(studentIDs[1], courseIDs[0]);
			a.CreateFromNewStudent(studentIDs[1], courseIDs[0]);

			DBStudentsPerCourse.CreateStudentPerCourse(studentIDs[2], courseIDs[0]);
			a.CreateFromNewStudent(studentIDs[2], courseIDs[0]);

			#endregion

			// 2 students to course 2
			#region
			DBStudentsPerCourse.CreateStudentPerCourse(studentIDs[0], courseIDs[1]);
			a.CreateFromNewStudent(studentIDs[0], courseIDs[1]);

			DBStudentsPerCourse.CreateStudentPerCourse(studentIDs[1], courseIDs[1]);
			a.CreateFromNewStudent(studentIDs[1], courseIDs[1]);

			#endregion
			// 2 courses to student 1
			#region
			DBStudentsPerCourse.CreateStudentPerCourse(studentIDs[0], courseIDs[2]);
			a.CreateFromNewStudent(studentIDs[0], courseIDs[2]);

			#endregion
			

			//trainer courses

			//all trainers to course 1
			#region
			DBTrainersPerCourse.CreateTrainerPerCourse(trainerIDs[0], courseIDs[0]);
			DBTrainersPerCourse.CreateTrainerPerCourse(trainerIDs[1], courseIDs[0]);
			#endregion

			// 2 trainers to course 2
			#region
			DBTrainersPerCourse.CreateTrainerPerCourse(trainerIDs[0],courseIDs[1]);
			DBTrainersPerCourse.CreateTrainerPerCourse(trainerIDs[1],courseIDs[1]);
			#endregion

			//2 courses to trainer 1
			#region
			DBTrainersPerCourse.CreateTrainerPerCourse(trainerIDs[0], courseIDs[2]);
			#endregion

			ConsoleUI.ShowLine("done");

		}
	}
}
