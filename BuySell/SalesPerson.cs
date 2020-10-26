using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Pluralsight.ConcurrentCollections.BuyAndSell
{
	public class SalesPerson
	{
		public string Name { get; }
		public SalesPerson(string name)
		{
			this.Name = name;
		}
		public void Work(TimeSpan workDay, StockController controller)
		{
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < workDay)
			{
				string msg = ServeCustomer(controller);
				if (msg != null)
					Console.WriteLine($"{Name}: {msg}");
			}
		}
		public string ServeCustomer(StockController controller)
		{
			Thread.Sleep(Rnd.NextInt(5));
			TShirt shirt = TShirtProvider.SelectRandomShirt();
			string code = shirt.Code;

			bool custSells = Rnd.TrueWithProb(1.0/6.0);
			if (custSells)
			{
				int quantity = Rnd.NextInt(9) + 1;
				controller.BuyShirts(code, quantity);
				return $"Bought {quantity} of {shirt}";
			}
			else
			{
				bool success = controller.TrySellShirt(code);
				if (success)
					return $"Sold {shirt}";
				else
					return $"Couldn't sell {shirt}: Out of stock";
			}
		}
	}
} 
