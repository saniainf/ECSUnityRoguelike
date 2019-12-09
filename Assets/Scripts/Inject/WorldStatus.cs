using UnityEngine;

namespace Client
{
    class WorldStatus
    {

        public GameStatus GameStatus = GameStatus.None;
        public int LevelNum = 0;

        public bool PlayerTurn { get; private set; }

        public void PlayerTurnSet(bool value)
        {
            PlayerTurn = value;
        }
    }
}
