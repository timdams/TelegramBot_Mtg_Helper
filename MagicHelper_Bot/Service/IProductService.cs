using System;
using System.Collections.Generic;
using MagicHelper_Bot.Models;

namespace MagicHelper_Bot.Service
{
	public interface IProductService
	{
		List<Product> SearchProduct (IDictionary<string, string> searchParams);
	}
}

