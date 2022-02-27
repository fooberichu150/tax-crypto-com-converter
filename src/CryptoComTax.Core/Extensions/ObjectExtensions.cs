using System.Text.Json;

namespace CryptoComTax.Core.Extensions
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Cheater way to clone an object and avoid copying memory references
		/// </summary>
		/// <typeparam name="T">Type to clone</typeparam>
		/// <param name="source">Source object</param>
		/// <returns>Cloned object</returns>
		/// <seealso href="https://stackoverflow.com/a/506677"/>
		/// <exception cref="ArgumentNullException"></exception>
		public static T Clone<T>(this T source) where T : class
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var serialized = JsonSerializer.Serialize(source);

#pragma warning disable CS8603 // Possible null reference return.
			return JsonSerializer.Deserialize<T>(serialized);
#pragma warning restore CS8603 // Possible null reference return.
		}
	}
}
