﻿using System;
using System.Threading.Tasks;
using MagicHelper_Bot.Service;
using MagicHelper_Bot.Commands;
using System.Collections.Generic;
using MagicHelper_Bot.FrontEnds;
using TextCommands;
using System.Linq;
using System.Threading;

namespace MagicHelper_Bot
{
	class MainClass
	{
		static IMagicService CardService;
		static IProductService ProductService;
		static List<ExecutableCommand> ExecutableCommands;
		static List<IMtgBotFrontEnd> FrontEnds;

		static void Main (string[] args)
		{
			Initialize ();

			Parallel.ForEach (FrontEnds, f => {
				f.Initialize ().Wait ();
				f.OnNewCommand += FrontEnd_OnNewCommand;
				while (true) {
					f.Poll ();
					Thread.Sleep (2000); // Don't overdo requests.
				}
			});
		}

		static void Initialize ()
		{
			CardService = new MagicTheGathering_IO ();
			ProductService = new MagicCardMarket_EU_v1_1 ();

			ExecutableCommands = new List<ExecutableCommand> {
				new StartCommand (),
				new HelpCommand (),
				new CardCommand (CardService),
				new PriceCommand (ProductService),
			};
			(ExecutableCommands.First (c => c is HelpCommand) as HelpCommand).BuildHelp (ExecutableCommands);

			FrontEnds = new List<IMtgBotFrontEnd> {
				new TelegramBot ()
			};
		}

		static void FrontEnd_OnNewCommand (object sender, CommandEventArgs e)
		{
			var parsedCmd = CommandParser.Parse (e.Command);
			Console.WriteLine ("Cmd " + parsedCmd);
			var cmdToExecute = ExecutableCommands.Find (c => c.Keyword.Equals (parsedCmd.Keyword, 
				                   StringComparison.InvariantCultureIgnoreCase));
			if (cmdToExecute != null) {
				(sender as IMtgBotFrontEnd).PushResponse (e.Identifier, cmdToExecute.Execute (parsedCmd));
			}
		}
	}
}
