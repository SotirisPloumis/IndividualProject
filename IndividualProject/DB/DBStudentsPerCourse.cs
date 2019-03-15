using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;

namespace IndividualProject.DB
{
	public static class DBStudentsPerCourse
	{
		public static int CreateStudentPerCourse(int studentID, int courseID)
		{
			Student student;
			Course course;

			using (SchoolContext sc = new SchoolContext())
			{
				student = sc.Students.Find(studentID);
				course = sc.Courses.Find(courseID);
				course.Students.Add(student);
				return sc.SaveChanges();
			}
		}

		public static ICollection<Course> ReadCoursesWithStudents()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<Course> courses = sc.Courses.ToList();
				foreach (Course c in courses)
				{
					c.Students = c.Students.ToList();
				}
				return courses;
			}
		}

		public static ICollection<Student> ReadStudentWithCourses()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<Student> students = sc.Students.ToList();
				foreach (Student s in students)
				{
					s.Courses = s.Courses.ToList();
				}
				return students;
			}
		}

		public static int DeleteStudentPerCourse(int studentID, int courseID)
		{
			Student student;
			Course course;

			using (SchoolContext sc = new SchoolContext())
			{
				student = sc.Students.Find(studentID);
				course = sc.Courses.Find(courseID);
				course.Students.Remove(student);
				return sc.SaveChanges();
			}
		}
	}
}
