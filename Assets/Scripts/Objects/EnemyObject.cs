using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/EnemyPreset", fileName = "EnemyPreset")]
    class EnemyObject : ScriptableObject
    {
        public RuntimeAnimatorController Animation;
        public int HealthPoint;
        public int Initiative;
        public WeaponItemObject WeaponItem;
    }
}
