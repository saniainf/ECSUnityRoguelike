using Leopotam.Ecs;

namespace Client
{
    interface ICollectItem
    {
        void OnCollect(EcsEntity entity);
    }

    struct CollectItemHeal : ICollectItem
    {
        private int value;

        public CollectItemHeal(int value)
        {
            this.value = value;
        }

        void ICollectItem.OnCollect(EcsEntity entity)
        {
            entity.RLSetHealth(entity.RLGetHealth() + value);
        }
    }

    struct CollectItemBoostHP : ICollectItem
    {
        private int value;

        public CollectItemBoostHP(int value)
        {
            this.value = value;
        }

        void ICollectItem.OnCollect(EcsEntity entity)
        {
            entity.RLSetMaxHealth(entity.RLGetMaxHealth() + value);
            entity.RLSetHealth(entity.RLGetHealth() + value);

        }
    }
}
