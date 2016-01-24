﻿using System;
using RestSharp;
using System.Collections.Generic;

namespace Telegram_MagicHelper_Bot.Service
{
	/// <summary>
	/// Interface that represents a service that can be used to fetch MTG card data.
	/// </summary>
	public interface IMagicService
	{
		/// <summary>
		/// Searches a magic card with the parameters described in the dictionary. E.g.: {"Name" : "Nyx-Fleece Ram"}
		/// </summary>
		/// <returns>The result of the search as a string, to be printed in Telegram.</returns>
		/// <param name="queries">A dictionary of parameters you search for in a card. E.g: "Name" - "Nyx-Fleece"</param>
		string SearchCard (Dictionary<string,string> queries);
	}
}

