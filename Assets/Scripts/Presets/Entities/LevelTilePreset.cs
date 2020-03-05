using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Entities/LevelTile", fileName = "LevelTilePreset")]
    public class LevelTilePreset : ScriptableObject
    {
        public GameObjectPreset GameObject;

        [Header("Entity components")]
        public bool Obstacle;
        public bool ExitPoint;
    }
}