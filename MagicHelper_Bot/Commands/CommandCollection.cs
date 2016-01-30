using System;
using System.Collections.Generic;
using MagicHelper_Bot.Service;
using TextCommands;
using System.Text;

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

	public class CardCommand : ExecutableCommand
	{
		readonly IMagicService service;

		public CardCommand (IMagicService serv)
		{
			Keyword = "card";
			Description = @"Searches for a card. Options:
				-s, -set: define set(s).
				-c, -color: define color(s).
				-p, -page: define the page of results.
				-l, -legal: get the legality of the card.
				-r, -rules: get the special rulings of the card.
				-f, -first: force first result.
				-t, -text: get the card as text.
			TIP: a comma is an OR, use quotes to do an absolute search.";
			service = serv;
		}

		public override string Execute (TextCommand cmd)
		{
			var res = new StringBuilder ();

			List<Card> results = service.SearchCard (cmd.Params).Cards;

			if (results.Count == 0)
				return "Couldn't find that card.";
			else if (results.Count == 1 || cmd.Flags.Contains ("f") || cmd.Flags.Contains ("first")) {
				Card card = results [0];
				if (card.ImageUrl == null || cmd.Flags.Contains ("t") || cmd.Flags.Contains ("text")) {
					res.AppendLine (card.ToString ());
				} else {
					res.AppendLine (card.ImageUrl);
				}
				if (cmd.Flags.Contains ("l") || cmd.Flags.Contains ("legal")) {
					res.AppendLine ("Legal in:");
					foreach (var l in card.Legalities) {
						res.AppendLine (l.ToString ());
					}
				}
				if (cmd.Flags.Contains ("r") || cmd.Flags.Contains ("rules")) {
					if (card.Rulings == null)
						res.AppendLine ("No special rulings.");
					else {
						res.AppendLine ("Rulings:");
						foreach (var r in card.Rulings) {
							res.AppendLine (r.ToString ());
						}
					}
				}
			} else {
				res.AppendFormat ("Found {0} results:\n", results.Count);
				for (int i = 0; i < results.Count; i++) {
					res.AppendFormat ("{0}. {1} ({2})\n", i + 1, results [i].Name, results [i].Set);
				}
			}
			return res.ToString ();
		}
	}
}

