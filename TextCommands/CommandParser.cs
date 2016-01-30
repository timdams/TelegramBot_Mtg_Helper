using System.Text.RegularExpressions;

namespace TextCommands
{
	public static class CommandParser
	{
		/// <summary>
		/// Regex to split a string on -, -- or / .
		/// </summary>
		static readonly Regex CommandSplitter = new Regex (@"\B(?:-+|\/)", RegexOptions.Compiled);

		/// <summary>
		/// Parse the specified input string to a text command with arguments etc.
		/// </summary>
		/// <param name="input">Input string</param>
		static public TextCommand Parse (string input)
		{
			TextCommand result = new TextCommand ();
			string[] commandParts = CommandSplitter.Split (input);
			for (int i = 0; i < commandParts.Length; i++) {
				if (commandParts [i].Equals (string.Empty))
					continue;
					
				string param, arg;
				if (result.Keyword == null) {
					result.Keyword = GetParam (commandParts [i], out arg);
					if (arg != string.Empty)
						result.Params.Add ("subject", arg);
				} else {
					param = GetParam (commandParts [i], out arg);
					if (string.IsNullOrEmpty (param))
						continue;
					if (string.IsNullOrEmpty (arg))
						result.Flags.Add (param);
					else {
						result.Params.Add (param, arg);
					}
				}
			}
			return result;
		}

		static string GetParam (string input, out string rest)
		{
			input = input.Trim ();
			int firstSpace = input.IndexOf (' ');
			if (firstSpace < 0) {
				rest = string.Empty;
				return input;
			} else {
				rest = input.Substring (firstSpace).Trim ();
				return input.Substring (0, firstSpace);
			}
		}
	}
}