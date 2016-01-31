using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace MagicHelper_Bot.Service.DeserializerClasses
{
	public class Game
	{
		public int IdGame { get; set; }

		public string Name { get; set; }
	}


	public class EnglishLangDetails
	{
		public int IdLanguage { get; set; }

		public string LanguageName { get; set; }

		public string ProductName { get; set; }
	}

	public class Name
	{
		[DeserializeAs (Name = "1")]
		public EnglishLangDetails English { get; set; }

		public override string ToString ()
		{
			return English != null ? English.ProductName : "???";
		}
	}

	public class Category
	{
		public int IdCategory { get; set; }

		public string CategoryName { get; set; }

		public override string ToString ()
		{
			return CategoryName;
		}
	}

	public class PriceGuide
	{
		public double SELL { get; set; }

		public double LOW { get; set; }

		public double LOWEX { get; set; }

		public double LOWFOIL { get; set; }

		public double AVG { get; set; }

		public double TREND { get; set; }

		public static explicit operator MagicHelper_Bot.Models.PriceData (PriceGuide p)  // explicit byte to digit conversion operator
		{
			MagicHelper_Bot.Models.PriceData result = new MagicHelper_Bot.Models.PriceData () {
				Sold_Avg = p.SELL,
				Low = p.LOW,
				Low_HighQuality = p.LOWEX,
				Low_Foil = p.LOWFOIL,
				Avg = p.AVG,
				Trend = p.TREND
			};

			return result;
		}
	}

	public class Product
	{
		public int IdProduct { get; set; }

		public int IdMetaproduct { get; set; }

		public int IdGame { get; set; }

		public string CountReprints { get; set; }

		public Name Name { get; set; }

		public string Website { get; set; }

		public string Image { get; set; }

		public Category Category { get; set; }

		public PriceGuide PriceGuide { get; set; }

		public string Expansion { get; set; }

		public int ExpIcon { get; set; }

		public string Number { get; set; }

		public string Rarity { get; set; }

		public static explicit operator MagicHelper_Bot.Models.Product (Product p)  // explicit byte to digit conversion operator
		{
			MagicHelper_Bot.Models.Product result = new MagicHelper_Bot.Models.Product () {

				ID = p.IdProduct,
				Link = "https://magiccardmarket.eu" + p.Website,
				Name = p.Name.English.ProductName,
				Set = new MagicHelper_Bot.Models.Set (){ Name = p.Expansion },
				Pricing = (MagicHelper_Bot.Models.PriceData)p.PriceGuide,
				Category = p.Category.CategoryName
			};

			return result;
		}
	}

	public class ProductQueryResult
	{
		[DeserializeAs (Name = "product")]
		public List<Product> Products { get; set; }
	}

	public class GameQueryResult
	{
		public List<Game> Games { get; set; }
	}
}

