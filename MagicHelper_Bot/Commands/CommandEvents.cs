using System;

namespace MagicHelper_Bot.Commands
{
	public delegate void CommandEventHandler (object sender, CommandEventArgs e);

	public class CommandEventArgs : EventArgs
	{
		public long Identifier;
		public string Command;

		public CommandEventArgs (long identifier, string cmd)
		{
			Identifier = identifier;
			Command = cmd;
		}
	}
}

