using System;
using MagicHelper_Bot.Service;
using TextCommands;
using System.Text;
using System.Collections.Generic;
using MagicHelper_Bot.Models;
using System.Linq;

namespace MagicHelper_Bot.Commands
{
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

