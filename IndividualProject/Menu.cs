using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Manager;

namespace IndividualProject
{
	enum MainOptions
	{
		crudCourses=1,
		crudStudents = 2,
		crudAssignments,
		crudTrainers,
		crudStudentsPerCourse,
		crudTrainersPerCourse,
		crudAssignmentsPerCourse,
		crudSchedulePerCourse
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
		public static void showMenuAndDo()
		{
			while (true)
			{
				showHeadMasterMenu();
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

				MainOptions mainOption = (MainOptions)choice;

				IManager manager = null;

				switch (mainOption)
				{
					case MainOptions.crudCourses:
						manager = new CourseManager();
						break;
					case MainOptions.crudStudents:
						manager = new StudentManager();
						break;
					case MainOptions.crudAssignments:
						manager = new AssignmentManager();
						break;
					case MainOptions.crudTrainers:
						manager = new TrainerManager();
						break;
					case MainOptions.crudStudentsPerCourse:
						manager = new StudentPerCourseManager();
						break;
					case MainOptions.crudTrainersPerCourse:
						manager = new TrainerPerCourseManager();
						break;
					case MainOptions.crudAssignmentsPerCourse:
						manager = new AssignmentPerCourseManager();
						break;
					case MainOptions.crudSchedulePerCourse:
						Console.WriteLine("CRUD ops for 'schedule per course' belongs to CRUD ops for course (start and end date)");
						break;
					default:
						break;
				}

				if (manager != null)
				{
					showElementCRUD(manager);
				}
			}
		}

		private static void showHeadMasterMenu()
		{
			Console.WriteLine("1. CRUD on courses");
			Console.WriteLine("2. CRUD on students");
			Console.WriteLine("3. CRUD on assignments");
			Console.WriteLine("4. CRUD on trainers");
			Console.WriteLine("5. CRUD on students per course");
			Console.WriteLine("6. CRUD on trainers per course");
			Console.WriteLine("7. CRUD on assignments per course");
			Console.WriteLine("8. CRUD on schedule per course");
			Console.WriteLine("0. Exit");

		}

		private static void showElementCRUD(IManager manager)
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
