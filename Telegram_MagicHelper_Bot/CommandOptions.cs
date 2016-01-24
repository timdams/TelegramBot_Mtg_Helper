using System;
using CommandLine;
using CommandLine.Text;

namespace Telegram_MagicHelper_Bot
{
	public class CommandOptions
	{
		[Option ('s', "set", HelpText = "The set to search in.")]
		public string Set { get; set; }

		[Option ('v', "verbose", DefaultValue = true, HelpText = "Prints all messages to standard output.")]
		public bool Verbose { get; set; }

		[ParserState]
		public IParserState LastParserState { get; set; }

		[HelpOption]
		public string GetUsage ()
		{
			return HelpText.AutoBuild (this, (HelpText current) => HelpText.DefaultParsingErrorsHandler (this, current));
		}
	}
}

