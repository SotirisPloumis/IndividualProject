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

		public int OralMark { get; set; }

		public int TotalMark { get; set; }

		public bool Submitted { get; set; }

		public DateTime SubmissionDate { get; set; }

		public override string ToString()
		{
			Student s = this.Student;
			Assignment a = this.Assignment;
			Course c = this.Course;
			bool isMarked = this.OralMark != -1 && this.TotalMark != -1;
			string marksString = isMarked ? $"({this.OralMark}/{this.TotalMark})" : "not marked yet";
			string submissionString = this.Submitted ? $"submitted at {this.SubmissionDate}" : "not submitted yet";
			return ($"student {s.Id} {s.FirstName} {s.LastName} has assignment {a.Id} {a.Title} by course {c.Id} {c.Title} {marksString} {submissionString}");
		}
	}
}
