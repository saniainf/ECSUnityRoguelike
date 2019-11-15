using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.Ecs;

namespace Client
{
    class CollectItemComponent : IEcsAutoResetComponent
    {
        public ICollectItem CollectItem;

        void IEcsAutoResetComponent.Reset()
        {
            CollectItem = null;
        }
    }

    interface ICollectItem
    {
        void OnCollect(EcsWorld world, EcsEntity entity);
    }

    class CollectItemHeal : ICollectItem
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

    class CollectItemBoostHP : ICollectItem
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
