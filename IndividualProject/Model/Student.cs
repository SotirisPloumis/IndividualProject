using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndividualProject.Model
{
	public class Student
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public decimal Fees { get; set; }

		public virtual ICollection<Assignment> Assignments { get; set; }
		public virtual ICollection<Course> Courses { get; set; }

		public Student()
		{
			Assignments = new List<Assignment>();
			Courses = new List<Course>();
		}

		public override string ToString()
		{
			return $"ID:{Id}, {FirstName} {LastName}, born in {DateOfBirth}, owes {Fees} euros";
		}
	}
}