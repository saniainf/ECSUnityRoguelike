using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/GameObject", fileName = "GameObjectPreset")]
    public class GameObjectPreset : ScriptableObject
    {
        public char MapChar;
        public string Name;
        public SortingLayer SortingLayer;

        [Header("Game Object Resources")]
        public Sprite[] spritesArray;
        public RuntimeAnimatorController AnimatorController;

        [Header("Game Object Components")]
        public bool SpriteRenderer = true;
        public bool Animator;
        public bool Rigidbody;
        public bool Collider;
    }
}