using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;

namespace IndividualProject.DB
{
	public class DBTrainersPerCourse
	{
		public static int CreateTrainerPerCourse(int trainerID, int courseID)
		{
			Trainer trainer;
			Course course;

			using (SchoolContext sc = new SchoolContext())
			{
				trainer = sc.Trainers.Find(trainerID);
				course = sc.Courses.Find(courseID);
				course.Trainers.Add(trainer);
				return sc.SaveChanges();
			}
		}

		public static ICollection<Course> ReadCoursesWithTrainers()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<Course> courses = sc.Courses.ToList();
				foreach (Course c in courses)
				{
					c.Trainers = c.Trainers.ToList();
				}
				return courses;
			}
		}

		public static ICollection<Trainer> ReadTrainersWithCourses()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				ICollection<Trainer> trainers = sc.Trainers.ToList();
				foreach (Trainer t in trainers)
				{
					t.Courses = t.Courses.ToList();
				}
				return trainers;
			}
		}

		public static int DeleteTrainerPerCourse(int trainerID, int courseID)
		{
			Trainer trainer;
			Course course;

			using (SchoolContext sc = new SchoolContext())
			{
				trainer = sc.Trainers.Find(trainerID);
				course = sc.Courses.Find(courseID);
				course.Trainers.Remove(trainer);
				return sc.SaveChanges();
			}
		}
	}
}
