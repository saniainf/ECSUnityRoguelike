using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/PlayerPreset", fileName = "PlayerPreset")]
    class PlayerObject : ScriptableObject
    {
        public RuntimeAnimatorController Animation;
        public int HealthPoint;
        public int Initiative;
        public WeaponItemObject PrimaryWeaponItem;
        public WeaponItemObject SecondaryWeaponItem;
    }
}
