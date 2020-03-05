using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Entities/Player", fileName = "PlayerPreset")]
    class PlayerPreset : ScriptableObject
    {
        public GameObjectPreset GameObject;

        [Header("Preferences")]
        public int HealthPoint;
        public int Initiative;
        public WeaponItemObject PrimaryWeaponItem;
        public WeaponItemObject SecondaryWeaponItem;
    }
}
