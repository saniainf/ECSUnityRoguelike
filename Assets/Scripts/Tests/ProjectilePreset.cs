using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/ObjPresets/Projectile", fileName = "ProjectilePreset")]
    class ProjectilePreset : ScriptableObject
    {
        [SerializeField]
        private string idName;

        public string IDName { get { return idName; } }
    }
}
