using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

public class Ruling
{

	[JsonProperty ("date")]
	public string Date { get; set; }

	[JsonProperty ("text")]
	public string Text { get; set; }

	public override string ToString ()
	{
		return "- " + Text;
	}
}

public class ForeignName
{

	[JsonProperty ("name")]
	public string Name { get; set; }

	[JsonProperty ("language")]
	public string Language { get; set; }

	[JsonProperty ("multiverseid")]
	public int? Multiverseid { get; set; }
}

public class Legality
{

	[JsonProperty ("format")]
	public string Format { get; set; }

	[JsonProperty ("legality")]
	public string Legal { get; set; }

	public override string ToString ()
	{
		if (Legal == null)
			return Format;
		return string.Format ("{0}: {1}", Format, Legal);
	}
}

public class Card
{

	[JsonProperty ("name")]
	public string Name { get; set; }

	[JsonProperty ("manaCost")]
	public string ManaCost { get; set; }

	[JsonProperty ("cmc")]
	public int Cmc { get; set; }

	[JsonProperty ("colors")]
	public List<string> Colors { get; set; }

	[JsonProperty ("type")]
	public string Type { get; set; }

	[JsonProperty ("supertypes")]
	public List<string> Supertypes { get; set; }

	[JsonProperty ("types")]
	public List<string> Types { get; set; }

	[JsonProperty ("subtypes")]
	public List<string> Subtypes { get; set; }

	[JsonProperty ("rarity")]
	public string Rarity { get; set; }

	[JsonProperty ("set")]
	public string Set { get; set; }

	[JsonProperty ("text")]
	public string Text { get; set; }

	[JsonProperty ("artist")]
	public string Artist { get; set; }

	[JsonProperty ("number")]
	public string Number { get; set; }

	[JsonProperty ("power")]
	public string Power { get; set; }

	[JsonProperty ("toughness")]
	public string Toughness { get; set; }

	[JsonProperty ("layout")]
	public string Layout { get; set; }

	[JsonProperty ("releaseDate")]
	public string ReleaseDate { get; set; }

	[JsonProperty ("rulings")]
	public List<Ruling> Rulings { get; set; }

	[JsonProperty ("foreignNames")]
	public List<ForeignName> ForeignNames { get; set; }

	[JsonProperty ("printings")]
	public List<string> Printings { get; set; }

	[JsonProperty ("legalities")]
	public List<Legality> Legalities { get; set; }

	[JsonProperty ("source")]
	public string Source { get; set; }

	[JsonProperty ("id")]
	public string Id { get; set; }

	[JsonProperty ("multiverseid")]
	public int? Multiverseid { get; set; }

	[JsonProperty ("imageUrl")]
	public string ImageUrl { get; set; }

	[JsonProperty ("originalText")]
	public string OriginalText { get; set; }

	[JsonProperty ("originalType")]
	public string OriginalType { get; set; }

	[JsonProperty ("watermark")]
	public string Watermark { get; set; }

	[JsonProperty ("flavor")]
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

public class RootObject
{

	[JsonProperty ("cards")]
	public List<Card> Cards { get; set; }
}


