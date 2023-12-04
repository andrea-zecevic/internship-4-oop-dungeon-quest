using Domain.Game.Enums;

namespace Domain.Game.Repositories
{
    public class Enchater : Hero
    {
        public static int DefaultMana { get; set; } = 50;

        private bool isRevived;

        public int Mana { get; set; }

        public Enchater(string name, int healthPoints = 70, int experience = 0, int damage = 30)
            : base(name, healthPoints, experience, damage, HeroType.Enchater)
        {
            Type = HeroType.Enchater;
            InitializeDefaultChances();
        }

        private void InitializeDefaultChances()
        {
            Mana = DefaultMana;
            isRevived = false;
        }

        public override void Attack(Monster monster)
        {
            if (Mana > 0)
            {
                Console.WriteLine($"{Name} napada čudovište sa svojim magičnim napadom!");
                monster.HealthPoints -= Damage;
                Mana--;
            }
            else
            {
                Console.WriteLine($"{Name} nema dovoljno mane za napad. Mana će se obnoviti sljedeću rundu.");
            }
        }

        public override void LevelUp()
        {
            HealthPoints += 10;
            Damage += 8;
            Mana += 20;
            Console.WriteLine($"{Name} je level-upao! Novi HP: {HealthPoints}, Novi Damage: {Damage}, Nova Mana: {Mana}");
        }

        public override void GainExperience(int gainedExperience)
        {
            Experience += gainedExperience;
            Console.WriteLine($"{Name} je povećao iskustvo. Trenutno iskustvo: {Experience}");

            if (Experience >= 100)
            {
                LevelUp();
                Experience %= 100;
            }
        }

        public void UseRevive()
        {
            if (!isRevived)
            {
                Console.WriteLine($"{Name} se vraća iz mrtvih!");
                HealthPoints = 70;
                isRevived = true;
            }
            else
            {
                Console.WriteLine($"{Name} već koristi sposobnost vraćanja iz mrtvih. Ne može ponovno koristiti.");
            }
        }

        public void RestoreMana()
        {
            Mana = DefaultMana; 
            Console.WriteLine($"{Name} je obnovio svoju manu.");
        }

        public void HealWithMana(int amount)
        {
            if (Mana >= amount)
            {
                HealthPoints += amount;
                Mana -= amount;
                Console.WriteLine($"{Name} je iskoristio manu da se izliječi za {amount} HP.");
            }
            else
            {
                Console.WriteLine($"{Name} nema dovoljno mane za liječenje.");
            }
        }
    }
}
