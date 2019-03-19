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

			ConsoleUI.ShowLine("Trainers");
			ICollection<Trainer> trainers = DBTrainersPerCourse.ReadTrainersWithCourses();

			if (trainers.Count() == 0)
			{
				ConsoleUI.ShowLine("no trainers yet");
			}
			else
			{
				foreach (Trainer t in trainers)
				{
					ConsoleUI.ShowLine(t);
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("Courses");
			ICollection<Course> courses = DBTrainersPerCourse.ReadCoursesWithTrainers();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("no courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("press 0 anytime to exit");

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
				ConsoleUI.ShowLine($"NO TRAINER FOUND FOR ID {selectedTrainer}");
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
				ConsoleUI.ShowLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasTrainer = course.Trainers.Where(t => t.Id == selectedTrainer).Count() > 0;

			if (courseHasTrainer)
			{
				ConsoleUI.ShowLine("this course already connects to this trainer");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool trainerHasCourse = trainer.Courses.Where(c => c.Id == selectedCourse).Count() > 0;

			if (trainerHasCourse)
			{
				ConsoleUI.ShowLine("this trainer already connects to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBTrainersPerCourse.CreateTrainerPerCourse(selectedTrainer, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.ShowLine("connection could NOT be made");
			}
			else
			{
				ConsoleUI.ShowLine("connection saved");
			}
			ConsoleUI.ReadKey();
		}

		public void Read()
		{
			ICollection<Course> courses = DBTrainersPerCourse.ReadCoursesWithTrainers();
			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of courses with trainers\n");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);

					ICollection<Trainer> CourseTrainers = c.Trainers.ToList();
					if (CourseTrainers.Count() == 0)
					{
						ConsoleUI.ShowLine("->this course has not trainers\n");
					}
					else
					{

						foreach (Trainer t in CourseTrainers)
						{
							ConsoleUI.ShowMessage("->");
							ConsoleUI.ShowLine(t);
						}
						ConsoleUI.ChangeLine();
					}
				}
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void ReadTrainerCourses(int trainerID)
		{
			ICollection<Trainer> trainers = DBTrainersPerCourse.ReadTrainersWithCourses();
			if (trainers.Count() == 0)
			{
				ConsoleUI.ShowLine("No trainers yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of my courses:\n");

				Trainer me = trainers.Where(t => t.Id == trainerID).First();

				if (me.Courses.Count() == 0)
				{
					ConsoleUI.ShowLine("I am not enrolled in any course");
				}
				else
				{
					foreach (Course c in me.Courses)
					{
						ConsoleUI.ShowMessage("->");
						ConsoleUI.ShowLine(c);
					}
				}

				ConsoleUI.ChangeLine();
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			ConsoleUI.ShowLine("Update is not available for this connection, please choose create or delete instead");
		}

		public void Delete()
		{
			bool exit;

			ICollection<Course> courses = DBTrainersPerCourse.ReadCoursesWithTrainers();
			ICollection<Trainer> trainers = DBTrainersPerCourse.ReadTrainersWithCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of courses with trainers\n");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);

					ICollection<Trainer> CourseTrainers = c.Trainers.ToList();
					if (CourseTrainers.Count() == 0)
					{
						ConsoleUI.ShowLine("->this course has not trainers\n");
					}
					else
					{

						foreach (Trainer t in CourseTrainers)
						{
							ConsoleUI.ShowMessage("->");
							ConsoleUI.ShowLine(t);
						}
						ConsoleUI.ChangeLine();
					}
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("press 0 anytime to exit");

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
				ConsoleUI.ShowLine($"NO TRAINER FOUND FOR ID {selectedTrainer}");
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
				ConsoleUI.ShowLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}
			
			bool courseHasNoTrainer = course.Trainers.Where(t => t.Id == selectedTrainer).Count() == 0;

			if (courseHasNoTrainer)
			{
				ConsoleUI.ShowLine("this course doesn't connect to this trainer");
				//ConsoleUI.ReadKey();
				//return;
			}
			
			bool trainerHasNoCourse = trainer.Courses.Where(c => c.Id == selectedCourse).Count() == 0;

			if (trainerHasNoCourse)
			{
				ConsoleUI.ShowLine("this trainer doesn't connect to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBTrainersPerCourse.DeleteTrainerPerCourse(selectedTrainer, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.ShowLine("connection could NOT be removed");
			}
			else
			{
				ConsoleUI.ShowLine("connection removed");
			}
			ConsoleUI.ReadKey();
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.ShowLine($"select action for trainers per course");
			ConsoleUI.ShowLine("1. Create");
			ConsoleUI.ShowLine("2. Read");
			ConsoleUI.ShowLine("3. Update");
			ConsoleUI.ShowLine("4. Delete");
			ConsoleUI.ShowLine("0. Exit");
		}
	}
}
