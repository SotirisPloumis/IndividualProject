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
	public enum CourseAttributes
	{
		Title = 1,
		Stream,
		Type,
		StartDate,
		EndDate
	}

	public class CourseManager : IManager
	{
		public void Create()
		{
			bool exit;

			while (true)
			{
				ConsoleUI.Clear();

				string baseMessage = "type course's information or type 0 to exit \n";


				exit = ConsoleUI.GetString(out string title, $"{baseMessage}Course's title: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetString(out string stream, $"{baseMessage}Course's stream: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetInt(out int typeInput, $"{baseMessage}Course's type (1 = Full time, 2 = Part time): ");
				if (exit)
				{
					return;
				}
				if (typeInput != 1 && typeInput != 2)
				{
					ConsoleUI.showLine("course type not accepted");
					ConsoleUI.ReadKey();
					continue;
				}
				string type = typeInput == 1 ? "Full time" : "Part time";

				exit = ConsoleUI.GetDate(out DateTime? startDate, "Course's start date:\n");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetDate(out DateTime? endDate, "Course's end date:\n");
				if (exit)
				{
					return;
				}

				if (endDate <= startDate)
				{
					ConsoleUI.showLine("invalid dates: end date is before start date, IMPOSSIBRU!!");
					ConsoleUI.ReadKey();
					continue;
				}

				int result = DBCourse.CreateCourse(title, stream, type, (DateTime)startDate, (DateTime)endDate, out int id);

				if (result == 0)
				{
					ConsoleUI.showLine("course could NOT be saved");
				}
				else
				{
					ConsoleUI.showLine($"course created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Course> courses = DBCourse.ReadCourses();
			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);
				}
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			bool exit;
			

			ICollection<Course> courses = DBCourse.ReadCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("no courses yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showLine("select course to update, type 0 to exit");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);
				}
			}
			

			exit = ConsoleUI.GetInt(out int CourseID, "give course id: ");
			if (exit)
			{
				return;
			}

			ConsoleUI.Clear();

			Course course;
			try
			{
				course = courses.Where(c => c.Id == CourseID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO COURSE FOUND WITH ID: {CourseID}");
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.showLine($"you selected to edit course: {course.Title}");

			ConsoleUI.showLine($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showLine("1. Title");
			ConsoleUI.showLine("2. Stream");
			ConsoleUI.showLine("3. Type");
			ConsoleUI.showLine("4. Date of start");
			ConsoleUI.showLine("5. Date of end");

			exit = ConsoleUI.GetInt(out int choice);
			if (exit)
			{
				return;
			}
			ConsoleUI.Clear();

			CourseAttributes attribute = (CourseAttributes)choice;
			string newInput = "";
			DateTime? newStartDate;
			DateTime? newEndDate;

			int result = 0;

			switch (attribute)
			{
				case CourseAttributes.Title:
					exit = ConsoleUI.GetString(out newInput, "enter new title: ");
					if (exit)
					{
						return;
					}

					result = DBCourse.UpdateCourse(CourseID, attribute, newInput);

					break;
				case CourseAttributes.Stream:
					exit = ConsoleUI.GetString(out newInput, "enter new stream: ");
					if (exit)
					{
						return;
					}

					result = DBCourse.UpdateCourse(CourseID, attribute, newInput);

					break;
				case CourseAttributes.Type:
					exit = ConsoleUI.GetString(out newInput, "enter new type: ");
					if (exit)
					{
						return;
					}

					result = DBCourse.UpdateCourse(CourseID, attribute, newInput);

					break;
				case CourseAttributes.StartDate:
					exit = ConsoleUI.GetDate(out newStartDate, "enter new start date: ");
					if (exit)
					{
						return;
					}

					result = DBCourse.UpdateCourse(CourseID, attribute, newStartDate);

					break;
				case CourseAttributes.EndDate:
					exit = ConsoleUI.GetDate(out newEndDate, "enter new end date: ");
					if (exit)
					{
						return;
					}

					result = DBCourse.UpdateCourse(CourseID, attribute, newEndDate);

					break;
				default:
					break;
			}

			if (result == 0)
			{
				ConsoleUI.showLine("course update failed");
			}
			else
			{
				ConsoleUI.showLine("course updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			

			ICollection<Course> courses = DBCourse.ReadCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("no courses yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showLine("select course to delete, type 0 to exit");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);
				}
			}
			

			exit = ConsoleUI.GetInt(out int CourseID, "give course id: ");
			if (exit)
			{
				return;
			}

			Course course;
			try
			{
				course = courses.Where(c => c.Id == CourseID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO COURSE FOUND WITH ID: {CourseID}");
				ConsoleUI.ReadKey();
				return;
			}

			bool confirmed = ConsoleUI.GetConfirmation($"are you sure you want to delete course {course.Title}? [y/n]: ");

			int result = 0;
			if (confirmed)
			{
				result = DBCourse.DeleteCourse(CourseID);
				if (result == 0)
				{
					ConsoleUI.showLine("delete failed");
				}
				else
				{
					ConsoleUI.showLine("course deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showLine($"select action for courses");
			ConsoleUI.showLine("1. Create");
			ConsoleUI.showLine("2. Read");
			ConsoleUI.showLine("3. Update");
			ConsoleUI.showLine("4. Delete");
			ConsoleUI.showLine("0. Exit");
		}
	}
}
