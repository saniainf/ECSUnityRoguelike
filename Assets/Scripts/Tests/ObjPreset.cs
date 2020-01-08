using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/ObjPresets", fileName = "ObjPreset")]
    class ObjPreset : ScriptableObject
    {
        public List<ProjectilePreset> ProjectilePresets;
    }
}