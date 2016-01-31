using System;

namespace MagicHelper_Bot.Models
{
	public class Product
	{
		public string Name { get; set; }

		public string Category { get; set; }

		public Set Set { get; set; }

		public PriceData Pricing { get; set; }

		public int ID { get; set; }

		public string Link { get; set; }
	}
}

