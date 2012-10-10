using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockGuiWpf.Abstract;
using StockGuiWpf.Model;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace StockGuiWpf.Infrastructure
{
    public class StooqService : IRatesRepository, IDisposable
    {
        private WebClient client;
        public StooqService()
        {
            client = new WebClient();
        }

        public Model.StockRate GetRate()
        {
            try
            {
                if (client != null)
                {
                    var myUri = new Uri(@"http://stooq.pl/q/?s=chfpln");
                    var myStream = client.OpenRead(myUri);
                    using (var readStream = new StreamReader(myStream))
                    {
                        var str = readStream.ReadToEnd();
                        readStream.Close();
                        return new StockRate { Rate = str.StockSubstring("Kurs", 58, 12), RateChange = str.StockSubstring("Zmiana", 47, 12), RateDate = DateTime.Now };

                    }
                }
                else
                    throw new NullReferenceException();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());
                throw new IndexOutOfRangeException();
            }
        }

        public void Dispose()
        {
            if (client != null)
                client.Dispose();

        }
    }
}
