using System;

namespace MagicHelper_Bot.Models
{
	public class Legality
	{
		public string Format { get; set; }

		public string Legal { get; set; }

		public override string ToString ()
		{
			if (Legal == null)
				return Format;
			return $"{Format}: {Legal}";
		}
	}
}

