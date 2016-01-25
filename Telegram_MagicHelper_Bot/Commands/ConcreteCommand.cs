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
			Description = "Searches for a card. Options: \n" +
			"-f: force first result\n" +
			"-s: define a set.\n" +
			"-t: get card as text.\n" +
			"-l: get the legality of the card.\n" +
			"TIP: a comma is an OR, use quotes to do an absolute search.";
			service = serv;
		}

		public override string Execute (Command cmd)
		{
			// Get Data:

			var parameters = new Dictionary<string,string> ();

			parameters.Add ("name", cmd.Subject);

			string set_param = cmd.Params.FirstOrDefault (p => (p.Key == "s" || p.Key == "set")).Value;
			if (set_param != null)
				parameters.Add ("set", set_param);

			// Build Response after getting data:

			var res = new StringBuilder ();

			List<Card> results = service.SearchCard (parameters).Cards;

			if (results.Count == 0)
				return "Couldn't find that card.";
			else if (results.Count == 1 || cmd.Params.ContainsKey ("f")) {
				Card card = results [0];
				if (cmd.Params.ContainsKey ("t") || card.ImageUrl == null) {
					res.AppendLine (card.ToString ());
				} else {
					res.AppendLine (card.ImageUrl);
				}
				if (cmd.Params.ContainsKey ("l")) {
					res.AppendLine ("LEGAL:");
					foreach (var l in card.Legalities) {
						res.AppendLine (l.ToString ());
					}
				}
					
			} else {
				res.AppendFormat ("Found {0} results:\n", results.Count);

				int maxItems = results.Count > 25 ? 25 : results.Count;
				for (int i = 0; i < maxItems; i++) {
					res.AppendFormat ("{0}. {1} ({2})\n", i + 1, results [i].Name, results [i].Set);
				}
			}

			return res.ToString ();
		}
	}
}

