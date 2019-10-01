using Leopotam.Ecs;

namespace Client
{
    sealed class ActionAnimationComponent:IEcsAutoResetComponent
    {
        public ActionAnimation Animation = ActionAnimation.NONE;
        public bool Run = false;

        void IEcsAutoResetComponent.Reset()
        {
            Run = false;
        }
    }
}