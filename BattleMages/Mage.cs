using System;

namespace BattleMages
{
    public class Mage
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Wins { get; set; }
        public bool GameOver { get; set; }

        public Mage(string name, int wins)
        {
            Name = name;
            HP = 3;
            MP = 1;
            Wins = wins;
            GameOver = false;
        }

        public void HPLoss()
        {
            if (HP > 0)
            {
                HP--;
                Message($"{Name} lost HP", ConsoleColor.Red);
            }
            GameOver = HP == 0;
        }

        public void Recharge()
        {
            if (MP >= 3)
            {
                Message($"{Name} can't recharge because max MP limit reached", ConsoleColor.Green);
            }
            else if (MP <= 3)
            {
                MP++;
                Message($"{Name} recharged", ConsoleColor.Green);
            }
        }

        public void Attack()
        {
            if (MP >= 1)
            {
                MP--;
                Message($"{Name} attacked", ConsoleColor.Yellow);
            }
        }

        public void Defend()
        {
            Message($"{Name} defended and no damage was caused", ConsoleColor.Green);
        }

        public void Message(string message, ConsoleColor color)
        {
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
