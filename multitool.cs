using System;
using System.Threading;

namespace Tut
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.Title = "ULTRA HACKER TERMINAL";

            BootSequence(); // Cool startup animation
            Login();

            while (true)
            {
                Console.Clear();
                Banner();

                Console.WriteLine("[1] Scan Network");
                Console.WriteLine("[2] Breach System");
                Console.WriteLine("[3] Matrix Mode");
                Console.WriteLine("[4] Hack Puzzle");
                Console.WriteLine("[5] Exit");

                Console.Write("\n> ");

                char key = Console.ReadKey(true).KeyChar;
                Console.WriteLine();

                switch (key)
                {
                    case '1': Scan(); break;
                    case '2': Breach(); break;
                    case '3': MatrixRain(); break;
                    case '4': Puzzle(); break;
                    case '5': return;
                    default: TypeGlitch("UNKNOWN COMMAND"); break;
                }

                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }
        }

        // ================= BOOT SEQUENCE =================
        static void BootSequence()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            TypeGlitch("Initializing system...");
            Thread.Sleep(500);

            TypeGlitch("Loading modules...");
            Thread.Sleep(500);

            TypeGlitch("Establishing secure connection...");
            Thread.Sleep(500);

            TypeGlitch("ACCESS READY");
            Thread.Sleep(800);

            Console.Clear();
        }

        // ================= LOGIN (MASKED PASSWORD) =================
        static void Login()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("=== SECURE LOGIN ===");

                Console.Write("Username: ");
                string user = Console.ReadLine();

                Console.Write("Password: ");
                string pass = ReadPassword(); // Masked input

                if (user == "admin" && pass == "1234")
                {
                    TypeGlitch("ACCESS GRANTED");
                    Thread.Sleep(800);
                    return;
                }
                else
                {
                    TypeGlitch("ACCESS DENIED");
                    Thread.Sleep(800);
                }
            }
        }

        // ================= PASSWORD MASKING =================
        static string ReadPassword()
        {
            string password = "";

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return password;
        }

        // ================= BANNER =================
        static void Banner()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====================================");
            Console.WriteLine("   ULTRA HACKER TERMINAL v3.0");
            Console.WriteLine("====================================");
        }

        // ================= GLITCH TEXT =================
        static void TypeGlitch(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(rand.Next(5, 30)); // random speed
            }
            Console.WriteLine();
        }

        // ================= PROGRESS BAR =================
        static void Progress()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\r[{new string('#', i / 5).PadRight(20)}] {i}%");
                Thread.Sleep(15);
            }
            Console.WriteLine();
        }

        // ================= RANDOM IP =================
        static string IP()
        {
            return $"{rand.Next(1,255)}.{rand.Next(1,255)}.{rand.Next(1,255)}.{rand.Next(1,255)}";
        }

        // ================= SCAN =================
        static void Scan()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            TypeGlitch("Scanning network...");
            Progress();

            for (int i = 0; i < 5; i++)
            {
                TypeGlitch("Found node: " + IP());
            }

            TypeGlitch("Scan complete.");
        }

        // ================= BREACH =================
        static void Breach()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            TypeGlitch("Injecting payload...");
            Progress();

            TypeGlitch("Bypassing firewall...");
            Progress();

            TypeGlitch("ACCESS GRANTED");
        }

        // ================= MATRIX RAIN =================
        static void MatrixRain()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < 200; i++)
            {
                Console.SetCursorPosition(rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight));
                Console.Write((char)rand.Next(33, 126));
                Thread.Sleep(5);
            }
        }

        // ================= PUZZLE =================
        static void Puzzle()
        {
            int code = rand.Next(100, 999);

            TypeGlitch("Enter 3-digit code:");

            Console.Write("> ");
            string input = Console.ReadLine();

            if (input == code.ToString())
                TypeGlitch("ACCESS GRANTED");
            else
                TypeGlitch("ACCESS DENIED (Code was " + code + ")");
        }
    }
}
