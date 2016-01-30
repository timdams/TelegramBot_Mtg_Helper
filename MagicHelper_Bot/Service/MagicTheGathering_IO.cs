using System;
using RestSharp;
using System.Linq;
using System.Collections.Generic;

namespace MagicHelper_Bot.Service
{
	/// <summary>
	/// Implementation of IMagicService that uses MagicTheGathering.io as API.
	/// </summary>
	public class MagicTheGathering_IO : IMagicService
	{
		readonly RestClient client = new RestClient ("http://api.magicthegathering.io/v1/");

		public CardQueryResult SearchCard (IDictionary<string,string> searchParams)
		{
			RestRequest req = new RestRequest ("cards", Method.GET);

			foreach (var param in searchParams) {
				if (param.Key.Equals ("subject")) {
					req.AddParameter ("name", param.Value);
				} else if (param.Key.Equals ("s") || param.Key.Equals ("set")) {
					req.AddParameter ("set", param.Value);
				} else if (param.Key.Equals ("p") || param.Key.Equals ("page")) {
					req.AddParameter ("page", param.Value);
				} else if (param.Key.Equals ("c") || param.Key.Equals ("color")) {
					req.AddParameter ("colors", param.Value);
				}
			}

			return client.Execute<CardQueryResult> (req).Data;
		}
	}
}


