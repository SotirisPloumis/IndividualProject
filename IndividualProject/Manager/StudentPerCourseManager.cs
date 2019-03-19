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

			ConsoleUI.ShowLine("Students");
			ICollection<Student> students = DBStudentsPerCourse.ReadStudentWithCourses();

			if (students.Count() == 0)
			{
				ConsoleUI.ShowLine("no students yet");
			}
			else
			{
				foreach (Student s in students)
				{
					ConsoleUI.ShowLine(s);
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("Courses");
			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("no courses yet");
			}
			else
			{
				foreach(Course c in courses)
				{
					ConsoleUI.ShowLine(c);
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("press 0 anytime to exit");

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
				ConsoleUI.ShowLine($"NO STUDENT FOUND FOR ID {selectedStudent}");
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
				ConsoleUI.ShowLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasStudent = course.Students.Where(s => s.Id == selectedStudent).Count() > 0;

			if (courseHasStudent)
			{
				ConsoleUI.ShowLine("this course already connects to this student");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool studentHasCourse = student.Courses.Where(c => c.Id == selectedCourse).Count() > 0;

			if (studentHasCourse)
			{
				ConsoleUI.ShowLine("this student already connects to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBStudentsPerCourse.CreateStudentPerCourse(selectedStudent, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.ShowLine("connection could NOT be made");
			}
			else
			{
				ConsoleUI.ShowLine("connection saved");
				//Add student to assignments of this course
				AssignmentPerStudentManager apcm = new AssignmentPerStudentManager();
				apcm.CreateFromNewStudent(selectedStudent, selectedCourse);
			}
			ConsoleUI.ReadKey();
		}

		public void Read()
		{
			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();
			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of courses with students\n");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);

					ICollection<Student> CourseStudents = c.Students.ToList();
					if (CourseStudents.Count() == 0)
					{
						ConsoleUI.ShowLine("->this course has not students\n");
					}
					else
					{
						
						foreach (Student s in CourseStudents)
						{
							ConsoleUI.ShowMessage("->");
							ConsoleUI.ShowLine(s);
						}
						ConsoleUI.ChangeLine();
					}
				}
			}

			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			ConsoleUI.ShowLine("Update is not available for this connection, please choose create or delete instead");
		}

		public void Delete()
		{
			bool exit;

			ICollection<Course> courses = DBStudentsPerCourse.ReadCoursesWithStudents();
			ICollection<Student> students = DBStudentsPerCourse.ReadStudentWithCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of courses with students\n");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);

					ICollection<Student> CourseStudents = c.Students.ToList();
					if (CourseStudents.Count() == 0)
					{
						ConsoleUI.ShowLine("->this course has not students\n");
					}
					else
					{

						foreach (Student s in CourseStudents)
						{
							ConsoleUI.ShowMessage("->");
							ConsoleUI.ShowLine(s);
						}
						ConsoleUI.ChangeLine();
					}
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("press 0 anytime to exit");

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
				ConsoleUI.ShowLine($"NO STUDENT FOUND FOR ID {selectedStudent}");
				ConsoleUI.ReadKey();
				return;
			}

			exit = ConsoleUI.GetInt(out int selectedCourse, "give course id to disconnect: ");
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
				ConsoleUI.ShowLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasNoStudent = course.Students.Where(s => s.Id == selectedStudent).Count() == 0;

			if (courseHasNoStudent)
			{
				ConsoleUI.ShowLine("this course doesn't connect to this student");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool studentHasNoCourse = student.Courses.Where(c => c.Id == selectedCourse).Count() == 0;

			if (studentHasNoCourse)
			{
				ConsoleUI.ShowLine("this student doesn't connect to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBStudentsPerCourse.DeleteStudentPerCourse(selectedStudent, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.ShowLine("connection could NOT be removed");
			}
			else
			{
				ConsoleUI.ShowLine("connection removed");
				AssignmentPerStudentManager apsm = new AssignmentPerStudentManager();
				apsm.DeleteByStudent(selectedStudent, selectedCourse);
			}
			ConsoleUI.ReadKey();
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.ShowLine($"select action for students per course");
			ConsoleUI.ShowLine("1. Create");
			ConsoleUI.ShowLine("2. Read");
			ConsoleUI.ShowLine("3. Update");
			ConsoleUI.ShowLine("4. Delete");
			ConsoleUI.ShowLine("0. Exit");
		}
	}
}
