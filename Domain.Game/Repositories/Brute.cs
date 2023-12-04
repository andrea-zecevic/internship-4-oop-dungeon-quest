using Domain.Game.Enums;
using System;

namespace Domain.Game.Repositories
{
    public class Brute : Monster
    {
        public Brute() : base(MonsterType.Brute)
        {
            Type = MonsterType.Brute;
        }
        protected override void GenerateAttributes()
        {
            Random random = new Random();

            HealthPoints = random.Next(50, 80);
            Damage = random.Next(8, 15); 
            ExperienceValue = random.Next(15, 25);
        }

        public override void Attack(Hero hero)
        {
            Random random = new Random();

            if (random.Next(1, 101) <= 30)
            {
                int percentage = random.Next(10, 21);
                int damage = (int)(hero.HealthPoints * (percentage / 100.0));
                Console.WriteLine($"{Name} napada heroja s jakim udarcem i oduzima mu {percentage}% života!");
                hero.HealthPoints -= damage;
            }
            else
            {
                Console.WriteLine($"{Name} napada heroja!");
                hero.HealthPoints -= Damage;
            }
        }
    }
}
