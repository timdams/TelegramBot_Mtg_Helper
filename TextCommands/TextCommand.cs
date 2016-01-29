using System;
using System.Collections.Generic;

namespace TextCommands
{
	/// <summary>
	/// Base class for any text that can be seen as a command.
	/// </summary>
	public class TextCommand
	{
		/// <summary>
		/// Keyword of the command, e.g.: /help
		/// </summary>
		public string Keyword;

		/// <summary>
		/// Parameters of the command, e.g.: -s JOU 
		/// The Key represents the parameter, the value is its value.
		/// </summary>
		public Dictionary<string, string> Params;

		/// <summary>
		/// Flags of the command, e.g.: -t or -f
		/// </summary>
		public List<string> Flags;

		public TextCommand ()
		{
			Params = new Dictionary<string, string> ();
			Flags = new List<string> ();
		}

		public override string ToString ()
		{
			return string.Format ("{0}: [{1}] [{2}]", Keyword, Params.ToDebugString (), String.Join (",", Flags));
		}
	}
}

