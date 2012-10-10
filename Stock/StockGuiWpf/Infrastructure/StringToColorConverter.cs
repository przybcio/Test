// -----------------------------------------------------------------------
// <copyright file="StringToColorConverter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StockGuiWpf.Infrastructure
{
	using System;
	using System.Windows;
	using System.Windows.Data;
	using System.Windows.Media;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	//[ValueConversion(typeof(object), typeof(int))] 
	public class StringToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string str = value as string;
			if (str.StartsWith("-"))
				return Brushes.Red;
			else
				return Brushes.Green;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;// return new SolidColorBrush(Colors.Red);
		}
	}
}
