using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;

namespace IndividualProject.DB
{
	public static class DBAssignmentPerStudent
	{
		public static int CreateAssignmentPerNewStudent(int studentID, int courseID, int assignmentID)
		{
			StudentAssignment sa = new StudentAssignment()
			{
				StudentId = studentID,
				CourseId = courseID,
				AssignmentId = assignmentID,
				oralMark = -1,
				totalMark = -1,
				submitted = false,
				submissionDate = new DateTime(1800,1,1)
			};
			using(SchoolContext sc = new SchoolContext())
			{
				sc.StudentAssignments.Add(sa);
				return sc.SaveChanges();
			}

		}

		public static ICollection<StudentAssignment> Read()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<StudentAssignment> saList = sc.StudentAssignments.ToList();
				foreach(StudentAssignment item in saList)
				{
					item.Student = item.Student;
					item.Assignment = item.Assignment;
					item.Course = item.Course;
				}
				return saList;
			}
		}

		public static int MarkAssignment(int studentid, int assignmentid, int courseid, int oralMark, int totalMark)
		{

			StudentAssignment sa;
			using (SchoolContext sc = new SchoolContext())
			{
				try
				{
					sa = sc.StudentAssignments.Find(studentid, assignmentid, courseid);
					if (sa == null)
					{
						return 0;
					}
					sa.oralMark = oralMark;
					sa.totalMark = totalMark;

					int result = sc.SaveChanges();

					if (result == 0)
					{
						return 0;
					}
					return result;
				}
				catch (Exception)
				{
					return 0;
				}
				
			}
			
		}

		public static int DeleteAssignmentPerStudent(int studentID, int assignmentID, int courseID)
		{
			StudentAssignment saItem;

			using (SchoolContext sc = new SchoolContext())
			{
				try
				{
					saItem = sc.StudentAssignments.Find(studentID, assignmentID, courseID);
					sc.StudentAssignments.Remove(saItem);
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
