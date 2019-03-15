using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;

namespace IndividualProject.DB
{
	public static class DBAssignmentsPerCourse
	{
		public static int CreateAssignmentPerCourse(int assignmentID, int courseID)
		{
			Assignment assignment;
			Course course;

			using (SchoolContext sc = new SchoolContext())
			{
				assignment = sc.Assignments.Find(assignmentID);
				course = sc.Courses.Find(courseID);
				course.Assignments.Add(assignment);
				return sc.SaveChanges();
			}
		}

		public static ICollection<Course> ReadCoursesWithAssignments()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<Course> courses = sc.Courses.ToList();
				foreach (Course c in courses)
				{
					c.Assignments = c.Assignments.ToList();
				}
				return courses;
			}
		}

		public static ICollection<Assignment> ReadAssignmentsWithCourses()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<Assignment> assignments = sc.Assignments.ToList();
				foreach (Assignment a in assignments)
				{
					a.Courses = a.Courses.ToList();
				}
				return assignments;
			}
		}

		public static int DeleteAssignmentPerCourse(int assignmentID, int courseID)
		{
			Assignment assignment;
			Course course;

			using (SchoolContext sc = new SchoolContext())
			{
				assignment = sc.Assignments.Find(assignmentID);
				course = sc.Courses.Find(courseID);
				course.Assignments.Remove(assignment);
				return sc.SaveChanges();
			}
		}
	}
}
