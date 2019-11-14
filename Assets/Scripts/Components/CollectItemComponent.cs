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
        void OnCollect(DataSheetComponent dataSheet);
    }

    class CollectItemHeal : ICollectItem
    {
        private int value;

        public CollectItemHeal(int value)
        {
            this.value = value;
        }

        void ICollectItem.OnCollect(DataSheetComponent dataSheet)
        {
            dataSheet.CurrentHealthPoint = Math.Min(dataSheet.CurrentHealthPoint + value, dataSheet.HealthPoint);
        }
    }

    class CollectItemBoostHP : ICollectItem
    {
        private int value;

        public CollectItemBoostHP(int value)
        {
            this.value = value;
        }

        void ICollectItem.OnCollect(DataSheetComponent dataSheet)
        {
            dataSheet.CurrentHealthPoint += value;
            dataSheet.HealthPoint += value;
        }
    }
}
