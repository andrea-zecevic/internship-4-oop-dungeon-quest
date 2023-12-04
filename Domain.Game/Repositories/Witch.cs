using Domain.Game.Enums;
using System;

namespace Domain.Game.Repositories
{
    public class Witch : Monster
    {
        private bool isHexed;

        public Witch() : base(MonsterType.Witch)
        {
            Type = MonsterType.Witch;
        }

        protected override void GenerateAttributes()
        {
            Random random = new Random();

            HealthPoints = random.Next(70, 100);
            Damage = random.Next(10, 20);
            ExperienceValue = random.Next(25, 35);
        }

        public override void Attack(Hero hero)
        {
            if (isHexed)
            {
                Random random = new Random();
                int newHealth = random.Next(1, 101);
                Console.WriteLine($"{Name} baca dumbus! Svi sudionici sada imaju {newHealth} zivotnih bodova!");
                hero.HealthPoints = newHealth;
                isHexed = false;
            }
            else
            {
                Console.WriteLine($"{Name} napada heroja!");
                hero.HealthPoints -= Damage;
            }
        }

        public override void OnDefeat()
        {
            base.OnDefeat();
            if (!isHexed)
            {
                Console.WriteLine($"{Name} je poražena! Stvaraju se 2 nova čudovišta.");
            }
        }

        public void CastHex()
        {
            Console.WriteLine($"{Name} baca dumbus.");
            isHexed = true;
        }
    }
}
