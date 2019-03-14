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

				exit = ConsoleUI.GetString(out string type, $"{baseMessage}Course's type: ");
				if (exit)
				{
					return;
				}

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

				int result = DBCourse.CreateCourse(title, stream, type, (DateTime)startDate, (DateTime)endDate, out int id);

				if (result == 0)
				{
					ConsoleUI.showMessage("course could NOT be saved");
				}
				else
				{
					ConsoleUI.showMessage($"course created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Course> courses = DBCourse.ReadCourses();
			if (courses.Count() == 0)
			{
				ConsoleUI.showMessage("No courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.showMessage(c);
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
				ConsoleUI.showMessage("no courses yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showMessage("select course to update, type 0 to exit");
				foreach (Course c in courses)
				{
					ConsoleUI.showMessage(c);
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
				ConsoleUI.showMessage($"NO COURSE FOUND WITH ID: {CourseID}");
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.showMessage($"you selected to edit course: {course.Title}");

			ConsoleUI.showMessage($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showMessage("1. Title");
			ConsoleUI.showMessage("2. Stream");
			ConsoleUI.showMessage("3. Type");
			ConsoleUI.showMessage("4. Date of start");
			ConsoleUI.showMessage("5. Date of end");

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
				ConsoleUI.showMessage("course update failed");
			}
			else
			{
				ConsoleUI.showMessage("course updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			

			ICollection<Course> courses = DBCourse.ReadCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.showMessage("no courses yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showMessage("select course to delete, type 0 to exit");
				foreach (Course c in courses)
				{
					ConsoleUI.showMessage(c);
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
				ConsoleUI.showMessage($"NO COURSE FOUND WITH ID: {CourseID}");
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
					ConsoleUI.showMessage("delete failed");
				}
				else
				{
					ConsoleUI.showMessage("course deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showMessage($"select action for courses");
			ConsoleUI.showMessage("1. Create");
			ConsoleUI.showMessage("2. Read");
			ConsoleUI.showMessage("3. Update");
			ConsoleUI.showMessage("4. Delete");
			ConsoleUI.showMessage("0. Exit");
		}
	}
}
