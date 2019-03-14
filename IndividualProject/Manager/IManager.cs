using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProject.Manager
{
	public interface IManager
	{
		void Create();

		void Read();

		void Update();

		void Delete();

		void ShowCRUDMenu();
	}
}
