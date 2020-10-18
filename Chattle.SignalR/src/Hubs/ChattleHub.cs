using Microsoft.AspNetCore.SignalR;

namespace Chattle.SignalR
{
    public class ChattleHub : Hub
    {
        private Chattle _chattle;

        public ChattleHub(Chattle chattle)
        {
            _chattle = chattle;
        }
    }
}
