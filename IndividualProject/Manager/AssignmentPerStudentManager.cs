using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.DB;
using IndividualProject.Model;
using IndividualProject.UI;

namespace IndividualProject.Manager
{
	public class AssignmentPerStudentManager
	{
		public void CreateFromNewStudent(int studentID, int courseID)
		{
			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();

			Course course = courses.Where(c => c.Id == courseID).First();

			ICollection<Assignment> courseAssigments = course.Assignments;

			int result = 0;
			foreach (Assignment a in courseAssigments)
			{
				result = DBAssignmentPerStudent.CreateAssignmentPerNewStudent(studentID, courseID, a.Id);
				if (result == 0)
				{
					ConsoleUI.showLine("an error occured");
					ConsoleUI.ReadKey();
					return;
				}
				else
				{
					ConsoleUI.showLine($"assignment id: {a.Id} saved for student id: {studentID} and course id {courseID}");
				}
			}
			ConsoleUI.ReadKey();
		}

		public void CreateFromNewAssignment(int assignmentID, int courseID)
		{
			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();

			Course course = courses.Where(c => c.Id == courseID).First();

			ICollection<Student> CourseStudents = course.Students;

			int result = 0;
			foreach(Student s in CourseStudents)
			{
				result = DBAssignmentPerStudent.CreateAssignmentPerNewStudent(s.Id, courseID, assignmentID);
				if (result == 0)
				{
					ConsoleUI.showLine("an error occured");
					ConsoleUI.ReadKey();
					return;
				}
				else
				{
					ConsoleUI.showLine($"assignment id: {assignmentID} saved for student id: {s.Id} and course id {courseID}");
				}
			}
			ConsoleUI.ReadKey();
		}

		public void ReadAssignmentsPerCoursePerStudent()
		{
			ICollection<StudentAssignment> saList = DBAssignmentPerStudent.Read();

			if (saList.Count() == 0)
			{
				ConsoleUI.showLine("nothing to show");
			}
			else
			{
				foreach (StudentAssignment item in saList)
				{
					Student s = item.Student;
					Assignment a = item.Assignment;
					Course c = item.Course;
					ConsoleUI.showLine($"student {s.FirstName} {s.LastName} has assignment {a.Title} by course {c.Title}c");
				}
			}
			

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void MarkAssignmentsPerCoursePerStudent()
		{
			ICollection<StudentAssignment> saList = DBAssignmentPerStudent.Read();

			if (saList.Count() == 0)
			{
				ConsoleUI.showLine("nothing to show");
			}
			else
			{
				foreach (StudentAssignment item in saList)
				{
					Student s = item.Student;
					Assignment a = item.Assignment;
					Course c = item.Course;
					ConsoleUI.showLine($"student {s.Id} {s.FirstName} {s.LastName} has assignment {a.Id} {a.Title} by course {c.Id} {c.Title} ({item.oralMark}/{item.totalMark})");
				}
			}

			ConsoleUI.showLine("select student, assignment and course to mark or type 0 to exit");
			bool exit;

			exit = ConsoleUI.GetInt(out int selectedStudent, "give student id: ");
			if (exit)
			{
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedAssignment, "give assignment id: ");
			if (exit)
			{
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedCourse, "give course id: ");
			if (exit)
			{
				return;
			}

			exit = ConsoleUI.GetInt(out int oralMark, "give new oral mark: ");
			if (exit)
			{
				return;
			}

			exit = ConsoleUI.GetInt(out int totalMark, "give new total mark: ");
			if (exit)
			{
				return;
			}

			int result = DBAssignmentPerStudent.MarkAssignment(selectedStudent, 
																selectedAssignment, 
																selectedCourse,
																oralMark,
																totalMark);
			if (result == 0)
			{
				ConsoleUI.showLine("error marking assignment");
			}
			else
			{
				ConsoleUI.showLine("assignment marked successfully");
			}
			ConsoleUI.ReadKey();
			ConsoleUI.Clear();

		}

		public void DeleteByAssignment(int assignmentID, int courseID)
		{
			ICollection<StudentAssignment> sa = DBAssignmentPerStudent.Read();

			List<StudentAssignment> toDelete = sa.Where(row => row.AssignmentId == assignmentID && row.CourseId == courseID).ToList();

			int result = 0;
			foreach(StudentAssignment item in toDelete)
			{
				result = DBAssignmentPerStudent.DeleteAssignmentPerStudent(item.StudentId, assignmentID, courseID);
				if (result == 0)
				{
					ConsoleUI.showLine("error occured");
					return;
				}
				else
				{
					ConsoleUI.showLine($"assignment {assignmentID} for {item.StudentId} deleted");
				}
			}
			
		}

		public void DeleteByStudent(int studentID, int courseID)
		{
			ICollection<StudentAssignment> sa = DBAssignmentPerStudent.Read();

			List<StudentAssignment> toDelete = sa.Where(row => row.StudentId == studentID && row.CourseId == courseID).ToList();

			int result = 0;
			foreach(StudentAssignment item in toDelete)
			{
				result = DBAssignmentPerStudent.DeleteAssignmentPerStudent(studentID, item.AssignmentId, courseID);
				if (result == 0)
				{
					ConsoleUI.showLine("error occured");
					return;
				}
				else
				{
					ConsoleUI.showLine($"assignment {item.AssignmentId} for student {studentID} deleted");
				}
			}
			
		}
	}
}
