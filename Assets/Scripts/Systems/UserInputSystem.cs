using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class UserInputSystem : IEcsRunSystem
    {
        // Auto-injected fields.
        readonly EcsWorld _world = null;

        void IEcsRunSystem.Run()
        {
            // Add your run code here.
        }
    }
}