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
					ConsoleUI.ShowLine("course type not accepted");
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
					ConsoleUI.ShowLine("invalid dates: end date is before start date, IMPOSSIBRU!!");
					ConsoleUI.ReadKey();
					continue;
				}

				int result = DBCourse.CreateCourse(title, stream, type, (DateTime)startDate, (DateTime)endDate, out int id);

				if (result == 0)
				{
					ConsoleUI.ShowLine("course could NOT be saved");
				}
				else
				{
					ConsoleUI.ShowLine($"course created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Course> courses = DBCourse.ReadCourses();
			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);
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
				ConsoleUI.ShowLine("no courses yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.ShowLine("select course to update, type 0 to exit");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);
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
				ConsoleUI.ShowLine($"NO COURSE FOUND WITH ID: {CourseID}");
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.ShowLine($"you selected to edit course: {course.Title}");

			ConsoleUI.ShowLine($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.ShowLine("1. Title");
			ConsoleUI.ShowLine("2. Stream");
			ConsoleUI.ShowLine("3. Type");
			ConsoleUI.ShowLine("4. Date of start");
			ConsoleUI.ShowLine("5. Date of end");

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
				ConsoleUI.ShowLine("course update failed");
			}
			else
			{
				ConsoleUI.ShowLine("course updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			

			ICollection<Course> courses = DBCourse.ReadCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("no courses yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.ShowLine("select course to delete, type 0 to exit");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);
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
				ConsoleUI.ShowLine($"NO COURSE FOUND WITH ID: {CourseID}");
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
					ConsoleUI.ShowLine("delete failed");
				}
				else
				{
					ConsoleUI.ShowLine("course deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.ShowLine($"select action for courses");
			ConsoleUI.ShowLine("1. Create");
			ConsoleUI.ShowLine("2. Read");
			ConsoleUI.ShowLine("3. Update");
			ConsoleUI.ShowLine("4. Delete");
			ConsoleUI.ShowLine("0. Exit");
		}
	}
}
