using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/WeaponItemPreset", fileName = "WeaponItemPreset")]
    class WeaponItemObject : ScriptableObject
    {
        public int Damage;
        public Sprite ProjectileSprite;
    }
}
