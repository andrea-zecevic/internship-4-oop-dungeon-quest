using Domain.Game.Enums;

namespace Domain.Game.Repositories
{
    public abstract class Hero
    {
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int Experience { get; set; }
        public int Damage { get; set; }
        public HeroType Type { get; set; }
        protected Hero(string name, int healthPoints, int experience, int damage, HeroType type)
        {
            Name = name;
            HealthPoints = healthPoints;
            Experience = experience;
            Damage = damage;
            Type = type;
        }
        public abstract void Attack(Monster monster);
        public abstract void LevelUp();
        public abstract void GainExperience(int gainedExperience);
    }
}
