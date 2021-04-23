using System;
using System.Collections.Generic;

namespace BattleMages
{
    class Program
    {
        private static bool gameOver = false;
        private static Mage mage1;
        private static Mage mage2;
        private enum BattleActionOptions
        {
            Attack,
            Defend,
            Recharge
        }
        private enum BattleEndOptions
        {
            No,
            Yes
        }
        static void Main(string[] args)
        {
            Console.Write("First Player Name: ");
            string name = Console.ReadLine();
            mage1 = new Mage(name, 0);
            Console.Write("Second Player Name: ");
            name = Console.ReadLine();
            mage2 = new Mage(name, 0);
            do
            {
                ShowStatus();
                int option1 = PlayerChooseOptions(mage1);
                int option2 = PlayerChooseOptions(mage2);
                Console.Clear();
                PlayerAction(option1, option2, mage1, mage2);
                PlayerAction(option2, option1, mage2, mage1);
                CheckBattleResults();
            }
            while (!gameOver);

            if (mage1.Wins > mage2.Wins)
            {
                Console.WriteLine($"Congratulations, {mage1.Name}! You won {mage1.Wins} battle(s).");
            }
            else if (mage2.Wins > mage1.Wins)
            {
                Console.WriteLine($"Congratulations, {mage2.Name}! You won {mage2.Wins} battle(s).");
            }
            else
            {
                Console.WriteLine("The game ended with a draw.");
            }

            Console.WriteLine();
            Console.WriteLine("Press enter key to finish ...");
            Console.ReadLine();
        }

        private static void CheckBattleResults()
        {
            if (mage2.GameOver)
            {
                Console.WriteLine(mage1.Name + " won the battle");
                mage1.Wins++;
            }
            else if (mage1.GameOver)
            {
                Console.WriteLine(mage2.Name + " won the battle");
                mage2.Wins++;
            }

            if (mage1.GameOver || mage2.GameOver)
            {
                bool isValid = false;
                do
                {
                    Console.WriteLine("Want to restart?");
                    Console.WriteLine($"{(int)BattleEndOptions.Yes}) Yes");
                    Console.WriteLine($"{(int)BattleEndOptions.No}) No");

                    int option = int.Parse(Console.ReadLine());
                    if (option == (int)BattleEndOptions.No)
                    {
                        isValid = true;
                        gameOver = true;
                    }
                    else if (option == (int)BattleEndOptions.Yes)
                    {
                        isValid = true;
                        mage1 = new Mage(mage1.Name, mage1.Wins);
                        mage2 = new Mage(mage2.Name, mage2.Wins);
                    }
                } while (!isValid);
            }
        }

        public static void PlayerAction(int playerOption, int enemyOption, Mage player, Mage enemy)
        {
            if (player.HP == 0 || enemy.HP == 0)
            {
                return;
            }
            switch (playerOption)
            {
                case (int)BattleActionOptions.Attack:
                    int playerMP = player.MP;
                    player.Attack();
                    if (enemyOption != (int)BattleActionOptions.Defend && playerMP > 0)
                    {
                        enemy.HPLoss();
                    }
                    break;
                case (int)BattleActionOptions.Defend:
                    player.Defend();
                    break;
                case (int)BattleActionOptions.Recharge:
                    if (player.MP < 3)
                    {
                        player.Recharge();
                    }
                    break;
            };
        }
        public static int PlayerChooseOptions(Mage player)
        {
            bool isValid = false;
            int option;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"{player.Name} turn");
                Console.ResetColor();
                Console.WriteLine();
                List<int> validActions = new List<int>();
                Console.WriteLine("Choose one option below");
                Console.WriteLine();
                if (player.MP > 0)
                {
                    Console.WriteLine($"{(int)BattleActionOptions.Attack}) Attack");
                    validActions.Add((int)BattleActionOptions.Attack);
                }
                Console.WriteLine($"{(int)BattleActionOptions.Defend}) Defend");
                validActions.Add((int)BattleActionOptions.Defend);
                Console.WriteLine($"{(int)BattleActionOptions.Recharge}) Recharge");
                validActions.Add((int)BattleActionOptions.Recharge);

                Console.Write("Option: ");
                option = int.Parse(Console.ReadLine());
                if (validActions.Contains(option))
                {
                    isValid = true;
                }
            } while (!isValid);

            return option;
        }
        public static void ShowStatus()
        {
            Console.WriteLine($"{mage1.Name.ToUpper()} VS {mage2.Name.ToUpper()}");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{mage1.Name}");
            Console.WriteLine($"HP: {mage1.HP} MP: {mage1.MP}");
            Console.WriteLine();
            Console.WriteLine($"{mage2.Name}");
            Console.WriteLine($"HP: {mage2.HP} MP: {mage2.MP}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
