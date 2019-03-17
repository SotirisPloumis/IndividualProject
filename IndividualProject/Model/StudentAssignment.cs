using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndividualProject.Model
{
	public class StudentAssignment
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Key, Column(Order = 0)]
		[ForeignKey("Student")]
		public int StudentId { get; set; }
		public virtual Student Student { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Key, Column(Order = 1)]
		[ForeignKey("Assignment")]
		public int AssignmentId { get; set; }
		public virtual Assignment Assignment { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Key, Column(Order = 2)]
		[ForeignKey("Course")]
		public int CourseId { get; set; }
		public virtual Course Course { get; set; }

		public int oralMark { get; set; }

		public int totalMark { get; set; }

		public bool submitted { get; set; }

		public DateTime submissionDate { get; set; }

	}
}
