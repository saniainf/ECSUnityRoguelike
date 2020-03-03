using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/WallsPreset", fileName = "WallsPreset")]
    class WallsObject : ScriptableObject
    {
        public RuntimeAnimatorController[] Animation;
    }
}
