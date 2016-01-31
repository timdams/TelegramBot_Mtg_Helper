using RestSharp;
using System.Collections.Generic;
using MagicHelper_Bot.Service.DeserializerClasses;
using MagicHelper_Bot.Service.Helpers;


namespace MagicHelper_Bot.Service
{
	public class MagicCardMarket_EU_v1_1 : IProductService
	{
		readonly RestClient client = new RestClient ("https://www.mkmapi.eu/ws/v1.1/output.json");

		const int MtgGameId = 1;
		const int EnglishLanguageId = 1;

		public List<MagicHelper_Bot.Models.Product> SearchProduct (IDictionary<string,string> searchParams)
		{			
			var req = new RestRequest ("products/{name}/{idGame}/{idLanguage}/{isExact}", Method.GET);

			foreach (var param in searchParams) {
				if (param.Key.Equals ("subject")) {
					req.AddUrlSegment ("name", param.Value);
				}
			}

			req.AddUrlSegment ("idGame", MtgGameId.ToString ());
			req.AddUrlSegment ("idLanguage", EnglishLanguageId.ToString ());
			req.AddUrlSegment ("isExact", "false");

			AddAuthorizationHeader (req);

			var result = client.Execute<ProductQueryResult> (req);
			if (result.StatusCode != System.Net.HttpStatusCode.OK) {
				System.Console.WriteLine ("Mcm request failed: " + result.StatusCode + ": " + result.StatusDescription);
			}

			var casted = new List<MagicHelper_Bot.Models.Product> ();
			foreach (Product product in result.Data.Products) {
				casted.Add ((MagicHelper_Bot.Models.Product)product);
			}

			return casted;
		}
			
		/*
		int GetMtgGameId ()
		{
			RestRequest req = new RestRequest ("games", Method.GET);
			AddAuthorizationHeader (req);
			var gameResults = client.Execute<GameQueryResult> (req).Data;
			var mtg = gameResults.Game.FirstOrDefault (g => g.Name.Equals ("Magic the Gathering"));
			return mtg.IdGame;
		}
		*/

		/// <summary>
		/// The default OAuth1 Authenticator doesn't work for Mcm because it doesn't send 'realm' or the empty access token.
		/// </summary>
		/// <param name="req">Req.</param>
		void AddAuthorizationHeader (IRestRequest req)
		{
			var header = new OAuthHeader ();
			string fullUrl = client.BuildUri (req).AbsoluteUri;
			req.AddHeader ("Authorization", header.getAuthorizationHeader (req.Method.ToString (), fullUrl));
		}

	}
}

