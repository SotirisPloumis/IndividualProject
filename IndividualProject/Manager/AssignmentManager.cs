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
	public enum AssignmentAttributes
	{
		Title = 1,
		Description,
		SubmissionDate,
		OralMark,
		TotalMark
	}

	public class AssignmentManager : IManager
	{
		public void Create()
		{
			bool exit;

			while (true)
			{
				ConsoleUI.Clear();

				string baseMessage = "type assignment's information or type 0 to exit \n";


				exit = ConsoleUI.GetString(out string title, $"{baseMessage}Assignment's title: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetString(out string description, $"{baseMessage}Assignment's description: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetDate(out DateTime? submissionDate, "Assignment's date of submsission:\n");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetDecimal(out decimal oralMark, "Assignment's oral mark: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetDecimal(out decimal totalMark, "Assignment's total mark: ");
				if (exit)
				{
					return;
				}

				int result = DBAssignment.CreateAssignment(title, description, (DateTime)submissionDate, oralMark, totalMark, out int id);

				if (result == 0)
				{
					ConsoleUI.showMessage("assignment could NOT be saved");
				}
				else
				{
					ConsoleUI.showMessage($"assignment created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Assignment> assignments = DBAssignment.ReadAssignments();
			if (assignments.Count() == 0)
			{
				ConsoleUI.showMessage("No assignments yet");
			}
			else
			{
				foreach (Assignment a in assignments)
				{
					ConsoleUI.showMessage(a);
				}
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			bool exit;

			ICollection<Assignment> assignments = DBAssignment.ReadAssignments();
			if (assignments.Count() == 0)
			{
				ConsoleUI.showMessage("No assignments yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showMessage("select assignment to update, type 0 to exit");
				foreach (Assignment s in assignments)
				{
					ConsoleUI.showMessage(s);
				}
			}


			exit = ConsoleUI.GetInt(out int AssignmentID, "give assignment id: ");
			if (exit)
			{
				return;
			}

			ConsoleUI.Clear();

			Assignment assignment;
			try
			{
				assignment = assignments.Where(a => a.Id == AssignmentID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showMessage($"NO ASSIGNMENT FOUND WITH ID: {AssignmentID}");
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.showMessage($"you selected to edit assignment: {assignment.Title}");

			ConsoleUI.showMessage($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showMessage("1. Title");
			ConsoleUI.showMessage("2. Description");
			ConsoleUI.showMessage("3. Date of submission");
			ConsoleUI.showMessage("4. Oral Mark");
			ConsoleUI.showMessage("5. Total Mark");

			exit = ConsoleUI.GetInt(out int choice);
			if (exit)
			{
				return;
			}
			ConsoleUI.Clear();

			AssignmentAttributes attribute = (AssignmentAttributes)choice;
			string newInput = "";
			DateTime? newDate;
			decimal newMark;

			int result = 0;

			switch (attribute)
			{
				case AssignmentAttributes.Title:
					exit = ConsoleUI.GetString(out newInput, "enter new title: ");
					if (exit)
					{
						return;
					}

					result = DBAssignment.UpdateAssignment(AssignmentID, attribute, newInput);

					break;
				case AssignmentAttributes.Description:
					exit = ConsoleUI.GetString(out newInput, "enter new description: ");
					if (exit)
					{
						return;
					}

					result = DBAssignment.UpdateAssignment(AssignmentID, attribute, newInput);

					break;
				case AssignmentAttributes.SubmissionDate:
					exit = ConsoleUI.GetDate(out newDate, "enter new date of submission: ");
					if (exit)
					{
						return;
					}

					result = DBAssignment.UpdateAssignment(AssignmentID, attribute, newDate);

					break;
				case AssignmentAttributes.OralMark:
					exit = ConsoleUI.GetDecimal(out newMark, "enter new oral mark: ");
					if (exit)
					{
						return;
					}

					result = DBAssignment.UpdateAssignment(AssignmentID, attribute, newMark);

					break;
				case AssignmentAttributes.TotalMark:
					exit = ConsoleUI.GetDecimal(out newMark, "enter new total mark: ");
					if (exit)
					{
						return;
					}

					result = DBAssignment.UpdateAssignment(AssignmentID, attribute, newMark);

					break;
				default:
					break;
			}

			if (result == 0)
			{
				ConsoleUI.showMessage("assignment update failed");
			}
			else
			{
				ConsoleUI.showMessage("assignment updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;


			ICollection<Assignment> assignments = DBAssignment.ReadAssignments();
			if (assignments.Count() == 0)
			{
				ConsoleUI.showMessage("No assignments yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showMessage("select assignment to delete, type 0 to exit");
				foreach (Assignment a in assignments)
				{
					ConsoleUI.showMessage(a);
				}
			}


			exit = ConsoleUI.GetInt(out int AssignmentID, "give assignment id: ");
			if (exit)
			{
				return;
			}

			Assignment assignment;
			try
			{
				assignment = assignments.Where(a => a.Id == AssignmentID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showMessage($"NO ASSIGNMENT FOUND WITH ID: {AssignmentID}");
				ConsoleUI.ReadKey();
				return;
			}

			bool confirmed = ConsoleUI.GetConfirmation($"are you sure you want to delete assignment {assignment.Title}? [y/n]: ");

			int result = 0;
			if (confirmed)
			{
				result = DBAssignment.DeleteAssignment(AssignmentID);
				if (result == 0)
				{
					ConsoleUI.showMessage("delete failed");
				}
				else
				{
					ConsoleUI.showMessage("assignment deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showMessage($"select action for assignments");
			ConsoleUI.showMessage("1. Create");
			ConsoleUI.showMessage("2. Read");
			ConsoleUI.showMessage("3. Update");
			ConsoleUI.showMessage("4. Delete");
			ConsoleUI.showMessage("0. Exit");
		}
	}
}
