using System;
using Telegram_MagicHelper_Bot.Commands;
using System.Threading.Tasks;

namespace Telegram_MagicHelper_Bot.FrontEnds
{
	public interface IMtgBotFrontEnd
	{
		event CommandEventHandler OnNewCommand;

		Task Initialize ();

		void Poll ();

		Task PushResponse (long identifier, string response);
	}
}

