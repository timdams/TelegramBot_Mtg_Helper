using System;
using MagicHelper_Bot.Service;
using TextCommands;
using System.Text;
using System.Collections.Generic;
using MagicHelper_Bot.Models;

namespace MagicHelper_Bot.Commands
{
	public class CardCommand : ExecutableCommand
	{
		readonly IMagicService service;

		public CardCommand (IMagicService serv)
		{
			Keyword = "card";
			Description = @"Searches for a card. Options:
				-s, -set: define set(s) by code (GTC, OGW, ...)
				     Multiple sets split by comma's
				-c, -color: define color(s) (white, black, ...)
				     white,black = white AND black.
				     [white,black] = white OR black OR both.
				-t, -type: define type(s) (legendary, land, dragon, ...)
				-a, -ability: search the card's ability text. (vigilance, extort, ...)
				-p, -page: define the page of results.
				-L, -Legal: get the legality of the card.
				-R, -Rules: get the special rulings of the card.
				-F, -First: force first result.
				-T, -Text: get the card as text.";
			      
			service = serv;
		}

		public override string Execute (TextCommand cmd)
		{
			var res = new StringBuilder ();

			List<Card> results = service.SearchCard (cmd.Params);

			if (results.Count == 0)
				return "Couldn't find that card.";
			else if (results.Count == 1 || cmd.Flags.Contains ("F") || cmd.Flags.Contains ("First")) {
				Card card = results [0];
				if (card.ImageUrl == null || cmd.Flags.Contains ("T") || cmd.Flags.Contains ("Text")) {
					res.AppendLine (card.ToString ());
				} else {
					res.AppendLine (card.ImageUrl);
				}
				if (cmd.Flags.Contains ("L") || cmd.Flags.Contains ("Legal")) {
					res.AppendLine ("Legal in:");
					foreach (var l in card.Legalities) {
						res.AppendLine (l.ToString ());
					}
				}
				if (cmd.Flags.Contains ("R") || cmd.Flags.Contains ("Rules")) {
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

