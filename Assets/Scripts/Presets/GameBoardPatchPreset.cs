using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/GameBoardPatch", fileName = "GameBoardPatchPreset")]
    public class GameBoardPatchPreset : ScriptableObject
    {
        public char MapChar;
        public SortingLayer SortingLayer;

        [Header("Game Object Resources")]
        public Sprite[] spritesArray;
        public Sprite spriteSingle;
        public RuntimeAnimatorController AnimatorController;

        [Header("Game Object Components")]
        public bool SpriteRenderer = true;
        public bool Animator;
        public bool Rigidbody;
        public bool Collider;
    }
}