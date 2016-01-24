using System;
using System.Collections.Generic;

namespace Telegram_MagicHelper_Bot.Commands
{
	/// <summary>
	/// Base class for any text that can be seen as a command.
	/// </summary>
	public class Command
	{
		/// <summary>
		/// Keyword of the command, e.g.: /help
		/// </summary>
		public string Keyword;

		/// <summary>
		/// Subject of the command, e.g.: (/card) nyx-fleece ram
		/// </summary>
		public string Subject;

		/// <summary>
		/// Parameters of the command, e.g.: (/card nyx-fleece ram) -s JOU -v
		/// The Key represents the parameter, the value is it's value.
		/// </summary>
		public Dictionary<string, string> Params;
	}
}

