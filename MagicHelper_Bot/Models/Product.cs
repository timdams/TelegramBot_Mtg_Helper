using System;
using System.Collections.Generic;

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

	public class PriceData
	{
		public double Sold_Avg { get; set; }

		public double Low { get; set; }

		public double Low_HighQuality { get; set; }

		public double Low_Foil { get; set; }

		public double Avg { get; set; }

		public double Trend { get; set; }

		public override string ToString ()
		{
			return String.Format (
				@"Sold Avg: €{0}
				Current Avg: €{1}
				Low: €{2}
				Low (HQ): €{3}
				Low (Foil): €{4}
				Trend: €{5}",
				Sold_Avg, Avg, Low, Low_HighQuality, Low_Foil, Trend);
		}
	}
}

