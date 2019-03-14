using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;
using IndividualProject.Manager;

namespace IndividualProject.DB
{
	public class DBAssignment
	{
		public static int CreateAssignment(string title, string description, DateTime submissionDate, decimal oralMark, decimal totalMark, out int assignmentID)
		{
			Assignment newAssignment = new Assignment()
			{
				Title = title,
				Description = description,
				SubmissionDate = submissionDate,
				OralMark = oralMark,
				TotalMark = totalMark
			};

			using (SchoolContext context = new SchoolContext())
			{
				context.Assignments.Add(newAssignment);
				int created = context.SaveChanges();
				assignmentID = newAssignment.Id;
				return created;
			}
		}

		public static ICollection<Assignment> ReadAssignments()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				return sc.Assignments.ToList();
			}
		}

		public static int UpdateAssignment<T>(int id, AssignmentAttributes attribute, T newValue)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				Assignment assignment;

				try
				{
					assignment = sc.Assignments.Find(id);
					switch (attribute)
					{
						case AssignmentAttributes.Title:
							assignment.Title = newValue.ToString();
							break;
						case AssignmentAttributes.Description:
							assignment.Description = newValue.ToString();
							break;
						case AssignmentAttributes.SubmissionDate:
							assignment.SubmissionDate = (DateTime)(object)newValue;
							break;
						case AssignmentAttributes.OralMark:
							assignment.OralMark = (Decimal)(object)newValue;
							break;
						case AssignmentAttributes.TotalMark:
							assignment.TotalMark = (Decimal)(object)newValue;
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

		public static int DeleteAssignment(int assignmentID)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				Assignment a;

				try
				{
					a = sc.Assignments.Find(assignmentID);
					sc.Assignments.Remove(a);
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
