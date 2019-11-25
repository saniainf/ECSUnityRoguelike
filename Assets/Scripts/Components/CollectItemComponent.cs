using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.Ecs;

namespace Client
{
    class CollectItemComponent : IEcsAutoReset
    {
        public ICollectItem CollectItem;

        void IEcsAutoReset.Reset()
        {
            CollectItem = null;
        }
    }
}
