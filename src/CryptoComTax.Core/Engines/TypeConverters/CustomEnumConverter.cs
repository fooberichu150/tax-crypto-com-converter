using CryptoComTax.Core.Extensions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CryptoComTax.Core.Engines.TypeConverters
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
}
