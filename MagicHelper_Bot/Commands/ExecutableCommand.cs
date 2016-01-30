using TextCommands;

namespace Telegram_MagicHelper_Bot.Commands
{
	/// <summary>
	/// Abstract class that represents a command that can be executed.
	/// </summary>
	public abstract class ExecutableCommand
	{
		public string Keyword;
		public string Description;

		public abstract string Execute (TextCommand cmd);

		public override string ToString ()
		{
			return string.Format ("/{0} : {1}", Keyword, Description);
		}
	}
}

