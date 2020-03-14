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
        /*
         * может быть 
         * item use
         * item
         * spell
         * */
        public SpellPreset Spell;

        void IEcsAutoReset.Reset()
        {
            Spell = null;
        }
    }
}
