using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject.Model
{
	public class Assignment
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime SubmissionDate { get; set; }
		public decimal OralMark { get; set; }
		public decimal TotalMark { get; set; }

		public virtual ICollection<Student> Students { get; set; }
		public virtual ICollection<Course> Courses { get; set; }

		public Assignment()
		{
			Students = new List<Student>();
			Courses = new List<Course>();
		}

		public override string ToString()
		{
			return $"ID:{Id}, {Title}, due {SubmissionDate}";
		}
	}
}
