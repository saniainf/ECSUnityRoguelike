using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public enum BuffType
    {
        Heal,
        Damage,
        DamageResist,
        AtackDamage
    }

    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Buff", fileName = "BuffPreset")]
    public class BuffPreset : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        [Space]
        public BuffType BuffType;
        public int Amount;
        [Space]
        public bool Brittle;
        public int BrittleAmount;
        public bool DecreaseOnHit;
        public int DecreaseOnHitAmount;
        [Space]
        public bool Stackable;
        public int StackSize;
        [Space]
        public bool UseTimer;
        public int Time;
        [Space]
        public bool Attacks;
        public int AttacksAmount;
        public bool DecreaseOnAtack;
        public int DecreaseOnAtackAmount;
    }

}