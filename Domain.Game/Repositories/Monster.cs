using Domain.Game.Enums;
using System;

namespace Domain.Game.Repositories
{
    public abstract class Monster
    {
        public MonsterType Name { get; set; }
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
        public int ExperienceValue { get; set; }
        public MonsterType Type { get; set; }

        public bool IsStunned { get; set; }

        protected Monster(MonsterType type)
        {
            Name = type;
            Type = type;
            GenerateAttributes();
        }
        protected abstract void GenerateAttributes();
        public abstract void Attack(Hero hero);
        public virtual void OnDefeat()
        {
            Console.WriteLine($"{Name} je porazen! Dobivate {ExperienceValue} iskustva.");
        }
        public void Stun()
        {
            IsStunned = true;
        }

        public void RemoveStun()
        {
            IsStunned = false;
        }
    }
}
