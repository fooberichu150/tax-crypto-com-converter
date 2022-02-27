using CryptoComTax.Core.Extensions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CryptoComTax.Core.Engines
{
	public class CustomEnumConverter<T> : DefaultTypeConverter where T : struct
	{
		public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			if (text.TryParseEnum(out T parsedValue, ignoreCase: true))
				return parsedValue;

			throw new InvalidCastException($"Invalid mapping for {typeof(T).Name}");
		}

		public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
		{
			return base.ConvertToString(value, row, memberMapData);
		}
	}

	public class CryptoComDateConverter : DefaultTypeConverter
	{
		public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			return base.ConvertFromString(text, row, memberMapData);
		}

		public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
		{
			if (value.GetType() != typeof(DateTime))
				throw new InvalidOperationException($"Cannot perform DateTime converstion on {value.GetType().Name}");

			var dateObj = (DateTime)value;
			return dateObj.ToString("MM/dd/yyyy HH:mm:ss");
		}
	}
}
