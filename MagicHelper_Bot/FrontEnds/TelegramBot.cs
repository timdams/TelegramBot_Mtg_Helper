using System;
using MagicHelper_Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace MagicHelper_Bot.FrontEnds
{
	public class TelegramBot : IMtgBotFrontEnd
	{
		const int maxMsgLength = 50;
		int offset;

		readonly Api bot;
		User me;

		string myMention { get { return '@' + me.Username; } }

		public event CommandEventHandler OnNewCommand;

		public TelegramBot ()
		{
			bot = new Api (APIToken.Telegram);
		}

		async public Task Initialize ()
		{
			me = await bot.GetMe ();
			Console.WriteLine ("Telegram Frontend Initialized");
		}

		async public void Poll ()
		{
			var updates = await bot.GetUpdates (offset);

			foreach (var update in updates) {
				if (update.Message.Type == MessageType.TextMessage && OnNewCommand != null) {
					string cleanCommand;
					bool MsgForMe = AmIMentioned (update.Message.Text, out cleanCommand);
					OnNewCommand (this, new CommandEventArgs (update.Message.Chat.Id, cleanCommand));
				}
				offset = update.Id + 1;
			}
		}

		public Task PushResponse (long identifier, string response)
		{
			int numLines = response.Split ('\n').Length;
			if (numLines > maxMsgLength) {
				
			}
			return bot.SendTextMessage (identifier, response);
		}

		bool AmIMentioned (string input, out string cleaned)
		{
			int index = input.IndexOf (myMention);
			cleaned = (index < 0)
				? input
				: input.Remove (index, myMention.Length);
			return (index >= 0);
		}
	}
}

