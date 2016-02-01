using RestSharp;
using System.Collections.Generic;
using MagicHelper_Bot.Models;

namespace MagicHelper_Bot.Service
{
	/// <summary>
	/// Implementation of IMagicService that uses MagicTheGathering.io as API.
	/// </summary>
	public class MagicTheGathering_IO : IMagicService
	{
		readonly RestClient client = new RestClient ("http://api.magicthegathering.io/v1");

		public List<Card> SearchCard (IDictionary<string,string> searchParams)
		{
			var req = new RestRequest ("cards", Method.GET);

			foreach (var param in searchParams) {
				if (param.Key.Equals ("subject")) {
					req.AddParameter ("name", param.Value);
				} else if (param.Key.Equals ("s") || param.Key.Equals ("set")) {
					req.AddParameter ("set", param.Value);
				} else if (param.Key.Equals ("p") || param.Key.Equals ("page")) {
					req.AddParameter ("page", param.Value);
				} else if (param.Key.Equals ("c") || param.Key.Equals ("color")) {
					req.AddParameter ("colors", param.Value);
				} else if (param.Key.Equals ("t") || param.Key.Equals ("type")) {
					req.AddParameter ("type", param.Value);
				} else if (param.Key.Equals ("a") || param.Key.Equals ("ability")) {
					req.AddParameter ("text", param.Value);
				}
			}

			return client.Execute<CardQueryResult> (req).Data.Cards;
		}
	}

	public class CardQueryResult
	{
		public List<Card> Cards { get; set; }
	}
}


