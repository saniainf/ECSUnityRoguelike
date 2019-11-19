namespace Client
{
    struct NPCStats
    {
        public int MaxHealthPoint;
        public int HealthPoint;

        public int Initiative;

        public NPCStats(int maxHP, int HP, int initiative)
        {
            MaxHealthPoint = maxHP;
            HealthPoint = HP;
            Initiative = initiative;
        }
    }
}
