using System;
using System.Collections.Generic;
using TextCommands;

namespace MagicHelper_Bot.Commands
{
	public class HelpCommand : ExecutableCommand
	{
		string helpText;

		public HelpCommand ()
		{
			Keyword = "help";
			Description = "Returns this help page.";	
		}

		public void BuildHelp (IEnumerable<ExecutableCommand> commands)
		{
			foreach (var command in commands) {
				helpText += (command.ToString () + '\n'); 
			}
		}

		public override string Execute (TextCommand cmd)
		{
			return helpText;
		}
	}
}

