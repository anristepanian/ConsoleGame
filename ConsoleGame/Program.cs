using System;
using System.Diagnostics;
using System.Linq;

namespace ConsoleGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            Console.Title = "Console Game!";
            int sleep_tm = 750;
            bool is_lost = false;
            bool is_won = false;
            string els = "SISHS?I";
            int count = 0;
            int diff;
            while (true)
            {
                Console.Write("Choose a difficulty (2 to 15): ");
                diff = Convert.ToInt32(Console.ReadLine());
                if (diff >= 2 && diff <= 15) break;
            }
            Console.WriteLine();
            char[,] map = new char[diff, diff];
            for (int i = 0; i < diff; i++)
                for (int j = 0; j < diff; j++)
                    map[i, j] = els[r.Next(0, els.Length)];

            map[0, 0] = 'P';
            int[] pos = {0, 0};
            char loc = 'S';
            map[diff - 1, diff - 1] = 'F';
            DrawMap(diff, map);
            while (true)
            {
                Console.WriteLine();
                Console.Write("Make a move:\n");
                ConsoleKey act = Console.ReadKey().Key;
                switch (act)
                {
                    case ConsoleKey.UpArrow:
                        count++;
                        Up();
                        break;
                    case ConsoleKey.DownArrow:
                        count++;
                        Down();
                        break;
                    case ConsoleKey.LeftArrow:
                        count++;
                        Left();
                        break;
                    case ConsoleKey.RightArrow:
                        count++;
                        Right();
                        break;
                    default: Environment.Exit(0); break;
                }
                if (is_lost)
                {
                    Console.WriteLine("Unfortunately, you failed!\n");
                    break;
                }
                if (is_won)
                {
                    Console.WriteLine($"You won! \nIn {count} moves!\n");
                    break;
                }
            }
            Console.WriteLine("That was fun! Wasn't it??\nIf you want to play again press R\nIf you want to quit press Q");
            while (true)
            {
                ConsoleKey res = Console.ReadKey().Key;
                if (res == ConsoleKey.R) { RestartApp(1, "ConsoleGame.exe"); Environment.Exit(0); }
                else if (res == ConsoleKey.Q) Environment.Exit(0);
                else Console.WriteLine("\nOk. Try again!");
            }

            (char[,], int[], char, bool, bool) Up()
            {
                if (pos[0] == 0) return (map, pos, loc, is_lost, is_won);
                else
                {
                    if (map[pos[0] - 1, pos[1]] == 'S')
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[0] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0] - 1, pos[1]] == 'H')
                    {
                        is_lost = true;
                        map[pos[0], pos[1]] = loc;
                        pos[0] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = ' ';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        Console.Beep();
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0] - 1, pos[1]] == 'F')
                    {
                        is_won = true;
                        map[pos[0], pos[1]] = loc;
                        pos[0] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0] - 1, pos[1]] == 'I') && (pos[0] - 1 == 0))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[0] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0] - 1, pos[1]] == 'I') && (pos[0] - 1 > 0))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[0] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Up();
                    }
                    else if (map[pos[0] - 1, pos[1]] == '?' && loc != 'I')
                    {
                        map[pos[0] - 1, pos[1]] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                    }
                    else if (map[pos[0] - 1, pos[1]] == '?' && loc == 'I')
                    {
                        map[pos[0] - 1, pos[1]] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Up();
                    }
                    return (map, pos, loc, is_lost, is_won);
                }
            }
            (char[,], int[], char, bool, bool) Down()
            {
                if (pos[0] == diff - 1) return (map, pos, loc, is_lost, is_won);
                else
                {
                    if (map[pos[0] + 1, pos[1]] == 'S')
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[0] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0] + 1, pos[1]] == 'H')
                    {
                        is_lost = true;
                        map[pos[0], pos[1]] = loc;
                        pos[0] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = ' ';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        Console.Beep();
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0] + 1, pos[1]] == 'F')
                    {
                        is_won = true;
                        map[pos[0], pos[1]] = loc;
                        pos[0] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0] + 1, pos[1]] == 'I') && (pos[0] + 2 == diff))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[0] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0] + 1, pos[1]] == 'I') && (pos[0] + 2 < diff))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[0] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Down();
                    }
                    else if (map[pos[0] + 1, pos[1]] == '?' && loc != 'I')
                    {
                        map[pos[0] + 1, pos[1]] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                    }
                    else if (map[pos[0] + 1, pos[1]] == '?' && loc == 'I')
                    {
                        map[pos[0] + 1, pos[1]] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Down();
                    }
                    return (map, pos, loc, is_lost, is_won);
                }
            }
            (char[,], int[], char, bool, bool) Left()
            {
                if (pos[1] == 0) return (map, pos, loc, is_lost, is_won);
                else
                {
                    if (map[pos[0], pos[1] - 1] == 'S')
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[1] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0], pos[1] - 1] == 'H')
                    {
                        is_lost = true;
                        map[pos[0], pos[1]] = loc;
                        pos[1] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = ' ';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        Console.Beep();
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0], pos[1] - 1] == 'F')
                    {
                        is_won = true;
                        map[pos[0], pos[1]] = loc;
                        pos[1] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0], pos[1] - 1] == 'I') && (pos[1] - 1 == 0))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[1] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0], pos[1] - 1] == 'I') && (pos[1] - 1 > 0))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[1] -= 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Left();
                    }
                    else if (map[pos[0], pos[1] - 1] == '?' && loc != 'I')
                    {
                        map[pos[0], pos[1] - 1] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                    }
                    else if (map[pos[0], pos[1] - 1] == '?' && loc == 'I')
                    {
                        map[pos[0], pos[1] - 1] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Left();
                    }
                    return (map, pos, loc, is_lost, is_won);
                }
            }
            (char[,], int[], char, bool, bool) Right()
            {
                if (pos[1] == diff - 1) return (map, pos, loc, is_lost, is_won);
                else
                {
                    if (map[pos[0], pos[1] + 1] == 'S')
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[1] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0], pos[1] + 1] == 'H')
                    {
                        is_lost = true;
                        map[pos[0], pos[1]] = loc;
                        pos[1] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = ' ';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        Console.Beep();
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if (map[pos[0], pos[1] + 1] == 'F')
                    {
                        is_won = true;
                        map[pos[0], pos[1]] = loc;
                        pos[1] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0], pos[1] + 1] == 'I') && (pos[1] + 2 == diff))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[1] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        return (map, pos, loc, is_lost, is_won);
                    }
                    else if ((map[pos[0], pos[1] + 1] == 'I') && (pos[1] + 2 < diff))
                    {
                        map[pos[0], pos[1]] = loc;
                        pos[1] += 1;
                        loc = map[pos[0], pos[1]];
                        map[pos[0], pos[1]] = 'P';
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Right();
                    }
                    else if (map[pos[0], pos[1] + 1] == '?' && loc != 'I')
                    {
                        map[pos[0], pos[1] + 1] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                    }
                    else if (map[pos[0], pos[1] + 1] == '?' && loc == 'I')
                    {
                        map[pos[0], pos[1] + 1] = "HIS"[r.Next(0, 3)];
                        Console.WriteLine(String.Concat(Enumerable.Repeat("- ", 25)));
                        DrawMap(diff, map);
                        System.Threading.Thread.Sleep(sleep_tm);
                        Right();
                    }
                    return (map, pos, loc, is_lost, is_won);
                }
            }
        }
        public static void DrawMap(int diff, char[,] map)
        {
            for (int i = 0; i < diff; i++)
            {
                for (int j = 0; j < diff; j++)
                    Console.Write(map[i, j] + "\t");
                Console.WriteLine("\n");
            }
        }
        public static void RestartApp(int pid, string applicationName)
        {
            Process process = null;
            try
            {
                process = Process.GetProcessById(pid);
                process.WaitForExit(1000);
            }
            catch (ArgumentException) { }
            Process.Start(applicationName, "");
        }
    }
}
