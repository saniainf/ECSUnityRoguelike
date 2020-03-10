using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class EntitiesPresetsInject
    {
        public Dictionary<string, LevelTilePreset> LevelTiles;
        public PlayerPreset Player;
        public Dictionary<string, CollectingItemPreset> CollectingItems;

        public EntitiesPresetsInject(EntitiesPresets presets)
        {
            LevelTiles = new Dictionary<string, LevelTilePreset>();
            foreach (var p in presets.LevelTiles)
            {
                var key = p.GameObject.NameID;
                LevelTiles.Add(key, p);
            }

            Player = presets.Player;

            CollectingItems = new Dictionary<string, CollectingItemPreset>();
            foreach (var p in presets.CollectingItems)
            {
                var key = p.GameObject.NameID;
                CollectingItems.Add(key, p);
            }
        }
    }
}
