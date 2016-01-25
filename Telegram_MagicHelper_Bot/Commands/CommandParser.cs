using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Telegram_MagicHelper_Bot.Commands
{
	public static class CommandParser
	{
		/// <summary>
		/// Regex that matches - or -- followed by a letter or word, optionally followed by spaced params.
		/// </summary>
		static readonly Regex ParamSplitter = new Regex (@"\ (-|--)[A-Z]+\ *(\ *\w*\ *)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		/// <summary>
		/// Parse the specified input string to a text command with arguments etc.
		/// </summary>
		/// <param name="input">Input string</param>
		public static Command Parse (string input)
		{
			var newCommand = new Command ();
			string subjectAndParams;

			newCommand.Keyword = ExtractKeyword (input, out subjectAndParams);
			newCommand.Params = ExtractParams (subjectAndParams, out newCommand.Subject);

			return newCommand;
		}

		/// <summary>
		/// Extracts the keyword from the input string.
		/// </summary>
		/// <returns>The keyword that was found, without leading slash.</returns>
		/// <param name="input">the input string to extract from.</param>
		/// <param name="rest">the input, but with the keyword removed.</param>
		static string ExtractKeyword (string input, out string rest)
		{
			if (!input.StartsWith ("/"))
				throw new ArgumentException ("Trying to parse a non-command, telegram shouldn't send me this.");

			string keyword;
			var indexOfFirstSpace = input.IndexOf (' ');

			if (indexOfFirstSpace < 0) {
				keyword = input.Substring (1); // skip slash
				rest = null;
			} else {
				keyword = input.Substring (1, indexOfFirstSpace).Trim (); // skip slash and get until first space.
				rest = input.Remove (0, indexOfFirstSpace).Trim ();
			}

			#if DEBUG
			Console.WriteLine ("Got keyword: " + keyword);
			#endif

			return keyword;
		}

		/// <summary>
		/// Extracts the parameters from the input string
		/// </summary>
		/// <returns>The extracted parameters</returns>
		/// <param name="input">the input string to extract from</param>
		/// <param name="rest">the input, but with the parameters removed.</param>
		static Dictionary<string, string> ExtractParams (string input, out string rest)
		{
			if (input == null) {
				rest = null;
				return null;
			}
			
			var values = new Dictionary<string, string> (); 

			string[] split = ParamSplitter.Matches (input).OfType<Match> ().Select (m => m.Value).ToArray ();

			rest = ParamSplitter.Replace (input, "").Trim ();

			foreach (var param in split) {
				var current = param.Trim ();
				if (!current.StartsWith ("-"))
					continue;
				
				var indexOfFirstSpace = current.IndexOf (' ');
				if (indexOfFirstSpace < 0) {
					values.Add (current.Substring (1), "");
				} else {
					values.Add (current.Substring (1, indexOfFirstSpace).Trim (), current.Remove (0, indexOfFirstSpace + 1).Trim ());
				}
			}

			#if DEBUG
			Console.WriteLine ("Split into: " + string.Join (",", split) + ".");
			Console.WriteLine ("Rest: " + rest + ".");
			Console.WriteLine ("Parsed to: " + values.ToDebugString ());
			#endif

			return values;
		}

	}
}

