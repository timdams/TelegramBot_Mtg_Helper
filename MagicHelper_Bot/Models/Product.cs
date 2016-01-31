using System;
using System.Collections.Generic;
using System.Text;

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
			var sb = new StringBuilder ();
			sb.AppendLine ("   Sold Avg: €" + Sold_Avg);

			if (Low > 0d)
				sb.AppendLine ("   Low: €" + Low);
			if (Low_HighQuality > 0d)
				sb.AppendLine ("   Low (HQ): €" + Low_HighQuality);
			if (Low_Foil > 0d)
				sb.AppendLine ("   Low (Foil): €" + Low_Foil);
			if (Avg > 0d)
				sb.AppendLine ("   Avg: €" + Avg);

			return sb.ToString ();
		}
	}
}

