using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CryptoComTax.Core.Engines.Uphold
{
	public class UpholdDateConverter : DefaultTypeConverter
	{
		public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			// Sat Feb 12 2022 10:36:02 GMT+0000
			// Tue Feb 08 2022 23:50:12 GMT+0000
			string replacedValue = text.Replace("GMT", "");

			var formats = new[] { "ddd MMM dd yyyy HH:mm:ss zzz" };

			if (DateTime.TryParseExact(replacedValue, formats, null, System.Globalization.DateTimeStyles.None, out DateTime result))
			{
				return result;
			}

			throw new InvalidCastException($"Invalid mapping for {memberMapData.Names}");
		}

		public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
		{
			return base.ConvertToString(value, row, memberMapData);
		}
	}
}
