using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/PlayerPresets", fileName = "PlayerPreset")]
    public class PlayerObject : ScriptableObject
    {
        public RuntimeAnimatorController Animation;
        public int HealthPoint;
        public int HitDamage;
        public int Initiative;
    }
}
