using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject.Model
{
	public class Course
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Stream { get; set; }
		public string Type { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public virtual ICollection<Student> Students { get; set; }
		public virtual ICollection<Assignment> Assignments { get; set; }
		public virtual ICollection<Trainer> Trainers { get; set; }

		public Course()
		{
			Students = new List<Student>();
			Assignments = new List<Assignment>();
			Trainers = new List<Trainer>();
		}

		public override string ToString()
		{
			return $"{Title}, {Stream}, {Type}";
		}
	}
}
