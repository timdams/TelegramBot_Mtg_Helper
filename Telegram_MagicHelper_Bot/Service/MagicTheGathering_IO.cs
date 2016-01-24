using System;
using RestSharp;
using System.Linq;
using System.Collections.Generic;

namespace Telegram_MagicHelper_Bot.Service
{
	/// <summary>
	/// Implementation of IMagicService that uses MagicTheGathering.io as API.
	/// </summary>
	public class MagicTheGathering_IO : IMagicService
	{
		readonly RestClient client = new RestClient ("http://api.magicthegathering.io/v1/");

		public string SearchCard (Dictionary<string,string> queries)
		{
			RestRequest req = new RestRequest ("cards", Method.GET);
			foreach (var q in queries) {
				req.AddParameter (q.Key, q.Value);
			}
			var response = client.Execute<RootObject> (req);
				
			int n_results = response.Data.Cards.Count;

			if (n_results == 0)
				return "Couldn't find that card.";
			else if (n_results == 1 && response.Data.Cards [0].ImageUrl != null)
				return response.Data.Cards.First ().ImageUrl;
			else {
				string result = String.Format ("Found {0} results:\n", n_results);
				int maxItems = n_results > 25 ? 25 : n_results;
				for (int i = 0; i < maxItems; i++) {
					result += String.Format ("{0}. {1} ({2})\n", i + 1, response.Data.Cards [i].Name, response.Data.Cards [i].Set);
				}
				return result;
			}
		}
	}
}


