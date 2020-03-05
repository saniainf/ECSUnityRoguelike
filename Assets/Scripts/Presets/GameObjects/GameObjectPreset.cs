using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/GameObjects/GameObject", fileName = "GameObjectPreset")]
    public class GameObjectPreset : ScriptableObject
    {
        public string NameID;
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