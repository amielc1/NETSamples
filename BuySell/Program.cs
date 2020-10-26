using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pluralsight.ConcurrentCollections.BuyAndSell
{
	class Program
	{
		static void Main(string[] args)
		{
			StockController controller = new StockController();
			TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

            controller.DisplayStock();

            Task t1 = Task.Run(() => new SalesPerson("Amiel").Work(workDay, controller));
            Task t2 = Task.Run(() => new SalesPerson("Talya").Work(workDay, controller));
            Task t3 = Task.Run(() => new SalesPerson("David").Work(workDay, controller));

            Task.WaitAll(t1, t2, t3);

            controller.DisplayStock();

            Console.ReadKey();
		}
	}
}