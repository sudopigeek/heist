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
            for (int i = 0; i < rolodex.Count; i++)
            {
                Console.WriteLine($"{i}. {rolodex[i].Name}:");
                Console.WriteLine($"    Speciality: {rolodex[i].GetType().ToString().Split('.')[1]}");
                Console.WriteLine($"    Skill Level: {rolodex[i].SkillLevel}");
                Console.WriteLine($"    Percentage Cut: {rolodex[i].PercentageCut}%");
            }
            List<IRobber> crew = new List<IRobber>();
            string output = "value";
            while (output != "")
            {
                Console.Write("Enter the number of the operative you want to include in the heist:");
                output = Console.ReadLine();
                if (output == "") { continue; }
                int num = int.Parse(output);
                List<IRobber> filtered = rolodex.Where(r => !crew.Contains(r) && r.PercentageCut < 100 - crew.Select(s => s.PercentageCut).Sum()).ToList();
                if (filtered.Contains(rolodex[num]))
                {
                    crew.Add(rolodex[num]);
                    Console.WriteLine("Operative successfully added!");
                }
                else
                {
                    Console.WriteLine("Operative is already included.");
                }
            }
            foreach (IRobber robber in crew)
            {
                robber.PerformSkill(bank1);
            }
            if (bank1.IsSecure)
            {
                Console.WriteLine("The heist was a failure. :(");
            }
            else
            {
                Console.WriteLine("The heist was a success!!!!");
                Console.WriteLine("\n===== Operative Pay =====\n");
                double moneyPaid = 0;
                foreach (IRobber robber in crew)
                {
                    moneyPaid += (bank1.CashOnHand * (robber.PercentageCut * .01));
                    Console.WriteLine($"{robber.Name}: {robber.PercentageCut}% of ${bank1.CashOnHand.ToString("N")} = ${(bank1.CashOnHand * (robber.PercentageCut * .01)).ToString("N")}");
                }
                Console.WriteLine($"Amount left over for yourself: ${((double)bank1.CashOnHand - moneyPaid).ToString("N")}");
            }
        }
    }
}
