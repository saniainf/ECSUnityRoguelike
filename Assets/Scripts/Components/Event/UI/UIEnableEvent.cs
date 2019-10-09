using Leopotam.Ecs;

namespace Client
{
    [EcsOneFrame]
    sealed class UIEnableEvent
    {
        public UIType UIType = UIType.None;
        public int LevelNumber = 0;
    }
}
