using System.Globalization;

namespace Diplom.Views.Converters
{
    public class DateTimeToYearConverter : IMultiValueConverter
    {
		public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
		{
			var name = (string)value[0];
			var surname = (string)value[1];
			var patronymic = (string)value[2];
			var bornDate = (DateTime?)value[3];
			var deathdate = (DateTime?)value[4];

			return string.Format("{0} {1} {2} ({3} - {4})",
				name,
				surname,
				patronymic,
				bornDate?.Year,
				deathdate?.Year);
		}

		public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
