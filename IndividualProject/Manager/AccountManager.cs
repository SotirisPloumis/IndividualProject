using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.UI;
using IndividualProject.DB;
using IndividualProject.Model;
using System.Security.Cryptography;

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

			string encryptedPassword = CryptoManager.EncryptPassword(password1, out string encryptedSalt);

			try
			{
				int headMasterSaved = DBUser.CreateUser(username, encryptedPassword, encryptedSalt, "headmaster", out int id);
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

			User user;

			try
			{
				user = DBUser.ReadUsers().Where(u => u.Username.Equals(username)).First();
				string hash = user.Password;
				string salt = user.Salt;
				bool correctPassword = CryptoManager.ComparePassword(password, hash, salt);
				if (!correctPassword)
				{
					throw new Exception();
				}
				loggedIn = true;
				role = user.Role;
				id = user.Id;
				return false;
			}
			catch (Exception)
			{
				ConsoleUI.showLine("wrong credentials");
				return true;
			}
			
		}

	}
}
