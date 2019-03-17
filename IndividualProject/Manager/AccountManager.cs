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
	public static class AccountManager
	{
		public static bool HeadMasterExists()
		{
			ICollection<User> users = DBUser.ReadUsers();

			return users.Where(u => u.Role == "headmaster").Count() != 0;
		}

		public static bool Register()
		{
			ICollection<User> users = DBUser.ReadUsers();

			ConsoleUI.showLine("press 0 anytime to exit");
			bool exit;

			exit = ConsoleUI.GetString(out string username, "give username: ");
			if (exit)
			{
				return false;
			}			

			exit = ConsoleUI.GetPassword(out string password1, "give password: ");
			if (exit)
			{
				return false;
			}

			exit = ConsoleUI.GetPassword(out string password2, "repeat password: ");
			if (exit)
			{
				return false;
			}

			if (!password1.Equals(password2))
			{
				ConsoleUI.showLine("passwords are not the same!!");
				return false;
			}

			try
			{
				int headMasterSaved = DBUser.CreateUser(username, password1, "headmaster", out int id);
				if (headMasterSaved == 0)
				{
					throw new Exception("head master NOT saved");
				}
			}
			catch (Exception e)
			{
				ConsoleUI.showLine(e.Message);
				ConsoleUI.ReadKey();
				return false;
			}

			ConsoleUI.showLine("head master created");

			ConsoleUI.ReadKey();

			return true;
		}

		public static bool Login(out bool loggedIn, out string role, out int id)
		{
			ConsoleUI.showLine("press 0 anytime to exit");
			bool exit;
			loggedIn = false;
			role = "";
			id = -1;

			exit = ConsoleUI.GetString(out string username, "give username: ");
			if (exit)
			{
				return true;
			}

			exit = ConsoleUI.GetPassword(out string password, "give password: ");
			if (exit)
			{
				return true;
			}

			ICollection<User> users = DBUser.ReadUsers();
			User user;

			try
			{
				user = users.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).First();
				role = user.Role;
				id = user.Id;
				loggedIn = true;
				return false;
			}
			catch (InvalidOperationException)
			{
				return true;
			}
		}
	}
}
