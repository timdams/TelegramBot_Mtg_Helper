using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram_MagicHelper_Bot.Service;
using Telegram_MagicHelper_Bot.Commands;

namespace Telegram_MagicHelper_Bot
{
	class MainClass
	{
		static void Main (string[] args)
		{
			Run ().Wait ();
		}

		static async Task Run ()
		{
			var bot = new Api (APIToken.Token);
			var me = await bot.GetMe ();
			IMagicService service = new MagicTheGathering_IO ();
			CmdCollection.InitializeCommands (service);

			Console.WriteLine ("Hello my name is {0}", me.Username);

			var offset = 0;

			while (true) {
				var updates = await bot.GetUpdates (offset);

				foreach (var update in updates) {
					if (update.Message.Type == MessageType.TextMessage) {
						var currentCmd = CommandParser.Parse (update.Message.Text);
						ConcreteCommand cmd = CmdCollection.All.Find (c => c.Keyword.Equals (currentCmd.Keyword, 
							                      StringComparison.InvariantCultureIgnoreCase));
						string response = cmd != null ? cmd.Execute (currentCmd) : "Couldn't find the right command.";
						await bot.SendTextMessage (update.Message.Chat.Id, response);
					}
					offset = update.Id + 1;
				}
				await Task.Delay (1000);
			}
		}
	}
}
