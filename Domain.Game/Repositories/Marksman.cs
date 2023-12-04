using Domain.Game.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Game.Repositories
{
    public class Marksman : Hero
    {
        public int CriticalChance { get; set; }
        public int StunChance { get; set; }
        public Marksman(string name, int healthPoints = 90, int experience = 0, int damage = 25)
            : base(name, healthPoints, experience, damage, HeroType.Marksman)
        {
            Type = HeroType.Marksman;
            InitializeDefaultChances();
        }

        private void InitializeDefaultChances()
        {
            CriticalChance = 10;
            StunChance = 5; 
        }

        public override void Attack(Monster monster)
        {
            int damageDealt = Damage;

            if (RandomChance(CriticalChance))
            {
                damageDealt *= 2;
                Console.WriteLine($"{Name} je napravio kritični udarac!");
            }

            if (RandomChance(StunChance))
            {
                monster.Stun();
                Console.WriteLine($"{Name} je ošamutio čudovište! Čudovište gubi iduću rundu.");
                return;
            }

            if (!monster.IsStunned)
            {
                monster.HealthPoints -= damageDealt;
                Console.WriteLine($"{Name} napada čudovište! Nanio je {damageDealt} štete.");
            }
            else
            {
                Console.WriteLine($"{monster.Name} je ošamutio i ne može odgovoriti ovu rundu.");
                monster.RemoveStun();
            }
        }


        public override void LevelUp()
        {
            HealthPoints += 12;
            Damage += 6;
            CriticalChance += 3;
            StunChance += 2;
            Console.WriteLine($"{Name} je level-upao! Novi HP: {HealthPoints}, Novi Damage: {Damage}, Novi Critical Chance: {CriticalChance}, Novi Stun Chance: {StunChance}");
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

        private bool RandomChance(int percentage)
        {
            return new Random().Next(1, 101) <= percentage;
        }
    }
}
