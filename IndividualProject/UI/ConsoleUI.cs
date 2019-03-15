using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject.UI
{
	public class ConsoleUI
	{
		public static bool GetInt(out int integer, string message = "")
		{
			bool goodInput;

			do
			{
				Console.Write(message);

				string input = Console.ReadLine();

				goodInput = Int32.TryParse(input, out integer);
				
			} while (!goodInput) ;

			return integer == 0;
		}

		public static bool GetString(out string input, string message = "")
		{
			do
			{
				Console.Write(message);
				input = Console.ReadLine();
			} while (input.Equals(""));
			
			return input.Equals("0");
		}
		
		public static bool GetDate(out DateTime? finalDate, string message = "")
		{
			bool goodDate;
			bool exit;
			finalDate = null;
			DateTime parsedDate;

			do
			{
				Console.Write(message);
				exit = GetString(out string day,"give day: ");
				if (exit)
				{
					return true;
				}

				exit = GetString(out string month,"give month: ");
				if (exit)
				{
					return true;
				}

				exit = GetString(out string year,"give year: ");
				if (exit)
				{
					return true;
				}

				string fullDate = $"{year}/{month}/{day}";
				goodDate = DateTime.TryParse(fullDate, out parsedDate);

			} while (!goodDate);

			finalDate = parsedDate;

			return false;
		}

		public static bool GetDecimal(out decimal dec, string message = "")
		{
			bool goodInput;

			do
			{
				Console.Write(message);

				string input = Console.ReadLine();

				goodInput = Decimal.TryParse(input, out dec);

			} while (!goodInput);

			return dec == 0;
		}

		public static bool GetConfirmation(string message = "")
		{
			string input;
			do
			{
				Console.Write(message);

				input = Console.ReadLine();

			} while (!input.Equals("y") && !input.Equals("Y") && !input.Equals("n") && !input.Equals("N"));

			return input.Equals("y") || input.Equals("Y");
			
		}

		public static void showLine<T>(T message)
		{
			if (message == null)
			{
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine(message.ToString());
			}
			
		}

		public static void changeLine()
		{
			Console.WriteLine();
		}

		public static void showMessage<T>(T message)
		{
			Console.Write(message.ToString());
		}

		public static void Clear()
		{
			Console.Clear();
		}

		public static void ReadKey()
		{
			Console.ReadKey();
		}
	}
}
