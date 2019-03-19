using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Manager;

namespace IndividualProject
{
	enum HeadMasterOptions
	{
		crudCourses=1,
		crudStudents = 2,
		crudAssignments,
		crudTrainers,
		crudStudentsPerCourse,
		crudTrainersPerCourse,
		crudAssignmentsPerCourse,
		crudSchedulePerCourse,
		logout
	}

	enum TrainerOptions
	{
		viewCourses = 1,
		viewStudentsPerCourse = 2,
		viewAssignmentsPerStudentPerCourse = 3,
		markAssignmentsPerStudentPerCourse = 4,
		logout = 9
	}

	enum StudentOptions
	{
		viewSchedule = 1,
		viewDueDates = 2,
		submitAssignment = 3,
		logout = 9
	}

	enum CRUDOptions
	{
		create = 1,
		read,
		update,
		delete
	}

	public static class Menu
	{
		public static void Start()
		{
			bool emptyDB = AutoManager.IsEmptyDB();
			if (emptyDB)
			{
				Console.WriteLine("DB is empty, do you want to auto generate? [y/n]");
				string autoGenerate;

				while (true)
				{
					autoGenerate = Console.ReadLine();
					if (autoGenerate.Equals("y"))
					{
						AutoManager.AutoGenerate();
						break;
					}
					else if(autoGenerate.Equals("n"))
					{
						break;
					}
					else
					{
						continue;
					}
				}

				
			}

			bool headMasterExists = AccountManager.HeadMasterExists();

			if (!headMasterExists)
			{
				Console.WriteLine("No head master is present in the database");
				AccountManager.Register();
			}

			bool exit = false;
			bool loggedIn = false;

			do
			{
				exit = AccountManager.Login(out loggedIn, out string role, out int id);

				if (exit)
				{
					return;
				}

				if (loggedIn)
				{
					Console.Clear();
					switch (role)
					{
						case "headmaster":
							loggedIn = SelectHeadMasterOption();
							break;
						case "trainer":
							loggedIn = SelectTrainerOption(id);
							break;
						case "student":
							loggedIn = SelectStudentOption(id);
							break;
						default:
							Console.WriteLine("i am other");
							break;
					}
					
				}
				else
				{
					Console.WriteLine("wrong username or password");
					Console.ReadKey();
				}
			} while (!loggedIn);
			
		}

		private static bool SelectHeadMasterOption()
		{
			while (true)
			{
				Console.WriteLine("1. CRUD on courses");
				Console.WriteLine("2. CRUD on students");
				Console.WriteLine("3. CRUD on assignments");
				Console.WriteLine("4. CRUD on trainers");
				Console.WriteLine("5. CRUD on students per course");
				Console.WriteLine("6. CRUD on trainers per course");
				Console.WriteLine("7. CRUD on assignments per course");
				Console.WriteLine("8. CRUD on schedule per course");
				Console.WriteLine("9. Logout");
				Console.WriteLine("0. Exit");

				string input = Console.ReadLine();
				Console.Clear();

				bool goodInput = Int32.TryParse(input, out int choice);
				if (!goodInput)
				{
					continue;
				}

				if (choice == 0)
				{
					break;
				}

				HeadMasterOptions mainOption = (HeadMasterOptions)choice;

				IManager manager = null;

				switch (mainOption)
				{
					case HeadMasterOptions.crudCourses:
						manager = new CourseManager();
						break;
					case HeadMasterOptions.crudStudents:
						manager = new StudentManager();
						break;
					case HeadMasterOptions.crudAssignments:
						manager = new AssignmentManager();
						break;
					case HeadMasterOptions.crudTrainers:
						manager = new TrainerManager();
						break;
					case HeadMasterOptions.crudStudentsPerCourse:
						manager = new StudentPerCourseManager();
						break;
					case HeadMasterOptions.crudTrainersPerCourse:
						manager = new TrainerPerCourseManager();
						break;
					case HeadMasterOptions.crudAssignmentsPerCourse:
						manager = new AssignmentPerCourseManager();
						break;
					case HeadMasterOptions.crudSchedulePerCourse:
						Console.WriteLine("CRUD ops for 'schedule per course' belongs to CRUD ops for course (start and end date)");
						break;
					case HeadMasterOptions.logout:
						Console.WriteLine("logging out");
						Console.ReadKey();
						Console.Clear();
						return false;
					default:
						break;
				}

				if (manager != null)
				{
					SelectHeadMasterCRUDOption(manager);
				}
				
			}
			return true;
		}

		private static bool SelectTrainerOption(int trainerID)
		{
			while (true)
			{
				TrainerManager tm = new TrainerManager();
				tm.ShowPersonalMessage(trainerID);
				Console.WriteLine();

				Console.WriteLine("1. view my courses");
				Console.WriteLine("2. view students per course");
				Console.WriteLine("3. view assignments per student per course");
				Console.WriteLine("4. mark assignments per student per course");
				Console.WriteLine("9. Logout");
				Console.WriteLine("0. Exit");

				string input = Console.ReadLine();
				Console.Clear();

				bool goodInput = Int32.TryParse(input, out int choice);
				if (!goodInput)
				{
					continue;
				}

				if (choice == 0)
				{
					break;
				}

				TrainerOptions mainOption = (TrainerOptions)choice;

				switch (mainOption)
				{
					case TrainerOptions.viewCourses:
						TrainerPerCourseManager manager = new TrainerPerCourseManager();
						manager.ReadTrainerCourses(trainerID);
						break;
					case TrainerOptions.viewStudentsPerCourse:
						StudentPerCourseManager manager2 = new StudentPerCourseManager();
						manager2.Read();
						break;
					case TrainerOptions.viewAssignmentsPerStudentPerCourse:
						AssignmentPerStudentManager manager3 = new AssignmentPerStudentManager();
						manager3.ReadAssignmentsPerCoursePerStudent();
						break;
					case TrainerOptions.markAssignmentsPerStudentPerCourse:
						AssignmentPerStudentManager manager4 = new AssignmentPerStudentManager();
						manager4.MarkAssignmentsPerCoursePerStudent();
						break;
					case TrainerOptions.logout:
						Console.WriteLine("logging out");
						Console.ReadKey();
						Console.Clear();
						return false;
					default:
						break;
				}

			}
			return true;
		}

		private static bool SelectStudentOption(int studentID)
		{
			while (true)
			{
				StudentManager sm = new StudentManager();
				sm.ShowPersonalMessage(studentID);
				Console.WriteLine();

				Console.WriteLine("1. view my schedule per course");
				Console.WriteLine("2. view due date per assignment");
				Console.WriteLine("3. submit assignment");
				Console.WriteLine("9. Logout");
				Console.WriteLine("0. Exit");

				string input = Console.ReadLine();
				Console.Clear();

				bool goodInput = Int32.TryParse(input, out int choice);
				if (!goodInput)
				{
					continue;
				}

				if (choice == 0)
				{
					break;
				}

				StudentOptions mainOption = (StudentOptions)choice;

				switch (mainOption)
				{
					case StudentOptions.viewSchedule:
						AssignmentPerStudentManager manager = new AssignmentPerStudentManager();
						manager.ReadSchedule(studentID);
						break;
					case StudentOptions.viewDueDates:
						AssignmentPerStudentManager manager2 = new AssignmentPerStudentManager();
						manager2.ReadDueDates(studentID);
						break;
					case StudentOptions.submitAssignment:
						AssignmentPerStudentManager manager3 = new AssignmentPerStudentManager();
						manager3.SubmitAssignmentPerCoursePerStudent(studentID);
						break;
					case StudentOptions.logout:
						Console.WriteLine("logging out");
						Console.ReadKey();
						Console.Clear();
						return false;
					default:
						break;
				}

			}
			return true;
		}

		private static void SelectHeadMasterCRUDOption(IManager manager)
		{
			while (true)
			{
				manager.ShowCRUDMenu();

				string input = Console.ReadLine();
				Console.Clear();

				bool goodInput = Int32.TryParse(input, out int choice);
				if (!goodInput)
				{
					continue;
				}
				if (choice == 0)
				{
					break;
				}

				CRUDOptions CRUDOption = (CRUDOptions)choice;

				switch (CRUDOption)
				{
					case CRUDOptions.create:
						manager.Create();
						break;
					case CRUDOptions.read:
						manager.Read();
						break;
					case CRUDOptions.update:
						manager.Update();
						break;
					case CRUDOptions.delete:
						manager.Delete();
						break;
					default:
						break;	
				}
				Console.Clear();
			}

		}

	}
}
