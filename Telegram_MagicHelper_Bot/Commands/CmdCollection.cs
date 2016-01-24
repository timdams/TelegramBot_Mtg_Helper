using System;
using System.Collections.Generic;
using Telegram_MagicHelper_Bot.Service;

namespace Telegram_MagicHelper_Bot.Commands
{
	/// <summary>
	/// Keeps a collection of all possible commands.
	/// </summary>
	public static class CmdCollection
	{
		/// <summary>
		/// Collection of all possible commands.
		/// </summary>
		public static List<ConcreteCommand> All;

		/// <summary>
		/// Initializes the list of all possible commands.
		/// </summary>
		/// <param name="service">Service.</param>
		public static void InitializeCommands (IMagicService service)
		{
			All = new List<ConcreteCommand> () {
				new StartCommand (),
				new HelpCommand (),
				new CardCommand (service)
			};
		}
	}
}

