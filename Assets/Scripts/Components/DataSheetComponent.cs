using Leopotam.Ecs;

namespace Client
{
    sealed class DataSheetComponent:IEcsAutoResetComponent
    {
        public int MaxHealthPoint = 0;
        public int HitDamage = 0;
        public int Initiative = 0;

        public int HealthPoint = 0;

        void IEcsAutoResetComponent.Reset()
        {
            MaxHealthPoint = 666;
            HitDamage = 666;
            Initiative = 666;
            HealthPoint = 666;
        }
    }
}