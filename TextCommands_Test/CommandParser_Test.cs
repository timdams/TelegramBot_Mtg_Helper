using System;
using NUnit.Framework;
using TextCommands;
using System.Collections.Generic;

namespace TextCommands_Test
{
	[TestFixture]
	public class CommandParser_Test
	{
		string input;

		[SetUp]
		public void Init ()
		{
			input = "";
		}

		[Test]
		public void ParseTestEmpty ()
		{
			var result = CommandParser.Parse (input);

			Assert.IsNull (result.Keyword);
			Assert.IsEmpty (result.Params);
			Assert.IsEmpty (result.Flags);
		}

		[Test]
		public void ParseTestSubjectOnly ()
		{
			input = "/card nyx-fleece ram";
			var result = CommandParser.Parse (input);

			Assert.AreEqual (result.Keyword, "card");
			Assert.Contains (new KeyValuePair<string,string> ("subject", "nyx-fleece ram"), result.Params);
			Assert.IsEmpty (result.Flags);
		}

		[Test]
		public void ParseTestFlagsOnly ()
		{
			input = "/price -f -t";
			var result = CommandParser.Parse (input);

			Assert.AreEqual (result.Keyword, "price");
			Assert.Contains ("f", result.Flags);
			Assert.Contains ("t", result.Flags);
			Assert.IsEmpty (result.Params);
		}

		[Test]
		public void ParseTestFull ()
		{
			input = "/set abc-xyz -p 2 -t";
			var result = CommandParser.Parse (input);

			Assert.AreEqual (result.Keyword, "set");
			Assert.Contains (new KeyValuePair<string,string> ("subject", "abc-xyz"), result.Params);
			Assert.Contains (new KeyValuePair<string, string> ("p", "2"), result.Params);
			Assert.Contains ("t", result.Flags);
		}

		[Test]
		public void ParseTestWeird ()
		{
			input = "/ card     nyx-fleece ram - - - -s jou ---set   jou   ";
			var result = CommandParser.Parse (input);

			Assert.AreEqual (result.Keyword, "card");
			Assert.Contains (new KeyValuePair<string, string> ("subject", "nyx-fleece ram"), result.Params);
			Assert.Contains (new KeyValuePair<string, string> ("s", "jou"), result.Params);
			Assert.Contains (new KeyValuePair<string, string> ("set", "jou"), result.Params);
			Assert.IsEmpty (result.Flags);
		}
	}
}

