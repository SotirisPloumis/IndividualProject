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
	public enum StudentAttributes{
		Fname = 1,
		Lname,
		DOB,
		Fees
	}

	public class StudentManager : IManager
	{
		public void Create()
		{
			bool exit;

			while (true)
			{
				ConsoleUI.Clear();

				string baseMessage = "type student's information or type 0 to exit \n";


				exit = ConsoleUI.GetString(out string fname,$"{baseMessage}Student's first name: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetString(out string lname, $"{baseMessage}Student's last name: ");
				if (exit)
				{
					return;
				}
				
				exit = ConsoleUI.GetDate(out DateTime? dob, "Student's date of birth:\n");
				if (exit)
				{
					return;
				}
				
				exit = ConsoleUI.GetDecimal(out decimal fees, "Student's fees: ");
				if (exit)
				{
					return;
				}

				int result = DBStudent.CreateStudent(fname, lname, (DateTime)dob, fees, out int id);

				if (result == 0)
				{
					ConsoleUI.showMessage("student could NOT be saved");
				}
				else
				{
					ConsoleUI.showMessage($"student created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Student> students = DBStudent.ReadStudents();
			if (students.Count() == 0)
			{
				ConsoleUI.showMessage("No students yet");
			}
			else
			{
				foreach (Student s in students)
				{
					ConsoleUI.showMessage(s);
				}
			}
			
			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			bool exit;

			ICollection<Student> students = DBStudent.ReadStudents();
			if (students.Count() == 0)
			{
				ConsoleUI.showMessage("No students yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showMessage("select student to update, type 0 to exit");
				foreach (Student s in students)
				{
					ConsoleUI.showMessage(s);
				}
			}
			

			exit = ConsoleUI.GetInt(out int StudentID, "give student id: ");
			if (exit)
			{
				return;
			}

			ConsoleUI.Clear();

			Student student;
			try
			{
				student = students.Where(s => s.Id == StudentID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showMessage($"NO STUDENT FOUND WITH ID: {StudentID}");
				ConsoleUI.ReadKey();
				return;
			}
			
			ConsoleUI.showMessage($"you selected to edit student: {student.FirstName} {student.LastName}");

			ConsoleUI.showMessage($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showMessage("1. First name");
			ConsoleUI.showMessage("2. Last name");
			ConsoleUI.showMessage("3. Date of birth");
			ConsoleUI.showMessage("4. Fees");

			exit = ConsoleUI.GetInt(out int choice);
			if (exit)
			{
				return;
			}
			ConsoleUI.Clear();

			StudentAttributes attribute = (StudentAttributes)choice;
			string newInput = "";
			DateTime? newDate;
			decimal newFees;

			int result = 0;

			switch (attribute)
			{
				case StudentAttributes.Fname:
					exit = ConsoleUI.GetString(out newInput, "enter new first name: ");
					if (exit)
					{
						return;
					}

					result = DBStudent.UpdateStudent(StudentID, attribute, newInput);

					break;
				case StudentAttributes.Lname:
					exit = ConsoleUI.GetString(out newInput, "enter new last name: ");
					if (exit)
					{
						return;
					}

					result = DBStudent.UpdateStudent(StudentID, attribute, newInput);

					break;
				case StudentAttributes.DOB:
					exit = ConsoleUI.GetDate(out newDate, "enter new date of birth: ");
					if (exit)
					{
						return;
					}

					result = DBStudent.UpdateStudent(StudentID, attribute, newDate);

					break;
				case StudentAttributes.Fees:
					exit = ConsoleUI.GetDecimal(out newFees, "enter new fees: ");
					if (exit)
					{
						return;
					}

					result = DBStudent.UpdateStudent(StudentID, attribute, newFees);

					break;
				default:
					break;
			}

			if (result == 0)
			{
				ConsoleUI.showMessage("student update failed");
			}
			else
			{
				ConsoleUI.showMessage("student updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			

			ICollection<Student> students = DBStudent.ReadStudents();
			if (students.Count() == 0)
			{
				ConsoleUI.showMessage("No students yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showMessage("select student to delete, type 0 to exit");
				foreach (Student s in students)
				{
					ConsoleUI.showMessage(s);
				}
			}
			

			exit = ConsoleUI.GetInt(out int StudentID, "give student id: ");
			if (exit)
			{
				return;
			}

			Student student;
			try
			{
				student = students.Where(s => s.Id == StudentID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showMessage($"NO STUDENT FOUND WITH ID: {StudentID}");
				ConsoleUI.ReadKey();
				return;
			}

			bool confirmed = ConsoleUI.GetConfirmation($"are you sure you want to delete student {student.FirstName} {student.LastName}? [y/n]: ");

			int result = 0;
			if (confirmed)
			{
				result = DBStudent.DeleteStudent(StudentID);
				if (result == 0)
				{
					ConsoleUI.showMessage("delete failed");
				}
				else
				{
					ConsoleUI.showMessage("student deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showMessage($"select action for students");
			ConsoleUI.showMessage("1. Create");
			ConsoleUI.showMessage("2. Read");
			ConsoleUI.showMessage("3. Update");
			ConsoleUI.showMessage("4. Delete");
			ConsoleUI.showMessage("0. Exit");
		}
	}
}
