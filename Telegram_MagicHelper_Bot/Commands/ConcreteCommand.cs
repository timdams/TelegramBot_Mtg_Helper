using System;
using System.Text;
using Telegram_MagicHelper_Bot.Service;
using System.Linq;
using System.Collections.Generic;

namespace Telegram_MagicHelper_Bot.Commands
{
	/// <summary>
	/// Abstract class that represents a command that can be executed.
	/// </summary>
	public abstract class ConcreteCommand
	{
		public string Keyword;
		public string Description;

		public abstract string Execute (Command cmd);

		public override string ToString ()
		{
			return string.Format ("/{0} : {1}", Keyword, Description);
		}
	}

	public class StartCommand : ConcreteCommand
	{
		public StartCommand ()
		{
			Keyword = "start";
			Description = "Returns a greeting if the bot is online.";
		}

		public override string Execute (Command cmd)
		{
			return "Fresh & ready for the pickin'";
		}
	}

	public class HelpCommand : ConcreteCommand
	{
		public HelpCommand ()
		{
			Keyword = "help";
			Description = "Returns this help page.";
		}

		public override string Execute (Command cmd)
		{
			var helpText = new StringBuilder ();
			foreach (var command in CmdCollection.All) {
				helpText.AppendLine (command.ToString ());
			}
			return helpText.ToString ();
		}
	}

	public class CardCommand : ConcreteCommand
	{
		readonly IMagicService service;

		public CardCommand (IMagicService serv)
		{
			Keyword = "card";
			Description = "Searches for a card. use -s to specify a set.";
			service = serv;
		}

		public override string Execute (Command cmd)
		{
			if (cmd.Subject == null) {
				return "Please enter a search term.";
			}
			var parameters = new Dictionary<string,string> ();

			parameters.Add ("name", cmd.Subject);

			string set_param = cmd.Params.FirstOrDefault (p => (p.Key == "s" || p.Key == "set")).Value;
			if (set_param != null)
				parameters.Add ("set", set_param);

			return service.SearchCard (parameters);
		}
	}
}

