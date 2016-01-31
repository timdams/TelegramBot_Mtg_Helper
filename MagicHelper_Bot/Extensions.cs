using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicHelper_Bot
{
	public static class Extensions
	{
		/// <summary>
		/// Extension method that prints a dictionary so it's human readable.
		/// </summary>
		/// <returns>The debug string.</returns>
		/// <param name="dictionary">Dictionary.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		/// <typeparam name="TValue">The 2nd type parameter.</typeparam>
		public static string ToDebugString<TKey, TValue> (this IDictionary<TKey, TValue> dictionary)
		{
			return "{" + string.Join (",", dictionary.Select (kv => kv.Key + "=" + kv.Value)) + "}";
		}

		public static long ToUnixTime (this DateTime dateTime)
		{
			TimeSpan timeSpan = (dateTime - new DateTime (1970, 1, 1));
			long timestamp = (long)timeSpan.TotalSeconds;

			return timestamp;
		}
	}
}

