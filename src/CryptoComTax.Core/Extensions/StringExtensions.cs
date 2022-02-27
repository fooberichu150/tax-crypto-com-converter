using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CryptoComTax.Core.Extensions
{
	public static class StringExtensions
	{
		public static string ToFriendlyString<T>(this T value) where T : struct
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("Only valid with enumerated types");

			// Check to see if there's a description attribute on the enum value.
			FieldInfo fieldInfo = typeof(T).GetField(value.ToString());

			DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
				typeof(DescriptionAttribute), false);

			// If there is, return that description, otherwise return the normal string value
			if (attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}

		public static T ParseEnum<T>(this string stringValue, bool enumParseOnly = false, bool ignoreCase = false) where T : struct
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("Only valid with enumerated types");

			if (enumParseOnly)
				return (T)Enum.Parse(typeof(T), stringValue, ignoreCase);

			foreach (T enumValue in Enum.GetValues(typeof(T)))
			{
				if (string.Compare(enumValue.ToFriendlyString(), stringValue, ignoreCase) == 0)
					return enumValue;
			}

			throw new ArgumentException(string.Format("The string is not valid for the Enum {0}", typeof(T)));
		}

		public static bool TryParseEnum<T>(this string stringValue, out T parsedValue, bool enumParseOnly = false, bool ignoreCase = false) where T : struct
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("Only valid with enumerated types");

			if (!enumParseOnly) // try against friendly names first...
			{
				foreach (T enumValue in Enum.GetValues(typeof(T)))
				{
					if (string.Compare(enumValue.ToFriendlyString(), stringValue) == 0)
					{
						parsedValue = enumValue;
						return true;
					}
				}
			}

			// try against Enum names
			return Enum.TryParse(stringValue, ignoreCase, out parsedValue);
		}

		public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
