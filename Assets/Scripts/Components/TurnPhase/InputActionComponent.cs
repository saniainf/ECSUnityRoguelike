using Leopotam.Ecs;

namespace Client
{
    sealed class InputActionComponent:IEcsAutoResetComponent
    {
        public InputType InputAction = InputType.None;
        public MoveDirection MoveDirection = MoveDirection.None;

        void IEcsAutoResetComponent.Reset()
        {
            InputAction = InputType.None;
            MoveDirection = MoveDirection.None;
        }
    }
}