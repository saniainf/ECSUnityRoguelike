using Leopotam.Ecs;

namespace Client
{
    sealed class ActionAnimationComponent:IEcsAutoReset
    {
        public AnimatorField Animation = AnimatorField.None;
        public bool Run = false;

        void IEcsAutoReset.Reset()
        {
            Run = false;
        }
    }
}