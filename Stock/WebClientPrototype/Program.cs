using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using System.Threading;
using System.Diagnostics;

namespace WebClientPrototype
{
	public class MyStockRates
	{
		public string StockRate { get; set; }
		public string StockChange { get; set; }

		public void DoNothing(object state)
		{
			Debug.WriteLine("Do nothing");
		}
	}

	class Program
	{
		private static Timer _myTimer = new Timer(new TimerCallback(RunClient));
		private static IList<MyStockRates> _myList = new List<MyStockRates>();
		static void Main(string[] args)
		{
			_myTimer.Change(0, 5000);

			//RunClient();
			Console.ReadKey(true);

			foreach (var item in _myList)
			{
				Console.WriteLine(string.Format("Rate{0} and change {1}", item.StockRate, item.StockChange));
			}
			Console.ReadKey(true);
		}

		private static void RunClient(object state)
		{
			try
			{
				using (var client = new WebClient())
				{
					var myUri = new Uri(@"http://stooq.pl/q/?s=chfpln");
					var myStream = client.OpenRead(myUri);
					using (var readStream = new StreamReader(myStream))
					{
						var str = readStream.ReadToEnd();

						var myRate = new MyStockRates { StockChange = str.StockSubstring("Zmiana", 47, 12), StockRate = str.StockSubstring("kurs", 29, 12) };

						Debug.WriteLine(string.Format("Rate{0} and change {1}", myRate.StockRate, myRate.StockChange));

						_myList.Add(myRate);
					}

				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			
		}


	}

	static public class MyExtension
	{
		public static string StockSubstring(this string str, string stockName, int additionalOffset, int characteCount)
		{

			var myIndex = str.IndexOf(stockName);

			var mystring = str.Substring(myIndex + additionalOffset, characteCount);

			var secondIndex = mystring.IndexOf("<");

			return mystring.Substring(0, secondIndex);
		}

	}
}
