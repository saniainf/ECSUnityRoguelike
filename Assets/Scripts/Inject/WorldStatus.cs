using UnityEngine;

namespace Client
{
    class WorldStatus
    {
        public GameStatus GameStatus = GameStatus.None;
        public int LevelNum = 0;
        public Transform ParentOtherObject;
    }
}
