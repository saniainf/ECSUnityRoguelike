using UnityEngine;

namespace Client
{
    sealed class Wall
    {
        public bool Solid = false;
        public int HealthPoint = 3;
        public Sprite damageSprite;
    }
}