using System.Globalization;

namespace Diplom.Views.Converters
{
    public class DateTimeToYearConverter : IMultiValueConverter
    {
		public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
		{
			var bornDate = (DateTime?)value[0];
			var deathdate = (DateTime?)value[1];

			return string.Format("{0} - {1}",
				bornDate?.Year,
				deathdate?.Year);
		}

		public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
