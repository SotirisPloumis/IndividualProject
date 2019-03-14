using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;
using IndividualProject.Manager;

namespace IndividualProject.DB
{
	public static class DBTrainer
	{
		public static int CreateTrainer(string fname, string lname, string subject, out int trainerID)
		{
			Trainer t = new Trainer()
			{
				FirstName = fname,
				LastName = lname,
				Subject = subject
			};

			using (SchoolContext context = new SchoolContext())
			{
				context.Trainers.Add(t);
				int created = context.SaveChanges();
				trainerID = t.Id;
				return created;
			}
		}

		public static ICollection<Trainer> ReadTrainers()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				return sc.Trainers.ToList();
			}
		}

		public static int UpdateTrainer<T>(int id, TrainerAttributes attribute, T newValue)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				Trainer trainer;

				try
				{
					trainer = sc.Trainers.Find(id);
					switch (attribute)
					{
						case TrainerAttributes.Fname:
							trainer.FirstName = newValue.ToString();
							break;
						case TrainerAttributes.Lname:
							trainer.LastName = newValue.ToString();
							break;
						case TrainerAttributes.Subject:
							trainer.Subject = newValue.ToString();
							break;
						default:
							break;
					}
					return sc.SaveChanges();
				}
				catch (Exception)
				{
					return 0;
				}


			}
		}

		public static int DeleteTrainer(int trainerID)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				Trainer t;

				try
				{
					t = sc.Trainers.Find(trainerID);
					sc.Trainers.Remove(t);
					return sc.SaveChanges();
				}
				catch (Exception)
				{
					return 0;
				}


			}
		}
	}
}
