using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = new List<IRobber>()
            {
                {new Hacker() {Name = "Mr. Pink", SkillLevel = 100, PercentageCut = 15}},
                {new Muscle() {Name = "Bob", SkillLevel = 80, PercentageCut = 20}},
                {new Muscle() {Name = "Ralph", SkillLevel = 60, PercentageCut = 15}},
                {new LockSpecialist() {Name = "Riley", SkillLevel = 88, PercentageCut = 30}},
                {new LockSpecialist() {Name = "Steve", SkillLevel = 76, PercentageCut = 15}}
            };
            string crewMember = "value";
            while (crewMember != "")
            {
                Console.WriteLine("Current Operatives:\n");
                foreach (IRobber robber in rolodex)
                {
                    Console.WriteLine($"{robber.Name}");
                }
                Console.Write("Enter a new crew member: ");
                crewMember = Console.ReadLine();
                if (crewMember == "") { continue; }
                Console.Write($"What speciality (Hacker, Muscle, Lock Specialist) should {crewMember} have? ");
                string speciality = Console.ReadLine();
                Console.Write($"What skill level (1-100) should this {speciality} have? ");
                int skillLevel = int.Parse(Console.ReadLine());
                Console.Write($"What should {crewMember} want as a percentage cut? ");
                int percentageCut = int.Parse(Console.ReadLine());
                if (speciality == "Hacker")
                {
                    rolodex.Add(new Hacker()
                    {
                        Name = crewMember,
                        SkillLevel = skillLevel,
                        PercentageCut = percentageCut
                    });
                    Console.WriteLine("Crew member added!");
                }
                else if (speciality == "Muscle")
                {
                    rolodex.Add(new Muscle()
                    {
                        Name = crewMember,
                        SkillLevel = skillLevel,
                        PercentageCut = percentageCut
                    });
                    Console.WriteLine("Crew member added!");
                }
                else if (speciality == "Lock Specialist")
                {
                    rolodex.Add(new LockSpecialist()
                    {
                        Name = crewMember,
                        SkillLevel = skillLevel,
                        PercentageCut = percentageCut
                    });
                    Console.WriteLine("Crew member added!");
                }
            }
            Random randInt = new Random();
            Bank bank1 = new Bank()
            {
                AlarmScore = randInt.Next(0, 100),
                VaultScore = randInt.Next(0, 100),
                SecurityGuardScore = randInt.Next(0, 100),
                CashOnHand = randInt.Next(50000, 1000000)
            };
            Dictionary<string, int> systemList = new Dictionary<string, int>() {
                {"Alarm", bank1.AlarmScore},
                {"Vault", bank1.VaultScore},
                {"Security Guard", bank1.SecurityGuardScore}
            };
            var sortedDict = from entry in systemList orderby entry.Value ascending select entry;
            Console.WriteLine($"Least Secure: {sortedDict.ElementAt(0).Key}");
            Console.WriteLine($"Most Secure: {sortedDict.ElementAt(2).Key}");
        }
    }
}
