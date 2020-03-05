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

        public EntitiesPresetsInject(EntitiesPresets presets)
        {
            LevelTiles = new Dictionary<string, LevelTilePreset>();

            foreach (var p in presets.LevelTiles)
            {
                var key = p.GameObject.MapChar != ' ' ? p.GameObject.MapChar.ToString() : p.GameObject.Name;
                LevelTiles.Add(key, p);
            }
        }
    }
}
