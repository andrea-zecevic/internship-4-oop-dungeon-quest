using Domain.Game.Repositories;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Dobrodošli u igru!");

        do
        {
            Console.WriteLine("\nUnesite ime heroja:");
            var heroName = GetNonEmptyString();

            Console.WriteLine("\nOdaberite vrstu heroja:");
            Console.WriteLine("1. Gladiator");
            Console.WriteLine("2. Enchanter");
            Console.WriteLine("3. Marksman");

            int heroChoice = GetIntegerInput(1, 3);

            Hero hero;

            switch (heroChoice)
            {
                case 1:
                    hero = CreateGladiator(heroName);
                    break;
                case 2:
                    hero = CreateEnchanter(heroName);
                    break;
                case 3:
                    hero = CreateMarksman(heroName);
                    break;
                default:
                    Console.WriteLine("Pogrešan unos. Kreiran je Gladiator.");
                    hero = CreateGladiator(heroName);
                    break;
            }


            Console.WriteLine($"Heroj {hero.Name} stvoren.");
            PrintHero(hero);

            List<Monster> monsters = GenerateMonsters(10);
            PrintMonsterList(monsters);

            StartBattle(hero, monsters);

            Console.WriteLine("Tvoja igra je gotova!\nŽelite li igrati novu rundu?\n\n1 - Nova runda!\n2 - Kraj!");

            var userInput = GetIntegerInput(1, 2);

            if (userInput != 1)
            {
                Console.WriteLine("Hvala na igri. Dovidenja!");
                break;
            }

            Console.Clear();
            PrintTitleOfNewGame();

        } while (true);

    }

    static void PrintTitleOfNewGame()
    {
        Console.WriteLine("\n==================================");
        Console.WriteLine("           Nova Runda             ");
        Console.WriteLine("==================================\n");
    }

    static int GetIntegerInput(int minValue, int maxValue)
    {
        int result;
        while (true)
        {
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out result) && result >= minValue && result <= maxValue)
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Neispravan unos. Unesite broj između {minValue} i {maxValue}.");
            }
        }
    }
 
    static string GetNonEmptyString()
    {
        string? userInput;
        do
        {
            Console.Write(">> ");
            userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Unos ne može biti prazan. Molimo pokušajte ponovno.");
            }

        } while (string.IsNullOrWhiteSpace(userInput));

        return userInput;
    }

    static Hero CreateGladiator(string name)
    {
        var inputChoice = SeeIfUserWantsToSetValuesOrNot(name); 

        if (inputChoice == 1)
        {
            int healthPoints = SetValuesOfHealth(name);
            int damage = SetValuesOfDemage(name);

            return new Gladiator(name, healthPoints, 0, damage);
        }
        else
        {
            return new Gladiator(name);
        }
    }

    static int SetValuesOfHealth(string name)
    {
        Console.Clear();
        Console.WriteLine($"Unesite HealthPoints za {name} (unesite pozitivan cijeli broj):");
        var healthPoints = GetNonNegativeIntegerInput();
        return healthPoints;
    }

    static int SetValuesOfDemage(string name)
    {
        Console.WriteLine($"Unesite Damage za {name}:");
        int damage = GetNonNegativeIntegerInput();
        Console.Clear();
        return damage;
    }

    static int SeeIfUserWantsToSetValuesOrNot(string name)
    {
        Console.WriteLine($"\nŽelite li sami postaviti vrijednosti za {name}?");
        Console.WriteLine("1. Da");
        Console.WriteLine("2. Ne");
        
        int choice = GetIntegerInput(1, 2);

        return (choice == 1) ? 1 : 2;
    }

    static Hero CreateEnchanter(string name)
    {
        var inputChoice = SeeIfUserWantsToSetValuesOrNot(name);

        if (inputChoice == 1)
        {
            int healthPoints = SetValuesOfHealth(name);
            int damage = SetValuesOfDemage(name);

            return new Enchater(name, healthPoints, 0, damage);
        }
        else
        {
            return new Enchater(name);
        }

    }

    static Hero CreateMarksman(string name)
    {
        var inputChoice = SeeIfUserWantsToSetValuesOrNot(name);

        if (inputChoice == 1)
        {
            int healthPoints = SetValuesOfHealth(name);
            int damage = SetValuesOfDemage(name);

            return new Marksman(name, healthPoints, 0, damage);
        }
        else
        {
            return new Marksman(name);
        }
    }

    static int GetNonNegativeIntegerInput()
    {
        int result;
        while (true)
        {
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out result) && result >= 0)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Neispravan unos. Unesite pozitivan cijeli broj.");
            }
        }
    }

    static List<Monster> GenerateMonsters(int count)
    {
        List<Monster> monsters = new List<Monster>();
        for (int i = 0; i < count; i++)
        {
            monsters.Add(RandomMonster());
        }
        return monsters;
    }

    static Monster RandomMonster()
    {
        var monsterType = GetMonsterTypeBasedOnProbability();

        switch (monsterType)
        {
            case 1:
                return new Goblin();
            case 2:
                return new Brute();
            case 3:
                return new Witch();
            default:
                throw new InvalidOperationException("Neispravna vrsta čudovišta generirana.");
        }
    }

    static int GetMonsterTypeBasedOnProbability()
    {
        Random random = new Random();

        int probability = random.Next(1, 101);

        return (probability <= 60) ? 1 : (probability <= 90) ? 2 : 3;
    }

    static void PrintMonsterList(List<Monster> monsters)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nGenerirana čudovišta:", Console.ForegroundColor);

        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("| {0,-20} | {1,-10} | {2,-10} | {3,-10} |",
                          "Ime", "HP", "Damage", "XP");
        Console.WriteLine("-----------------------------------------------------------");

        foreach (var monster in monsters)
        {
            Console.WriteLine("| {0,-20} | {1,-10} | {2,-10} | {3,-10} |",
                              monster.Name, monster.HealthPoints, monster.Damage, monster.ExperienceValue);
        }

        Console.WriteLine("-----------------------------------------------------------");
        Console.ResetColor();
    }

    static void PrintHero(Hero hero)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\nDetalji o heroju {hero.Name}:", Console.ForegroundColor);

        PrintTableRow("Ime", hero.Name);
        PrintTableRow("Health Points", hero.HealthPoints.ToString());
        PrintTableRow("Experience", hero.Experience.ToString());
        PrintTableRow("Damage", hero.Damage.ToString());
        PrintTableRow("Tip heroja", hero.Type.ToString());

        if (hero is Gladiator gladiator)
        {
            PrintTableRow("Rage Mode", gladiator.IsRageMode.ToString());
        }
        else if (hero is Enchater enchater)
        {
            PrintTableRow("Mana", enchater.Mana.ToString());
        }
        else if (hero is Marksman marksman)
        {
            PrintTableRow("Critical Chance", marksman.CriticalChance.ToString());
            PrintTableRow("Stun Chance", marksman.StunChance.ToString());
        }

        Console.ResetColor();
    }

    static void PrintTableRow(string label, string value)
    {
        Console.Write($"{label,-20}");
        Console.WriteLine(value);
    }


    static void StartBattle(Hero hero, List<Monster> monsters)
    {

        Console.WriteLine("\nPocinje borba!");

        foreach (Monster monster in monsters)
        {
            Console.WriteLine($"\nHeroj {hero.Name} se suočava s cudovištem {monster.Name}!");

            while (hero.HealthPoints > 0 && monster.HealthPoints > 0)
            {
                var heroAction = PrintHeroAttacOptions(hero);

                int monsterAction = GetMonsterAction();

                DetermineBattleOutcome(hero, monster, heroAction, monsterAction);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{hero.Name,-20} - HP: {hero.HealthPoints,-5} | {monster.Name,-20} - HP: {monster.HealthPoints,-5}");
                Console.ResetColor();
            }

            if (hero.HealthPoints <= 0)
            {
                Console.WriteLine($"Heroj {hero.Name} je porazen. Kraj igre.");
                break;
            }
            else
            {
                Console.WriteLine($"Cudoviste {monster.Name} je porazeno!");
                hero.GainExperience(monster.ExperienceValue);
            }

            Console.Clear();
        }
    }

    static int GetMonsterAction()
    {
        Random random = new Random();
        return random.Next(1, 4);
    }

    static void DetermineBattleOutcome(Hero hero, Monster monster, int heroAction, int monsterAction)
    {
        if ((heroAction == 1 && monsterAction == 2) || (heroAction == 2 && monsterAction == 3) || (heroAction == 3 && monsterAction == 1))
        {
            Console.WriteLine($"{hero.Name} pobjeduje!");
            monster.HealthPoints -= hero.Damage;
        }
        else if ((monsterAction == 1 && heroAction == 2) || (monsterAction == 2 && heroAction == 3) || (monsterAction == 3 && heroAction == 1))
        {
            Console.WriteLine($"{monster.Name} pobjeduje!");
            hero.HealthPoints -= monster.Damage;
        }
        else
        {
            Console.WriteLine("Nema pobjednika u ovoj rundi.");
        }
    }
    static int PrintHeroAttacOptions(Hero hero)
    {
        Console.WriteLine("\nOdaberite svoju akciju:");
        Console.WriteLine("1. Direktan napad");
        Console.WriteLine("2. Napad s boka");
        Console.WriteLine("3. Protunapad");

        if (hero is Gladiator)
        {
            Console.WriteLine("4. Napad iz bijesa");
        }
        else if (hero is Enchater)
        {
            Console.WriteLine("4. Koristi magiju");
            Console.WriteLine("5. Obnovi životne bodove");
        }
        else if (hero is Marksman)
        {
            Console.WriteLine("4. Kritični napad");
            Console.WriteLine("5. Stun napad");
        }
        int heroAction = GetIntegerInput(1, 5);

        return heroAction;
    }
}
