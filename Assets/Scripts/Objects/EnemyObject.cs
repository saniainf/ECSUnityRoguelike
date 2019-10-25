using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/EnemyPreset", fileName = "EnemyPreset")]
    public class EnemyObject : ScriptableObject
    {
        public RuntimeAnimatorController Animation;
        public int HealthPoint;
        public int HitDamage;
        public int Initiative;
    }
}
