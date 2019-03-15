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
	public class StudentPerCourseManager : IManager
	{
		public void Create()
		{
			bool exit;

			ConsoleUI.showLine("Students");
			ICollection<Student> students = DBStudentsPerCourse.ReadStudentWithCourses();

			if (students.Count() == 0)
			{
				ConsoleUI.showLine("no students yet");
			}
			else
			{
				foreach (Student s in students)
				{
					ConsoleUI.showLine(s);
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("Courses");
			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("no courses yet");
			}
			else
			{
				foreach(Course c in courses)
				{
					ConsoleUI.showLine(c);
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("press 0 anytime to exit");

			exit = ConsoleUI.GetInt(out int selectedStudent, "give student id to connect: ");
			if (exit)
			{
				return;
			}

			Student student;

			try
			{
				student = students.Where(s => s.Id == selectedStudent).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO STUDENT FOUND FOR ID {selectedStudent}");
				ConsoleUI.ReadKey();
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedCourse, "give course id to connect: ");
			if (exit)
			{
				return;
			}

			Course course;

			try
			{
				course = courses.Where(c => c.Id == selectedCourse).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasStudent = course.Students.Where(s => s.Id == selectedStudent).Count() > 0;

			if (courseHasStudent)
			{
				ConsoleUI.showLine("this course already connects to this student");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool studentHasCourse = student.Courses.Where(c => c.Id == selectedCourse).Count() > 0;

			if (studentHasCourse)
			{
				ConsoleUI.showLine("this student already connects to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBStudentsPerCourse.CreateStudentPerCourse(selectedStudent, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.showLine("connection could NOT be made");
			}
			else
			{
				ConsoleUI.showLine("connection saved");
			}
			ConsoleUI.ReadKey();
		}

		public void Read()
		{
			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();
			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				ConsoleUI.showLine("List of courses with students\n");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);

					ICollection<Student> CourseStudents = c.Students.ToList();
					if (CourseStudents.Count() == 0)
					{
						ConsoleUI.showLine("->this course has not students\n");
					}
					else
					{
						
						foreach (Student s in CourseStudents)
						{
							ConsoleUI.showMessage("->");
							ConsoleUI.showLine(s);
						}
						ConsoleUI.changeLine();
					}
				}
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			ConsoleUI.showLine("Update is not available for this connection, please choose create or delete instead");
		}

		public void Delete()
		{
			bool exit;

			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();
			ICollection<Student> students = DBStudentsPerCourse.ReadStudentWithCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				ConsoleUI.showLine("List of courses with students\n");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);

					ICollection<Student> CourseStudents = c.Students.ToList();
					if (CourseStudents.Count() == 0)
					{
						ConsoleUI.showLine("->this course has not students\n");
					}
					else
					{

						foreach (Student s in CourseStudents)
						{
							ConsoleUI.showMessage("->");
							ConsoleUI.showLine(s);
						}
						ConsoleUI.changeLine();
					}
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("press 0 anytime to exit");

			exit = ConsoleUI.GetInt(out int selectedStudent, "give student id to disconnect: ");
			if (exit)
			{
				return;
			}

			Student student;

			try
			{
				student = students.Where(s => s.Id == selectedStudent).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO STUDENT FOUND FOR ID {selectedStudent}");
				ConsoleUI.ReadKey();
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedCourse, "give course id to discconnect: ");
			if (exit)
			{
				return;
			}

			Course course;

			try
			{
				course = courses.Where(c => c.Id == selectedCourse).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasNoStudent = course.Students.Where(s => s.Id == selectedStudent).Count() == 0;

			if (courseHasNoStudent)
			{
				ConsoleUI.showLine("this course doesn't connect to this student");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool studentHasNoCourse = student.Courses.Where(c => c.Id == selectedCourse).Count() == 0;

			if (studentHasNoCourse)
			{
				ConsoleUI.showLine("this student doesn't connect to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBStudentsPerCourse.DeleteStudentPerCourse(selectedStudent, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.showLine("connection could NOT be removed");
			}
			else
			{
				ConsoleUI.showLine("connection removed");
			}
			ConsoleUI.ReadKey();
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showLine($"select action for students per course");
			ConsoleUI.showLine("1. Create");
			ConsoleUI.showLine("2. Read");
			ConsoleUI.showLine("3. Update");
			ConsoleUI.showLine("4. Delete");
			ConsoleUI.showLine("0. Exit");
		}
	}
}
