using System;
using System.Collections.Generic;
using MagicHelper_Bot.Service;
using TextCommands;
using System.Text;
using MagicHelper_Bot.Models;
using System.Linq;

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

	public class PriceCommand : ExecutableCommand
	{
		readonly IProductService service;

		public PriceCommand (IProductService serv)
		{
			service = serv;

			Keyword = "price";
			Description = @"Searches for the price of a product on MagicCardMarket.eu
				-s, -single: get singles.
				-b, -booster: get boosters.
				-d, -display: get display boxes (boosterbox).
			TIP: a comma is an OR, use quotes to do an absolute search.";
		}

		public override string Execute (TextCommand cmd)
		{
			var res = new StringBuilder ();

			List<Product> results = service.SearchProduct (cmd.Params);
			List<Product> filteredResults = new List<Product> ();
			List<Product> activeList = results;

			if (cmd.Flags.Contains ("s") || cmd.Flags.Contains ("single")) {
				filteredResults.AddRange (results.Where (p => p.Category.Equals ("Magic Single")));
				activeList = filteredResults;
			}
			if (cmd.Flags.Contains ("b") || cmd.Flags.Contains ("booster")) {
				filteredResults.AddRange (results.Where (p => (p.Category.Equals ("Magic Booster"))));
				activeList = filteredResults;
			}
			if (cmd.Flags.Contains ("d") || cmd.Flags.Contains ("display")) {
				filteredResults.AddRange (results.Where (p => p.Category.Equals ("Magic Display")));
				activeList = filteredResults;
			}
					
			if (activeList.Count == 0) {
				return "Couldn't find that card.";
			} else if (activeList.Count < 10) {
				foreach (var product in activeList) {
					res.AppendLine (product.Name + ":");
					res.AppendLine (product.Link);
					res.AppendLine (product.Pricing.ToString ());
				}
			} else {
				res.AppendLine ("Found " + activeList.Count + " results:");
				for (int i = 0; i < activeList.Count; i++) {
					res.AppendFormat ("{0}. {1}\n", i + 1, results [i].Name);
				}
			}

			return res.ToString ();
		}

	}
}

