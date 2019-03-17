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

			ConsoleUI.showLine("Assignments");
			ICollection<Assignment> assignments = DBAssignmentsPerCourse.ReadAssignmentsWithCourses();

			if (assignments.Count() == 0)
			{
				ConsoleUI.showLine("no assignment yet");
			}
			else
			{
				foreach (Assignment a in assignments)
				{
					ConsoleUI.showLine(a);
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("Courses");
			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("no courses yet");
			}
			else
			{
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("press 0 anytime to exit");

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
				ConsoleUI.showLine($"NO ASSIGNMENT FOUND FOR ID {selectedAssignment}");
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

			bool courseHasAssignment = course.Assignments.Where(a => a.Id == selectedAssignment).Count() > 0;

			if (courseHasAssignment)
			{
				ConsoleUI.showLine("this course already connects to this assigment");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool assignmentHasCourse = assignment.Courses.Where(c => c.Id == selectedCourse).Count() > 0;

			if (assignmentHasCourse)
			{
				ConsoleUI.showLine("this assignment already connects to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBAssignmentsPerCourse.CreateAssignmentPerCourse(selectedAssignment, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.showLine("connection could NOT be made");
			}
			else
			{
				ConsoleUI.showLine("connection saved");
				//add assignment to students of this course
				AssignmentPerStudentManager apsm = new AssignmentPerStudentManager();
				apsm.CreateFromNewAssignment(selectedAssignment, selectedCourse);
			}
			
		}

		public void Read()
		{
			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();
			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				ConsoleUI.showLine("List of courses with assignments\n");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);

					ICollection<Assignment> CourseAssignments = c.Assignments.ToList();
					if (CourseAssignments.Count() == 0)
					{
						ConsoleUI.showLine("->this course has not assignments\n");
					}
					else
					{

						foreach (Assignment a in CourseAssignments)
						{
							ConsoleUI.showMessage("->");
							ConsoleUI.showLine(a);
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

			ICollection<Course> courses = DBAssignmentsPerCourse.ReadCoursesWithAssignments();
			ICollection<Assignment> assignments = DBAssignmentsPerCourse.ReadAssignmentsWithCourses();

			if (courses.Count() == 0)
			{
				ConsoleUI.showLine("No courses yet");
			}
			else
			{
				ConsoleUI.showLine("List of courses with assignments\n");
				foreach (Course c in courses)
				{
					ConsoleUI.showLine(c);

					ICollection<Assignment> CourseAssignments = c.Assignments.ToList();
					if (CourseAssignments.Count() == 0)
					{
						ConsoleUI.showLine("->this course has not assignments\n");
					}
					else
					{

						foreach (Assignment a in CourseAssignments)
						{
							ConsoleUI.showMessage("->");
							ConsoleUI.showLine(a);
						}
						ConsoleUI.changeLine();
					}
				}
			}

			ConsoleUI.changeLine();

			ConsoleUI.showLine("press 0 anytime to exit");

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
				ConsoleUI.showLine($"NO ASSIGNMENT FOUND FOR ID {selectedAssignment}");
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
				ConsoleUI.showLine($"NO COURSE FOUND FOR ID {selectedCourse}");
				ConsoleUI.ReadKey();
				return;
			}

			bool courseHasNoAssignment = course.Assignments.Where(a => a.Id == selectedAssignment).Count() == 0;

			if (courseHasNoAssignment)
			{
				ConsoleUI.showLine("this course doesn't connect to this assignment");
				//ConsoleUI.ReadKey();
				//return;
			}

			bool assignmentHasNoCourse = assignment.Courses.Where(c => c.Id == selectedCourse).Count() == 0;

			if (assignmentHasNoCourse)
			{
				ConsoleUI.showLine("this assigment doesn't connect to this course");
				ConsoleUI.ReadKey();
				return;
			}

			int result = DBAssignmentsPerCourse.DeleteAssignmentPerCourse(selectedAssignment, selectedCourse);

			if (result == 0)
			{
				ConsoleUI.showLine("connection could NOT be removed");
			}
			else
			{
				ConsoleUI.showLine("connection removed");
				AssignmentPerStudentManager apsm = new AssignmentPerStudentManager();
				apsm.DeleteByAssignment(selectedAssignment, selectedCourse);
			}
			
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showLine($"select action for assignments per course");
			ConsoleUI.showLine("1. Create");
			ConsoleUI.showLine("2. Read");
			ConsoleUI.showLine("3. Update");
			ConsoleUI.showLine("4. Delete");
			ConsoleUI.showLine("0. Exit");
		}
	}
}
