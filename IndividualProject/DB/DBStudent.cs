using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;
using IndividualProject.Manager;

namespace IndividualProject.DB
{
	public static class DBStudent
	{
		public static int CreateStudent(string fname, string lname, DateTime dob, decimal fees, out int studentID)
		{
			Student newStudent = new Student()
			{
				FirstName = fname,
				LastName = lname,
				DateOfBirth = dob,
				Fees = fees
			};

			using (SchoolContext context = new SchoolContext())
			{
				context.Students.Add(newStudent);
				int created = context.SaveChanges();
				studentID = newStudent.Id;
				return created;
			}
		}

		public static ICollection<Student> ReadStudents()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				return sc.Students.ToList();
			}
		}

		public static int UpdateStudent<T>(int id, StudentAttributes attribute, T newValue)
		{
			using(SchoolContext sc = new SchoolContext())
			{
				Student student;

				try
				{
					student = sc.Students.Find(id);
					switch (attribute)
					{
						case StudentAttributes.Fname:
							student.FirstName = newValue.ToString();
							break;
						case StudentAttributes.Lname:
							student.LastName = newValue.ToString();
							break;
						case StudentAttributes.DOB:
							student.DateOfBirth = (DateTime)(object)newValue;
							break;
						case StudentAttributes.Fees:
							student.Fees = (Decimal)(object)newValue;
							break;
						default:
							break;
					}
					return sc.SaveChanges();
				}
				catch (Exception)
				{
					return 0;
				}
		
				
			}
		}

		public static int DeleteStudent(int studentID)
		{
			using(SchoolContext sc = new SchoolContext())
			{
				Student s;

				try
				{
					s = sc.Students.Find(studentID);
					sc.Students.Remove(s);
					return sc.SaveChanges();
				}
				catch (Exception)
				{
					return 0;
				}
				
				
			}
		}
	}
}
