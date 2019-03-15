using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.UI;
using IndividualProject.DB;
using IndividualProject.Model;

namespace IndividualProject.Manager
{
	public enum TrainerAttributes
	{
		Fname = 1,
		Lname,
		Subject
	}

	public class TrainerManager : IManager
	{
		public void Create()
		{
			bool exit;

			while (true)
			{
				ConsoleUI.Clear();

				string baseMessage = "type trainer's information or type 0 to exit \n";


				exit = ConsoleUI.GetString(out string fname, $"{baseMessage}Trainer's first name: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetString(out string lname, $"{baseMessage}Trainer's last name: ");
				if (exit)
				{
					return;
				}

				exit = ConsoleUI.GetString(out string subject, $"{baseMessage}Trainer's subject: ");
				if (exit)
				{
					return;
				}

				int result = DBTrainer.CreateTrainer(fname, lname, subject, out int id);

				if (result == 0)
				{
					ConsoleUI.showLine("trainer could NOT be saved");
				}
				else
				{
					ConsoleUI.showLine($"trainer created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Trainer> trainers = DBTrainer.ReadTrainers();
			if (trainers.Count() == 0)
			{
				ConsoleUI.showLine("no trainers yet");
			}
			else
			{
				foreach (Trainer t in trainers)
				{
					ConsoleUI.showLine(t);
				}
			}
			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			bool exit;
			

			ICollection<Trainer> trainers = DBTrainer.ReadTrainers();

			if (trainers.Count() == 0)
			{
				ConsoleUI.showLine("No trainers yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showLine("select trainer to update, type 0 to exit");
				foreach (Trainer t in trainers)
				{
					ConsoleUI.showLine(t);
				}
			}
			

			exit = ConsoleUI.GetInt(out int TrainerID, "give trainer id: ");
			if (exit)
			{
				return;
			}

			ConsoleUI.Clear();

			Trainer trainer;
			try
			{
				trainer = trainers.Where(t => t.Id == TrainerID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO TRAINER FOUND WITH ID: {TrainerID}");
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.showLine($"you selected to edit trainer: {trainer.FirstName} {trainer.LastName}");

			ConsoleUI.showLine($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showLine("1. First name");
			ConsoleUI.showLine("2. Last name");
			ConsoleUI.showLine("3. Subject");

			exit = ConsoleUI.GetInt(out int choice);
			if (exit)
			{
				return;
			}
			ConsoleUI.Clear();

			TrainerAttributes attribute = (TrainerAttributes)choice;
			string newInput = "";

			int result = 0;

			switch (attribute)
			{
				case TrainerAttributes.Fname:
					exit = ConsoleUI.GetString(out newInput, "enter new first name: ");
					break;
				case TrainerAttributes.Lname:
					exit = ConsoleUI.GetString(out newInput, "enter new last name: ");
					break;
				case TrainerAttributes.Subject:
					exit = ConsoleUI.GetString(out newInput, "enter new subject: ");
					break;
				default:
					break;
			}

			if (exit)
			{
				return;
			}
			result = DBTrainer.UpdateTrainer(TrainerID, attribute, newInput);

			if (result == 0)
			{
				ConsoleUI.showLine("student update failed");
			}
			else
			{
				ConsoleUI.showLine("student updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			

			ICollection<Trainer> trainers = DBTrainer.ReadTrainers();

			if (trainers.Count() == 0)
			{
				ConsoleUI.showLine("no trainers yet");
				ConsoleUI.ReadKey();
				ConsoleUI.Clear();
				return;
			}
			else
			{
				ConsoleUI.showLine("select trainer to delete, type 0 to exit");
				foreach (Trainer t in trainers)
				{
					ConsoleUI.showLine(t);
				}
			}
			

			exit = ConsoleUI.GetInt(out int TrainerID, "give trainer id: ");
			if (exit)
			{
				return;
			}

			Trainer trainer;
			try
			{
				trainer = trainers.Where(t => t.Id == TrainerID).First();
			}
			catch (Exception)
			{
				ConsoleUI.showLine($"NO TRAINER FOUND WITH ID: {TrainerID}");
				ConsoleUI.ReadKey();
				return;
			}

			bool confirmed = ConsoleUI.GetConfirmation($"are you sure you want to delete trainer {trainer.FirstName} {trainer.LastName}? [y/n]: ");

			int result = 0;
			if (confirmed)
			{
				result = DBTrainer.DeleteTrainer(TrainerID);
				if (result == 0)
				{
					ConsoleUI.showLine("delete failed");
				}
				else
				{
					ConsoleUI.showLine("trainer deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showLine($"select action for trainers");
			ConsoleUI.showLine("1. Create");
			ConsoleUI.showLine("2. Read");
			ConsoleUI.showLine("3. Update");
			ConsoleUI.showLine("4. Delete");
			ConsoleUI.showLine("0. Exit");
		}
	}
}
