using NUnit.Framework;
using System;
using TextCommands;
using System.Collections.Generic;

namespace TextCommands_Test
{
	[TestFixture]
	public class TextCommand_Test
	{
		TextCommand cmd;

		[SetUp]
		public void Init ()
		{
			cmd = new TextCommand ();
		}

		[Test]
		public void TestToStringDefault ()
		{
			string result = cmd.ToString ();
			string expected = ": [] []";
			Assert.AreEqual (result, expected);
		}

		[Test]
		public void TestToStringFilled ()
		{
			cmd.Keyword = "card";
			cmd.Params.Add ("set", "JOU");
			cmd.Params.Add ("p", "2");
			cmd.Flags.Add ("t");
			cmd.Flags.Add ("f");

			string result = cmd.ToString ();
			string expected = "card: [set=JOU,p=2] [t,f]";
			Assert.AreEqual (result, expected);
		}
	}
}

