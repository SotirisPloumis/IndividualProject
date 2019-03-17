using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndividualProject.Model
{
	public class Trainer
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Subject { get; set; }

		public virtual ICollection<Course> Courses { get; set; }

		public Trainer()
		{
			Courses = new List<Course>();
		}

		public override string ToString()
		{
			return $"ID:{Id}, {FirstName} {LastName}, teaches {Subject}";
		}
	}
}
