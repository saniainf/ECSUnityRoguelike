using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Enemy", fileName = "EnemyPreset")]
    class EnemyPreset : ScriptableObject
    {
        public string PresetName;
        [Space]
        public RuntimeAnimatorController Animation;
        [Space]
        public int HealthPoint;
        public int Initiative;
        public WeaponItemObject PrimaryWeaponItem;
        public WeaponItemObject SecondaryWeaponItem;
    }
}
