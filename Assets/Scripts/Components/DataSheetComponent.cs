using Leopotam.Ecs;

namespace Client
{
    sealed class DataSheetComponent:IEcsAutoResetComponent
    {
        public int HealthPoint = 0;
        public int HitDamage = 0;
        public int Initiative = 0;

        public int CurrentHealthPoint = 0;

        void IEcsAutoResetComponent.Reset()
        {
            HealthPoint = 666;
            HitDamage = 666;
            Initiative = 666;
            CurrentHealthPoint = 666;
        }
    }
}