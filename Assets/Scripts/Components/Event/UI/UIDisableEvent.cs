using Leopotam.Ecs;

namespace Client
{
    [EcsOneFrame]
    sealed class UIDisableEvent
    {
        public UIType UIType = UIType.None;
    }
}
