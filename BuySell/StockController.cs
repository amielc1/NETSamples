using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Collections.ObjectModel;

namespace Pluralsight.ConcurrentCollections.BuyAndSell
{
    public class StockController
    {
        private ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
        int _totalQuantityBought;
        int _totalQuantitySold;
        public void BuyShirts(string code, int quantityToBuy)
        {
            _stock.AddOrUpdate(code, quantityToBuy, (code, oldVaiue) => oldVaiue + quantityToBuy);
            Interlocked.Add(ref _totalQuantityBought, quantityToBuy);
        }

        public bool TrySellShirt(string code)
        {
            _stock.AddOrUpdate(code, 0, (code, oldVal) => oldVal > 0 ? oldVal - 1 : oldVal);
            Interlocked.Increment(ref _totalQuantitySold);
            //ToDo : fix this method. in case the item isn't in dic - we not need to increment.
            return true;

        }

        public void DisplayStock()
        {
            Console.WriteLine("Stock levels by item:");
            foreach (TShirt shirt in TShirtProvider.AllShirts)
            {
                int stockLevel = _stock.GetOrAdd(shirt.Code, 0);
                Console.WriteLine($"{shirt.Name,-30}: {stockLevel}");
            }

            int totalStock = _stock.Values.Sum();
            Console.WriteLine($"\r\nBought = {_totalQuantityBought}");
            Console.WriteLine($"Sold   = {_totalQuantitySold}");
            Console.WriteLine($"Stock  = {totalStock}");
            int error = totalStock + _totalQuantitySold - _totalQuantityBought;
            if (error == 0)
                Console.WriteLine("Stock levels match");
            else
                Console.WriteLine($"Error in stock level: {error}");
        }
    }
}
