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
		Fname = 3,
		Lname = 4,
		DOB = 5,
		Fees = 6
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

				exit = ConsoleUI.GetString(out string username, $"{baseMessage}Student's username: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetString(out string password, $"{baseMessage}Student's password: ");
				if (exit)
				{
					return;
				}

				

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

				int studentID;

				try
				{
					int userSaved = DBUser.CreateUser(username, password, "student", out studentID);
					if (userSaved == 0)
					{
						throw new Exception("user NOT saved");
					}
				}
				catch (Exception e)
				{
					ConsoleUI.showLine(e.Message);
					ConsoleUI.ReadKey();
					return;
				}

				ConsoleUI.showLine($"user {username} saved");

				try
				{
					int studentSaved = DBStudent.CreateStudent(fname, lname, (DateTime)dob, fees, studentID);
					if (studentSaved == 0)
					{
						throw new Exception("student NOT saved");
					}
				}
				catch (Exception e)
				{
					ConsoleUI.showLine(e.InnerException.Message);
					ConsoleUI.ReadKey();
					return;
				}

				ConsoleUI.showLine("student created");

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Student> students = DBStudent.ReadStudents();
			if (students.Count() == 0)
			{
				ConsoleUI.showLine("No students yet");
			}
			else
			{
				foreach (Student s in students)
				{
					ConsoleUI.showLine(s);
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
				ConsoleUI.showLine("No students yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showLine("select student to update, type 0 to exit");
				foreach (Student s in students)
				{
					ConsoleUI.showLine(s);
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
				ConsoleUI.showLine($"NO STUDENT FOUND WITH ID: {StudentID}");
				ConsoleUI.ReadKey();
				return;
			}
			
			ConsoleUI.showLine($"you selected to edit student: {student.FirstName} {student.LastName}");

			ConsoleUI.showLine($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showLine("1. Username");
			ConsoleUI.showLine("2. Password");
			ConsoleUI.showLine("3. First name");
			ConsoleUI.showLine("4. Last name");
			ConsoleUI.showLine("5. Date of birth");
			ConsoleUI.showLine("6. Fees");
			

			exit = ConsoleUI.GetInt(out int choice);
			if (exit)
			{
				return;
			}
			ConsoleUI.Clear();

			Object attribute;
			if (choice == 1 || choice == 2)
			{
				attribute = (UserAttributes)choice;
			}
			else
			{
				attribute = (StudentAttributes)choice;
			}

			string newInput = "";
			DateTime? newDate;
			decimal newFees;

			int result = 0;

			try
			{
				switch (attribute)
				{
					case StudentAttributes.Fname:
						exit = ConsoleUI.GetString(out newInput, "enter new first name: ");
						if (exit)
						{
							return;
						}

						result = DBStudent.UpdateStudent(StudentID, attribute, newInput);

						if (result == 0)
						{
							throw new Exception("student NOT updated");
						}

						break;
					case StudentAttributes.Lname:
						exit = ConsoleUI.GetString(out newInput, "enter new last name: ");
						if (exit)
						{
							return;
						}

						result = DBStudent.UpdateStudent(StudentID, attribute, newInput);

						if (result == 0)
						{
							throw new Exception("student NOT updated");
						}

						break;
					case StudentAttributes.DOB:
						exit = ConsoleUI.GetDate(out newDate, "enter new date of birth: ");
						if (exit)
						{
							return;
						}

						result = DBStudent.UpdateStudent(StudentID, attribute, newDate);

						if (result == 0)
						{
							throw new Exception("student NOT updated");
						}

						break;
					case StudentAttributes.Fees:
						exit = ConsoleUI.GetDecimal(out newFees, "enter new fees: ");
						if (exit)
						{
							return;
						}

						result = DBStudent.UpdateStudent(StudentID, attribute, newFees);

						if (result == 0)
						{
							throw new Exception("student NOT updated");
						}

						break;
					case UserAttributes.Username:
						exit = ConsoleUI.GetString(out newInput, "enter new username: ");
						if (exit)
						{
							return;
						}

						result = DBUser.UpdateUser(StudentID, attribute, newInput);

						if (result == 0)
						{
							throw new Exception("user NOT updated");
						}

						break;

					case UserAttributes.Password:
						exit = ConsoleUI.GetString(out newInput, "enter new password: ");
						if (exit)
						{
							return;
						}

						result = DBUser.UpdateUser(StudentID, attribute, newInput);

						if (result == 0)
						{
							throw new Exception("user NOT updated");
						}

						break;
					default:
						break;
				}
			}
			catch (Exception e)
			{
				ConsoleUI.showLine(e.Message);
			}

			ConsoleUI.showLine("student updated successfully");

			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			

			ICollection<Student> students = DBStudent.ReadStudents();
			if (students.Count() == 0)
			{
				ConsoleUI.showLine("No students yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showLine("select student to delete, type 0 to exit");
				foreach (Student s in students)
				{
					ConsoleUI.showLine(s);
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
				ConsoleUI.showLine($"NO STUDENT FOUND WITH ID: {StudentID}");
				ConsoleUI.ReadKey();
				return;
			}

			bool confirmed = ConsoleUI.GetConfirmation($"are you sure you want to delete student {student.FirstName} {student.LastName}? [y/n]: ");

			int result = 0;
			if (confirmed)
			{
				try
				{
					result = DBStudent.DeleteStudent(StudentID);
					if (result == 0)
					{
						throw new Exception("could NOT delete student");
					}
				}
				catch (Exception e)
				{
					ConsoleUI.showLine(e.Message);
					ConsoleUI.ReadKey();
					return;
				}

				try
				{
					result = DBUser.DeleteUser(StudentID);
					if (result == 0)
					{
						throw new Exception("could NOT delete user");
					}
				}
				catch (Exception e)
				{
					ConsoleUI.showLine(e.Message);
					ConsoleUI.ReadKey();
					return;
				}

				ConsoleUI.showLine("student deleted successfully");

				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showLine($"select action for students");
			ConsoleUI.showLine("1. Create");
			ConsoleUI.showLine("2. Read");
			ConsoleUI.showLine("3. Update");
			ConsoleUI.showLine("4. Delete");
			ConsoleUI.showLine("0. Exit");
		}
	}
}
