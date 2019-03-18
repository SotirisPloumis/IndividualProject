using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualProject.Model;
using IndividualProject.Manager;

namespace IndividualProject.DB
{
	public class DBUser
	{
		public static int CreateUser(string username, string password, string salt, string role, out int id)
		{
			User u = new User()
			{
				Username = username,
				Password = password,
				Salt = salt,
				Role = role
			};

			using (SchoolContext context = new SchoolContext())
			{
				context.Users.Add(u);
				int created = context.SaveChanges();
				id = u.Id;
				return created;
			}
		}

		public static ICollection<User> ReadUsers()
		{
			using (SchoolContext sc = new SchoolContext())
			{
				return sc.Users.ToList();
			}
		}

		public static int UpdateUser<T>(int id, Object attribute, T newValue)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				User user;

				try
				{
					user = sc.Users.Find(id);
					switch (attribute)
					{
						case UserAttributes.Username:
							user.Username = newValue.ToString();
							break;
						case UserAttributes.Password:
							user.Password = newValue.ToString();
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

		public static int DeleteUser(int userID)
		{
			using (SchoolContext sc = new SchoolContext())
			{
				User u;

				try
				{
					u = sc.Users.Find(userID);
					sc.Users.Remove(u);
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
