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
	public class TrainerPerCourseManager : IManager
	{
		public void Create()
		{
			bool exit;

			ConsoleUI.showLine("Trainers");
			ICollection<Trainer> trainers = DBTrainersPerCourse.ReadTrainersWithCourses();

			if (trainers.Count() == 0)
			{
				ConsoleUI.showLine("no trainers yet");
			}
			else
			{
				foreach (Trainer t in trainers)
				{
					ConsoleUI.showLine(t);
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("Courses");
			ICollection<Course> courses = DBTrainersPerCourse.ReadCoursesWithTrainers();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("no courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("press 0 anytime to exit");

			exit = ConsoleUI.GetInt(out int selectedTrainer, "give trainer id to connect: ");
			if (exit)
			{
				return;
			}

			Trainer trainer;

			try
			{
				trainer = trainers.Where(t => t.Id == selectedTrainer).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO TRAINER FOUND FOR ID {selectedTrainer}");
				ConsoleUI.ReadKey();
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedCourse, "give course id to connect: ");
			if (exit)
			{
				return;
			}

			Course course;

			try
			{
				course = courses.Where(c => c.Id == selectedCourse).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasTrainer = course.Trainers.Where(t => t.Id == selectedTrainer).Count() > 0;

			if (courseHasTrainer)
			{
				ConsoleUI.showLine("this course already connects to this trainer");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool trainerHasCourse = trainer.Courses.Where(c => c.Id == selectedCourse).Count() > 0;

			if (trainerHasCourse)
			{
				ConsoleUI.showLine("this trainer already connects to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBTrainersPerCourse.CreateTrainerPerCourse(selectedTrainer, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.showLine("connection could NOT be made");
			}
			else
			{
				ConsoleUI.showLine("connection saved");
			}
			ConsoleUI.ReadKey();
		}

		public void Read()
		{
			ICollection<Course> courses = DBTrainersPerCourse.ReadCoursesWithTrainers();
			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				ConsoleUI.showLine("List of courses with trainers\n");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);

					ICollection<Trainer> CourseTrainers = c.Trainers.ToList();
					if (CourseTrainers.Count() == 0)
					{
						ConsoleUI.showLine("->this course has not trainers\n");
					}
					else
					{

						foreach (Trainer t in CourseTrainers)
						{
							ConsoleUI.showMessage("->");
							ConsoleUI.showLine(t);
						}
						ConsoleUI.changeLine();
					}
				}
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			ConsoleUI.showLine("Update is not available for this connection, please choose create or delete instead");
		}

		public void Delete()
		{
			bool exit;

			ICollection<Course> courses = DBTrainersPerCourse.ReadCoursesWithTrainers();
			ICollection<Trainer> trainers = DBTrainersPerCourse.ReadTrainersWithCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				ConsoleUI.showLine("List of courses with trainers\n");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);

					ICollection<Trainer> CourseTrainers = c.Trainers.ToList();
					if (CourseTrainers.Count() == 0)
					{
						ConsoleUI.showLine("->this course has not trainers\n");
					}
					else
					{

						foreach (Trainer t in CourseTrainers)
						{
							ConsoleUI.showMessage("->");
							ConsoleUI.showLine(t);
						}
						ConsoleUI.changeLine();
					}
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("press 0 anytime to exit");

			exit = ConsoleUI.GetInt(out int selectedTrainer, "give trainer id to disconnect: ");
			if (exit)
			{
				return;
			}

			Trainer trainer;

			try
			{
				trainer = trainers.Where(t => t.Id == selectedTrainer).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO TRAINER FOUND FOR ID {selectedTrainer}");
				ConsoleUI.ReadKey();
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedCourse, "give course id to disconnect: ");
			if (exit)
			{
				return;
			}

			Course course;

			try
			{
				course = courses.Where(c => c.Id == selectedCourse).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}
			
			bool courseHasNoTrainer = course.Trainers.Where(t => t.Id == selectedTrainer).Count() == 0;

			if (courseHasNoTrainer)
			{
				ConsoleUI.showLine("this course doesn't connect to this trainer");
				//ConsoleUI.ReadKey();
				//return;
			}
			
			bool trainerHasNoCourse = trainer.Courses.Where(c => c.Id == selectedCourse).Count() == 0;

			if (trainerHasNoCourse)
			{
				ConsoleUI.showLine("this trainer doesn't connect to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBTrainersPerCourse.DeleteTrainerPerCourse(selectedTrainer, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.showLine("connection could NOT be removed");
			}
			else
			{
				ConsoleUI.showLine("connection removed");
			}
			ConsoleUI.ReadKey();
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showLine($"select action for trainers per course");
			ConsoleUI.showLine("1. Create");
			ConsoleUI.showLine("2. Read");
			ConsoleUI.showLine("3. Update");
			ConsoleUI.showLine("4. Delete");
			ConsoleUI.showLine("0. Exit");
		}
	}
}
