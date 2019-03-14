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

				int result = DBTrainer.Create(fname, lname, subject, out int id);

				if (result == 0)
				{
					ConsoleUI.showMessage("trainer could NOT be saved");
				}
				else
				{
					ConsoleUI.showMessage($"trainer created with id: {id}");
				}

				ConsoleUI.ReadKey();

			}
		}

		public void Read()
		{
			ICollection<Trainer> trainers = DBTrainer.ReadTrainers();
			if (trainers.Count() == 0)
			{
				ConsoleUI.showMessage("no trainers yet");
			}
			else
			{
				foreach (Trainer t in trainers)
				{
					ConsoleUI.showMessage(t);
				}
			}
			ConsoleUI.ReadKey();
			ConsoleUI.Clear();
		}

		public void Update()
		{
			bool exit;
			ConsoleUI.showMessage("select trainer to update, type 0 to exit");

			ICollection<Trainer> trainers = DBTrainer.ReadTrainers();
			foreach (Trainer t in trainers)
			{
				ConsoleUI.showMessage(t);
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
				ConsoleUI.showMessage($"NO TRAINER FOUND WITH ID: {TrainerID}");
				ConsoleUI.ReadKey();
				return;
			}

			ConsoleUI.showMessage($"you selected to edit trainer: {trainer.FirstName} {trainer.LastName}");

			ConsoleUI.showMessage($"select attribute to edit, type 0 anytime to exit");
			ConsoleUI.showMessage("1. First name");
			ConsoleUI.showMessage("2. Last name");
			ConsoleUI.showMessage("3. Subject");

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
				ConsoleUI.showMessage("student update failed");
			}
			else
			{
				ConsoleUI.showMessage("student updated successfully");
			}
			ConsoleUI.ReadKey();

		}

		public void Delete()
		{
			bool exit;
			ConsoleUI.showMessage("select trainer to delete, type 0 to exit");

			ICollection<Trainer> trainers = DBTrainer.ReadTrainers();
			foreach (Trainer t in trainers)
			{
				ConsoleUI.showMessage(t);
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
				ConsoleUI.showMessage($"NO TRAINER FOUND WITH ID: {TrainerID}");
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
					ConsoleUI.showMessage("delete failed");
				}
				else
				{
					ConsoleUI.showMessage("trainer deleted successfully");
				}
				ConsoleUI.ReadKey();
			}
		}

		public void ShowCRUDMenu()
		{
			ConsoleUI.showMessage($"select action for trainers");
			ConsoleUI.showMessage("1. Create");
			ConsoleUI.showMessage("2. Read");
			ConsoleUI.showMessage("3. Update");
			ConsoleUI.showMessage("4. Delete");
			ConsoleUI.showMessage("0. Exit");
		}
	}
}
