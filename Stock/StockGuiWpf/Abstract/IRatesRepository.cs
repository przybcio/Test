using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockGuiWpf.Model;
using System.Collections.ObjectModel;

namespace StockGuiWpf.Abstract
{
    public interface IRatesRepository
    {
        StockRate GetRate();
    }
}
