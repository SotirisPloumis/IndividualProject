using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;
using IndividualProject.Manager;

namespace IndividualProject.DB
{
	public static class DBCourse
	{
		public static int CreateCourse(string title, string stream, string type, DateTime start, DateTime end, out int courseID)
		{
			Course newCourse = new Course()
			{
				Title = title,
				Stream = stream,
				Type = type,
				StartDate = start,
				EndDate = end
			};

			using (SchoolContext context = new SchoolContext())
			{
				context.Courses.Add(newCourse);
				int created = context.SaveChanges();
				courseID = newCourse.Id;
				return created;
			}
		}

		public static ICollection<Course> ReadCourses()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				return sc.Courses.ToList();
			}
		}

		public static int UpdateCourse<T>(int id, CourseAttributes attribute, T newValue)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				Course course;

				try
				{
					course = sc.Courses.Find(id);
					switch (attribute)
					{
						case CourseAttributes.Title:
							course.Title = newValue.ToString();
							break;
						case CourseAttributes.Stream:
							course.Stream = newValue.ToString();
							break;
						case CourseAttributes.Type:
							course.Type = newValue.ToString();
							break;
						case CourseAttributes.StartDate:
							course.StartDate = (DateTime)(object)newValue;
							break;
						case CourseAttributes.EndDate:
							course.EndDate = (DateTime)(object)newValue;
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

		public static int DeleteCourse(int courseID)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				Course c;

				try
				{
					c = sc.Courses.Find(courseID);
					sc.Courses.Remove(c);
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
