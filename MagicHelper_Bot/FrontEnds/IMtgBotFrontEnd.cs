using System;
using MagicHelper_Bot.Commands;
using System.Threading.Tasks;

namespace MagicHelper_Bot.FrontEnds
{
	public interface IMtgBotFrontEnd
	{
		event CommandEventHandler OnNewCommand;

		Task Initialize ();

		void Poll ();

		Task PushResponse (long identifier, string response);
	}
}

