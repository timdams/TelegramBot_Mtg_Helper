using System.Collections.Generic;
using System.Text;

namespace MagicHelper_Bot.Models
{
	public class Card
	{
		public string Name { get; set; }

		public string ManaCost { get; set; }

		public int Cmc { get; set; }

		public List<string> Colors { get; set; }

		public string Type { get; set; }

		public List<string> Supertypes { get; set; }

		public List<string> Types { get; set; }

		public List<string> Subtypes { get; set; }

		public string Rarity { get; set; }

		public string Set { get; set; }

		public string Text { get; set; }

		public string Artist { get; set; }

		public string Number { get; set; }

		public string Power { get; set; }

		public string Toughness { get; set; }

		public string Layout { get; set; }

		public string ReleaseDate { get; set; }

		public List<Ruling> Rulings { get; set; }

		public List<ForeignName> ForeignNames { get; set; }

		public List<string> Printings { get; set; }

		public List<Legality> Legalities { get; set; }

		public string Source { get; set; }

		public string Id { get; set; }

		public int? Multiverseid { get; set; }

		public string ImageUrl { get; set; }

		public string OriginalText { get; set; }

		public string OriginalType { get; set; }

		public string Watermark { get; set; }

		public string Flavor { get; set; }

		public override string ToString ()
		{
			var str = new StringBuilder ();
			str.AppendLine (Name + " - " + ManaCost);
			str.AppendLine (Type);
			if (Power != null && Toughness != null)
				str.AppendLine (Power + "/" + Toughness);
			str.AppendLine (Text);
			if (Flavor != null)
				str.AppendLine ("<" + Flavor + ">");
			return str.ToString ();
		}
	}

	public class Ruling
	{
		public string Date { get; set; }

		public string Text { get; set; }

		public override string ToString ()
		{
			return "- " + Text;
		}
	}

	public class ForeignName
	{
		public string Name { get; set; }

		public string Language { get; set; }

		public int? Multiverseid { get; set; }
	}

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



