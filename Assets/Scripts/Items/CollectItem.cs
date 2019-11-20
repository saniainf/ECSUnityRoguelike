using Leopotam.Ecs;

namespace Client
{
    interface ICollectItem
    {
        void OnCollect(EcsWorld world, EcsEntity entity);
    }

    struct CollectItemHeal : ICollectItem
    {
        private int value;

        public CollectItemHeal(int value)
        {
            this.value = value;
        }

        void ICollectItem.OnCollect(EcsWorld world, EcsEntity entity)
        {
            world.RLSetHealth(entity, world.RLGetHealth(entity) + value);
        }
    }

    struct CollectItemBoostHP : ICollectItem
    {
        private int value;

        public CollectItemBoostHP(int value)
        {
            this.value = value;
        }

        void ICollectItem.OnCollect(EcsWorld world, EcsEntity entity)
        {
            world.RLSetMaxHealth(entity, world.RLGetMaxHealth(entity) + value);
            world.RLSetHealth(entity, world.RLGetHealth(entity) + value);

        }
    }
}
