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

		public RootObject SearchCard (Dictionary<string,string> queries)
		{
			RestRequest req = new RestRequest ("cards", Method.GET);
			foreach (var q in queries) {
				req.AddParameter (q.Key, q.Value);
			}
			return client.Execute<RootObject> (req).Data;
		}
	}
}


