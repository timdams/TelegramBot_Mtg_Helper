using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram_MagicHelper_Bot.Service;
using Telegram_MagicHelper_Bot.Commands;
using System.Collections.Generic;
using Telegram_MagicHelper_Bot.FrontEnds;
using TextCommands;
using System.Linq;
using System.Threading;

namespace Telegram_MagicHelper_Bot
{
	class MainClass
	{
		static IMagicService CardService;
		static List<ExecutableCommand> ExecutableCommands;
		static List<IMtgBotFrontEnd> FrontEnds;

		static void Main (string[] args)
		{
			Initialize ();

			Parallel.ForEach (FrontEnds, f => {
				f.Initialize ().Wait ();
				f.OnNewCommand += FrontEnd_OnNewCommand;
				Console.WriteLine ("Frontend initialized");
				while (true) {
					f.Poll ();
					Thread.Sleep (2000); // Don't overdo requests.
				}
			});
		}

		static void Initialize ()
		{
			CardService = new MagicTheGathering_IO ();
			ExecutableCommands = new List<ExecutableCommand> () {
				new StartCommand (),
				new CardCommand (CardService),
				new HelpCommand ()
			};
			(ExecutableCommands.First (c => c is HelpCommand) as HelpCommand).BuildHelp (ExecutableCommands);
			FrontEnds = new List<IMtgBotFrontEnd> () {
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
