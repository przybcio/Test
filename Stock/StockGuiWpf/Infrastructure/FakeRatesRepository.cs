using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockGuiWpf.Abstract;
using StockGuiWpf.Model;
using System.Collections.ObjectModel;

namespace StockGuiWpf.Infrastructure
{
    public class FakeRatesRepository : IRatesRepository
    {
        public StockRate GetRate()
        {
           
            return new StockRate
            {
                Rate = "3,4567",
                RateChange = "-0,0098",
                RateDate = DateTime.Now
            }; 
        }
    }
}
