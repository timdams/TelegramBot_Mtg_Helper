using System;
using Telegram_MagicHelper_Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace Telegram_MagicHelper_Bot.FrontEnds
{
	public class TelegramBot : IMtgBotFrontEnd
	{
		int _offset;
		readonly Api _bot;
		const int maxMsgLength = 50;
		User me;

		string _myMention { get { return '@' + me.Username; } }

		public event CommandEventHandler OnNewCommand;

		public TelegramBot ()
		{
			_bot = new Api (APIToken.Telegram);
		}

		async public Task Initialize ()
		{
			me = await _bot.GetMe ();
		}

		async public void Poll ()
		{
			var updates = await _bot.GetUpdates (_offset);

			foreach (var update in updates) {
				if (update.Message.Type == MessageType.TextMessage && OnNewCommand != null) {
					string cleanCommand;
					bool MsgForMe = AmIMentioned (update.Message.Text, out cleanCommand);
					OnNewCommand (this, new CommandEventArgs (update.Message.Chat.Id, cleanCommand));
				}
				_offset = update.Id + 1;
			}
		}

		bool AmIMentioned (string input, out string cleaned)
		{
			int index = input.IndexOf (_myMention);
			cleaned = (index < 0)
				? input
				: input.Remove (index, _myMention.Length);
			return (index >= 0);
		}

		public Task PushResponse (long identifier, string response)
		{
			int numLines = response.Split ('\n').Length;
			if (numLines > maxMsgLength) {
				
			}
			return _bot.SendTextMessage (identifier, response);
		}
	}
}

