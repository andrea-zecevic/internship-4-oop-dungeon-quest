using Domain.Game.Enums;
namespace Domain.Game.Repositories
{
    public class Gladiator : Hero
    {
        public bool IsRageMode { get; set; }
        public int RagePoints { get; set; }

        public Gladiator(string name, int healthPoints = 100, int experience = 0, int damage = 20)
       : base(name, healthPoints, experience, damage, HeroType.Gladiator)
        {
            Type = HeroType.Gladiator;
            Name = name;
            IsRageMode = false; 
        }


        public void ActivateRage()
        {
            if (RagePoints >= 15)
            {
                IsRageMode = true;
                Console.WriteLine($"{Name} je ušao u mod bijesa!");
                RagePoints -= 15;
            }
            else
            {
                Console.WriteLine($"{Name} nema dovoljno Rage bodova za aktivaciju bijesa.");
            }
        }

        private void ApplyRageDamage()
        {
            if (IsRageMode)
            {
                Damage *= 2;
                Console.WriteLine($"{Name} nanosi duplu stetu u modu bijesa!");
            }
        }

        public override void Attack(Monster monster)
        {
            ApplyRageDamage();

            int gainedExperience = monster.ExperienceValue;
            GainExperience(gainedExperience);
        }

        public void IncreaseRage(int points)
        {
            RagePoints += points;
            Console.WriteLine($"{Name} dobiva {points} Rage bodova!");
        }

    public override void LevelUp()
        {
            HealthPoints += 15;
            Damage += 5;
            Console.WriteLine($"{Name} je level-upao! Novi HP: {HealthPoints}, Novi Damage: {Damage}");
        }

        public override void GainExperience(int gainedExperience)
        {
            Experience += gainedExperience;
            Console.WriteLine($"{Name} je povecao iskustvo. Trenutno iskustvo: {Experience}");

            if (Experience >= 100)
            {
                LevelUp();
                Experience %= 100;
            }
        }
    }
}
