using System;
using TextCommands;

namespace MagicHelper_Bot.Commands
{
	public class StartCommand : ExecutableCommand
	{
		public StartCommand ()
		{
			Keyword = "start";
			Description = "Returns a greeting if the bot is online.";
		}

		public override string Execute (TextCommand cmd)
		{
			return "Fresh & ready for the pickin'";
		}
	}
}

