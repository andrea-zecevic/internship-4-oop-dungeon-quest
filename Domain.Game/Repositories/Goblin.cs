using Domain.Game.Enums;
using System;

namespace Domain.Game.Repositories
{
    public class Goblin : Monster
    {
        public Goblin() : base(MonsterType.Goblin)
        {
            Type = MonsterType.Goblin;
        }

        protected override void GenerateAttributes()
        {
            Random random = new Random();

            HealthPoints = random.Next(20, 40); 
            Damage = random.Next(3, 8);
            ExperienceValue = random.Next(5, 15); 
        }

        public override void Attack(Hero hero)
        {
            Console.WriteLine($"{Name} napada heroja!");
            hero.HealthPoints -= Damage;
        }


    }
}
