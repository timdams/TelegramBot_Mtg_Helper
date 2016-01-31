using System;

namespace MagicHelper_Bot.Models
{
	public class Ruling
	{
		public string Date { get; set; }

		public string Text { get; set; }

		public override string ToString ()
		{
			return "- " + Text;
		}
	}
}

