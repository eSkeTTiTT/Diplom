using System.Globalization;

namespace Diplom.Views.Converters
{
    public class DateTimeToYearConverter : IMultiValueConverter
    {
		public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
		{
			var name = (string?)value[0];
			var bornDate = (DateTime?)value[1];
			var deathdate = (DateTime?)value[2];

			return string.Format("{0} ({1} - {2})",
				name,
				bornDate?.Year,
				deathdate?.Year);
		}

		public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
