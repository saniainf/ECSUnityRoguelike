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
}
