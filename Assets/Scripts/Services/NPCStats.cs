﻿namespace Client
{
    /// <summary>
    /// основные статы чара
    /// </summary>
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

        public NPCStats(EnemyPreset enemyPreset)
        {
            MaxHealthPoint = enemyPreset.HealthPoint;
            HealthPoint = enemyPreset.HealthPoint;
            Initiative = enemyPreset.Initiative;
        }
    }
}
