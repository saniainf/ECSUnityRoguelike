using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameObjectComponent : IEcsAutoResetComponent
    {
        //TODO сделать компонент для GameObject с кэшем всех этих компонентов
        public Transform Transform = null;
        public PrefabComponentsShortcut Link = null;

        //public Rigidbody2D Rigidbody = null;
        //public BoxCollider2D Collider = null;
        //public SpriteRenderer SpriteRenderer = null;
        //public Animator Animator = null;

        void IEcsAutoResetComponent.Reset()
        {
            Link = null;

            Transform = null;
            //Rigidbody = null;
            //Collider = null;
            //SpriteRenderer = null;
            //Animator = null;
        }
    }
}