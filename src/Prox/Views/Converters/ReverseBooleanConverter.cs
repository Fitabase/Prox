using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prox
{
	public class ReverseBooleanConverter : IValueConverter, IMarkupExtension
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var b = (bool)value;
			return !b;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object ProvideValue (IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}