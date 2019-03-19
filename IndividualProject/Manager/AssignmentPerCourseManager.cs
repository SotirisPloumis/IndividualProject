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
	public class AssignmentPerCourseManager : IManager
	{
		public void Create()
		{
			bool exit;

			ConsoleUI.ShowLine("Assignments");
			ICollection<Assignment> assignments = DBAssignmentsPerCourse.ReadAssignmentsWithCourses();

			if (assignments.Count() == 0)
			{
				ConsoleUI.ShowLine("no assignment yet");
			}
			else
			{
				foreach (Assignment a in assignments)
				{
					ConsoleUI.ShowLine(a);
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("Courses");
			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("no courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("press 0 anytime to exit");

			exit = ConsoleUI.GetInt(out int selectedAssignment, "give assignment id to connect: ");
			if (exit)
			{
				return;
			}

			Assignment assignment;

			try
			{
				assignment = assignments.Where(a => a.Id == selectedAssignment).First();
			}
			catch (Exception)
			{
				ConsoleUI.ShowLine($"NO ASSIGNMENT FOUND FOR ID {selectedAssignment}");
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

			bool courseHasAssignment = course.Assignments.Where(a => a.Id == selectedAssignment).Count() > 0;

			if (courseHasAssignment)
			{
				ConsoleUI.ShowLine("this course already connects to this assigment");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool assignmentHasCourse = assignment.Courses.Where(c => c.Id == selectedCourse).Count() > 0;

			if (assignmentHasCourse)
			{
				ConsoleUI.ShowLine("this assignment already connects to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBAssignmentsPerCourse.CreateAssignmentPerCourse(selectedAssignment, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.ShowLine("connection could NOT be made");
			}
			else
			{
				ConsoleUI.ShowLine("connection saved");
				//add assignment to students of this course
				AssignmentPerStudentManager apsm = new AssignmentPerStudentManager();
				apsm.CreateFromNewAssignment(selectedAssignment, selectedCourse);
				ConsoleUI.ReadKey();

			}

		}

		public void Read()
		{
			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();
			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of courses with assignments\n");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);

					ICollection<Assignment> CourseAssignments = c.Assignments.ToList();
					if (CourseAssignments.Count() == 0)
					{
						ConsoleUI.ShowLine("->this course has not assignments\n");
					}
					else
					{

						foreach (Assignment a in CourseAssignments)
						{
							ConsoleUI.ShowMessage("->");
							ConsoleUI.ShowLine(a);
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

			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();
			ICollection<Assignment> assignments = DBAssignmentsPerCourse.ReadAssignmentsWithCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.ShowLine("No courses yet");
			}
			else
			{
				ConsoleUI.ShowLine("List of courses with assignments\n");
				foreach (Course c in courses)
				{
					ConsoleUI.ShowLine(c);

					ICollection<Assignment> CourseAssignments = c.Assignments.ToList();
					if (CourseAssignments.Count() == 0)
					{
						ConsoleUI.ShowLine("->this course has not assignments\n");
					}
					else
					{

						foreach (Assignment a in CourseAssignments)
						{
							ConsoleUI.ShowMessage("->");
							ConsoleUI.ShowLine(a);
						}
						ConsoleUI.ChangeLine();
					}
				}
			}

			ConsoleUI.ChangeLine();

			ConsoleUI.ShowLine("press 0 anytime to exit");

			exit = ConsoleUI.GetInt(out int selectedAssignment, "give assignment id to disconnect: ");
			if (exit)
			{
				return;
			}

			Assignment assignment;

			try
			{
				assignment = assignments.Where(a => a.Id == selectedAssignment).First();
			}
			catch (Exception)
			{
				ConsoleUI.ShowLine($"NO ASSIGNMENT FOUND FOR ID {selectedAssignment}");
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

			bool courseHasNoAssignment = course.Assignments.Where(a => a.Id == selectedAssignment).Count() == 0;

			if (courseHasNoAssignment)
			{
				ConsoleUI.ShowLine("this course doesn't connect to this assignment");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool assignmentHasNoCourse = assignment.Courses.Where(c => c.Id == selectedCourse).Count() == 0;

			if (assignmentHasNoCourse)
			{
				ConsoleUI.ShowLine("this assigment doesn't connect to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBAssignmentsPerCourse.DeleteAssignmentPerCourse(selectedAssignment, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.ShowLine("connection could NOT be removed");
			}
			else
			{
				ConsoleUI.ShowLine("connection removed");
				AssignmentPerStudentManager apsm = new AssignmentPerStudentManager();
				apsm.DeleteByAssignment(selectedAssignment, selectedCourse);
			}
			
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.ShowLine($"select action for assignments per course");
			ConsoleUI.ShowLine("1. Create");
			ConsoleUI.ShowLine("2. Read");
			ConsoleUI.ShowLine("3. Update");
			ConsoleUI.ShowLine("4. Delete");
			ConsoleUI.ShowLine("0. Exit");
		}
	}
}
