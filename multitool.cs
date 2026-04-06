using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = System.Random;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;

namespace Multitool
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();

            while (true)
            {
                int width = 63;
                int height = 19;
                Console.Clear();
                EnsureConsoleSize(width, height);
                Banner(); // call method
                Menu(); // call method

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Option: ");
                Console.ResetColor();

                string option = (Console.ReadLine() ?? "").Trim();

                switch (option)
                {
                    case "1":
                        IpTools(); // call method
                        break;
                    case "2":
                        GeneratorsMenu(); // call method
                        break;    
                    case "3":
                        EncryptTextMenu();
                        break;
                    case "4":
                        DecryptTextMenu();
                        break;
                    case "5":
                        PortScanner();
                        break;
                    case "6":
                        HashGenerator();
                        break;
                    case "7":
                        HashCracker();
                        break; 
                    case "8":
                        SubdomainScanner();
                        break;           
                    case "9":
                        WhoisLookup(); // call method
                        break;        
                    case "10":
                        Console.Clear();
                        return; // exit program   
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                        
                }
            }
        }

        // <-- MUST BE OUTSIDE Main
        static void IpTools()
        {
            while (true)
            {
                string[] items = new string[] {
                    "[1] IP Generator",
                    "[2] Subnet Calculator",
                    "[3] Show Your Public IP",
                    "[4] Back to Main Menu"
                };
                DrawBoxedMenu("IP Tools", items);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Option: ");
                Console.ResetColor();
                string ipOption = (Console.ReadLine() ?? "").Trim();

                switch (ipOption)
                {
                    case "1":
                        Console.Clear();
                        string[] genItems = new string[] { "[1] Random IPv4", "[2] Random IPv6" };
                        DrawBoxedMenu("IP Generator", genItems);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Option: ");
                        Console.ResetColor();
                        string genOption1 = (Console.ReadLine() ?? "").Trim();
                        if (genOption1 == "1")
                        {
                            RandomIpv4();
                        }
                        else if (genOption1 == "2")
                        {
                            RandomIpv6();
                        }
                        break;                   
                    case "2":
                        Console.Clear();
                        SubnetCalculator();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        string publicIP = GetPublicIPAddress();
                        Console.ForegroundColor = ConsoleColor.Red;
                        string[] Items = new string[] { "Your Public IP: {publicIP}", "Press any key to return to the Menu" };
                        DrawBoxedMenu("Public IP Address", Items);
                        Console.ReadKey();
                        break;    
                    case "4":
                        Console.Clear();
                        return; // go back to main menu
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Banner()
        {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"██████╗  ██████╗  ██████╗ ███╗   ███╗███████╗██████╗");// red
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"██╔══██╗██╔═══██╗██╔═══██╗████╗ ████║██╔════╝██╔══██╗");// red
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(@"██║  ██║██║   ██║██║   ██║██╔████╔██║█████╗  ██████╔╝");// darkorange
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(@"██║  ██║██║   ██║██║   ██║██║╚██╔╝██║██╔══╝  ██╔══██╗");// darkorange
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"██████╔╝╚██████╔╝╚██████╔╝██║ ╚═╝ ██║███████╗██║  ██║");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"╚═════╝  ╚═════╝  ╚═════╝ ╚═╝     ╚═╝╚══════╝╚═╝  ╚═╝");// orange
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("                Multitool by 0xA1A                   ");// yellow
        }
        static void Menu()
        {
                Console.WriteLine();
                string[] items = new string[] {
                    "[1]  IP Tools",
                    "[2]  Generators",
                    "[3]  Encrypt text",
                    "[4]  Decrypt text",
                    "[5]  Port Scanner",
                    "[6]  Hash Generator",
                    "[7]  Hash Cracker",
                    "[8]  Subdomain Scanner",
                    "[9]  Whois Lookup",
                    "[10] Exit"
                };

                int boxWidth = Math.Min(48, Math.Max(30, Console.WindowWidth - 8));
                int left = Math.Max(0, (Console.WindowWidth - boxWidth) / 2);

                string top = new string('═', boxWidth - 2);
                Console.Write(new string(' ', left));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔" + top + "╗");

                for (int i = 0; i < items.Length; i++)
                {
                    int contentWidth = boxWidth - 4; // padding inside box
                    string txt = items[i];
                    int split = txt.IndexOf(']') + 1;
                    string num = split > 0 ? txt.Substring(0, split) : "";
                    string rest = split > 0 ? txt.Substring(split).TrimStart() : txt;

                    string lineContent = "  ";
                    // build colored segments but pad to contentWidth
                    int remaining = contentWidth - (num.Length + 2 + rest.Length);
                    if (remaining < 0) remaining = 0;
                    lineContent += num + " " + rest + new string(' ', remaining);

                    Console.Write(new string(' ', left));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("║ ");
                    // number in cyan
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(num);
                    Console.Write(" ");
                    // rest alternating color
                    Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.Yellow : ConsoleColor.Yellow;
                    Console.Write(rest);
                    // fill space to box width
                    int printed = num.Length + 1 + rest.Length;
                    int padRight = contentWidth - printed;
                    if (padRight > 0) Console.Write(new string(' ', padRight));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ║");
                    Console.ResetColor();
                }

                Console.Write(new string(' ', left));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╚" + top + "╝");
                Console.ResetColor();
        }
        static void RandomIpv4()
        {
            Random rand = new Random();
            string ip = $"{rand.Next(1, 255)}.{rand.Next(0, 255)}.{rand.Next(0, 255)}.{rand.Next(1, 255)}";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Generated IP: {ip}");
            Console.WriteLine("Press any key to return to the IP Tools menu.");
            Console.ReadKey();
        }
        static void RandomIpv6()
        {
            Random rand = new Random();
            string ip = $"{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}:{rand.Next(0, 65536):X4}";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Generated IP: {ip}");
            Console.WriteLine("Press any key to return to the IP Tools menu.");
            Console.ReadKey();
        }
        public static string GetPublicIPAddress()
        {
        using (WebClient client = new WebClient())
        {
            try
            {
                return client.DownloadString("https://api.ipify.org");
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            }
        }
        static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Password: ");
            string password = ReadPasswordMasked();
            if (password == "nkV%6CGgS23k") // hardcoded password check; username may be anything
            {
                Console.Clear();
                Banner();   
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteCentered($"Welcome, {username} to Doomer!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                WriteCentered("Press any key to continue...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                return; // return to Main so it draws the banner/menu
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid credentials. Press any key to try again.");
                Console.ReadKey();
                Login(); // retry login
            }
        }

        static string ReadPasswordMasked()
        {
            var sb = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        sb.Length -= 1;
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                    Console.Write('*');
                }
            }
            return sb.ToString();
        }
        static void WriteCentered(string text)
        {
            int padding = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
            Console.WriteLine(text.PadLeft(padding + text.Length));
        }

        static void DrawBoxedMenu(string title, string[] items)
        {
            // Fallback for very small terminals
            int winW = Console.WindowWidth;
            int winH = Console.WindowHeight;
            Console.Clear();
            if (winW < 40 || winH < 12)
            {
                // Compact, single-column layout without heavy box drawing
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(title);
                Console.ResetColor();
                for (int i = 0; i < items.Length; i++)
                {
                    string raw = items[i];
                    int split = raw.IndexOf(']') + 1;
                    string num = split > 0 ? raw.Substring(0, split) : "";
                    string rest = split > 0 ? raw.Substring(split).TrimStart() : raw;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(num + " ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(rest);
                    Console.ResetColor();
                }
                return;
            }

            int boxWidth = Math.Min(60, Math.Max(30, winW - 8));
            int left = Math.Max(0, (winW - boxWidth) / 2);
            string top = new string('═', boxWidth - 2);

            Console.Write(new string(' ', left));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔" + top + "╗");

            // title
            string titleLine = " " + title + " ";
            int titlePad = Math.Max(0, (boxWidth - 2 - titleLine.Length) / 2);
            Console.Write(new string(' ', left));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(new string(' ', titlePad) + titleLine + new string(' ', boxWidth - 2 - titlePad - titleLine.Length));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("║");

            // items
            for (int i = 0; i < items.Length; i++)
            {
                string raw = items[i];
                int contentWidth = boxWidth - 4;
                int split = raw.IndexOf(']') + 1;
                string num = split > 0 ? raw.Substring(0, split) : "";
                string rest = split > 0 ? raw.Substring(split).TrimStart() : raw;

                Console.Write(new string(' ', left));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("║ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(num);
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(rest);
                int printed = num.Length + 1 + rest.Length;
                if (printed < contentWidth) Console.Write(new string(' ', contentWidth - printed));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ║");
                Console.ResetColor();
            }

            Console.Write(new string(' ', left));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╚" + top + "╝");
            Console.ResetColor();
        }

        static void EnsureConsoleSize(int width, int height)
        {
            try
            {
                // Ensure buffer is at least as large as the requested window
                int bufW = Console.BufferWidth;
                int bufH = Console.BufferHeight;
                int newBufW = Math.Max(bufW, width);
                int newBufH = Math.Max(bufH, height);
                if (newBufW != bufW || newBufH != bufH)
                {
                    try { Console.SetBufferSize(newBufW, newBufH); } catch { }
                }

                try { Console.SetWindowSize(width, height); } catch { }
            }
            catch
            {
                // Silently ignore consoles that don't support resizing
            }
        }
        static void WhoisLookup()
{
            string[] items = new string[] { "Enter domain or IP:" };
            DrawBoxedMenu("WHOIS Lookup", items);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter domain or IP: ");
            Console.ResetColor();
    string target = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(target))
    {
        Console.WriteLine("No input provided. Press any key to return.");
        Console.ReadKey();
        return;
    }

    using (WebClient client = new WebClient())
    {
        try
        {
            string url = $"https://api.hackertarget.com/whois/?q={target}";
            string result = client.DownloadString(url);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"WHOIS info for {target}:\n");
            Console.WriteLine(result);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    Console.WriteLine("\nPress any key to return to the menu.");
    Console.ReadKey();

}
static class SubnetHelper
{
    // Convert IPAddress to uint
    public static uint IpToUInt32(IPAddress ip)
    {
        byte[] bytes = ip.GetAddressBytes();
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    // Convert uint to IPAddress
    public static IPAddress UInt32ToIp(uint ip)
    {
        byte[] bytes = BitConverter.GetBytes(ip);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);
        return new IPAddress(bytes);
    }

    // Convert CIDR to mask (e.g., 24 → 255.255.255.0)
    public static IPAddress CidrToMask(int cidr)
    {
        uint mask = 0xffffffff;
        mask <<= (32 - cidr);
        return UInt32ToIp(mask);
    }

    // Count number of 1 bits in mask
    public static int MaskToCidr(IPAddress mask)
    {
        uint m = IpToUInt32(mask);
        int cidr = 0;
        while ((m & 0x80000000) != 0)
        {
            cidr++;
            m <<= 1;
        }
        return cidr;
    }
    
} // end of SubnetHelper

static void SubnetCalculator()
{
    Console.Clear();
    string[] items = new string[] {
        "Enter IP address:",
        "Enter subnet (CIDR /24 or mask):"
    };
    DrawBoxedMenu("Subnet Calculator", items);
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Enter IP address: ");
    Console.ResetColor();
    string ipInput = Console.ReadLine();

    if (!IPAddress.TryParse(ipInput, out IPAddress ip))
    {
        Console.WriteLine("Invalid IP. Press any key to return.");
        Console.ReadKey();
        return;
    }

    Console.Write("Enter subnet (CIDR /24 or mask 255.255.255.0): ");
    string subnetInput = Console.ReadLine();

    IPAddress mask;
    int cidr;

    if (subnetInput.StartsWith("/"))
    {
        // CIDR input
        if (!int.TryParse(subnetInput.TrimStart('/'), out cidr) || cidr < 0 || cidr > 32)
        {
            Console.WriteLine("Invalid CIDR. Press any key to return.");
            Console.ReadKey();
            return;
        }
        mask = SubnetHelper.CidrToMask(cidr);
    }
    else
    {
        // Decimal mask input
        if (!IPAddress.TryParse(subnetInput, out mask))
        {
            Console.WriteLine("Invalid subnet mask. Press any key to return.");
            Console.ReadKey();
            return;
        }
        cidr = SubnetHelper.MaskToCidr(mask);
    }

    uint ipInt = SubnetHelper.IpToUInt32(ip);
    uint maskInt = SubnetHelper.IpToUInt32(mask);

    uint networkInt = ipInt & maskInt;
    uint broadcastInt = networkInt | ~maskInt;

    uint firstUsable = (cidr == 32) ? networkInt : networkInt + 1;
    uint lastUsable = (cidr >= 31) ? broadcastInt : broadcastInt - 1;
    int numberOfHosts = (cidr >= 31) ? (cidr == 32 ? 1 : 2) : (int)(broadcastInt - networkInt - 1);

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    WriteCentered($"IP: {ip}");
    WriteCentered($"Network: {SubnetHelper.UInt32ToIp(networkInt)}");
    WriteCentered($"Broadcast: {SubnetHelper.UInt32ToIp(broadcastInt)}");
    Console.ForegroundColor = ConsoleColor.Yellow;
    WriteCentered($"First usable IP: {SubnetHelper.UInt32ToIp(firstUsable)}");
    WriteCentered($"Last usable IP: {SubnetHelper.UInt32ToIp(lastUsable)}");
    WriteCentered($"Hosts: {numberOfHosts}");
    Console.ForegroundColor = ConsoleColor.Red;
    WriteCentered($"Subnet mask: {mask}");
    WriteCentered($"CIDR: /{cidr}");

    Console.WriteLine();
    Console.ResetColor();
    WriteCentered("Press any key to return to the menu.");
    Console.ReadKey();
}   

        // -------------------- Encryption / Decryption --------------------

        // Ensure keys directory and key file exist; returns key bytes
        static byte[] EnsureKey()
        {
            Directory.CreateDirectory("keys");
            var keyPath = Path.Combine("keys", "aes_key.bin");
            if (!File.Exists(keyPath))
            {
                byte[] k = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(k);
                }
                File.WriteAllBytes(keyPath, k);
            }
            return File.ReadAllBytes(keyPath);
        }

        static void EncryptTextMenu()
        {
            string[] items = new string[] {
                "[1] Encrypt text input",
                "[2] Encrypt file (input path)"
            };
            DrawBoxedMenu("Encrypt Text or File", items);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Option: ");
            Console.ResetColor();
            string o = (Console.ReadLine() ?? "").Trim();
            byte[] key = EnsureKey();

            try
            {
                if (o == "1")
                {
                    Console.Write("Enter text to encrypt: ");
                    string plain = Console.ReadLine() ?? "";
                    byte[] blob = AesGcmEncrypt(Encoding.UTF8.GetBytes(plain), key);
                    Console.WriteLine("\nEncrypted (Base64):");
                    Console.WriteLine(Convert.ToBase64String(blob));
                }
                else if (o == "2")
                {
                    Console.Write("Input file path: ");
                    string inPath = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(inPath) || !File.Exists(inPath))
                    {
                        Console.WriteLine("Invalid input file. Press any key.");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Output file path: ");
                    string outPath = Console.ReadLine();
                    byte[] plaintext = File.ReadAllBytes(inPath);
                    byte[] blob = AesGcmEncrypt(plaintext, key);
                    File.WriteAllBytes(outPath, blob);
                    Console.WriteLine($"Encrypted to {outPath}");
                }
                else Console.WriteLine("Invalid option.");
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        static void DecryptTextMenu()
        {
            string[] items = new string[] {
                "[1] Decrypt Base64 ciphertext (text)",
                "[2] Decrypt file "
            };
            DrawBoxedMenu("Decrypt Text or File", items);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Option: ");
            Console.ResetColor();
            string o = (Console.ReadLine() ?? "").Trim();
            byte[] key = EnsureKey();

            try
            {
                if (o == "1")
                {
                    Console.Write("Enter Base64 ciphertext: ");
                    string b64 = Console.ReadLine() ?? "";
                    byte[] blob = Convert.FromBase64String(b64);
                    byte[] plain = AesGcmDecrypt(blob, key);
                    Console.WriteLine("\nDecrypted text:");
                    Console.WriteLine(Encoding.UTF8.GetString(plain));
                }
                else if (o == "2")
                {
                    Console.Write("Input file path: ");
                    string inPath = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(inPath) || !File.Exists(inPath))
                    {
                        Console.WriteLine("Invalid input file. Press any key.");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Output file path: ");
                    string outPath = Console.ReadLine();
                    byte[] blob = File.ReadAllBytes(inPath);
                    byte[] plain = AesGcmDecrypt(blob, key);
                    File.WriteAllBytes(outPath, plain);
                    Console.WriteLine($"Decrypted to {outPath}");
                }
                else Console.WriteLine("Invalid option.");
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        // AES-CBC + HMAC-SHA256 helpers (compatible across OSes)
        // Format: [1 byte version=1][16 byte IV][32 byte HMAC][ciphertext]
        static byte[] AesGcmEncrypt(byte[] plaintext, byte[] key)
        {
            // derive keys: aesKey = SHA256(key || 0x01), hmacKey = SHA256(key || 0x02)
            byte[] aesKey, hmacKey;
            using (var sha = SHA256.Create())
            {
                sha.TransformFinalBlock(new byte[0], 0, 0); // ensure usable
            }
            using (var sha = SHA256.Create())
            {
                sha.Initialize();
                sha.TransformBlock(key, 0, key.Length, null, 0);
                sha.TransformFinalBlock(new byte[] { 0x01 }, 0, 1);
                aesKey = sha.Hash;
            }
            using (var sha = SHA256.Create())
            {
                sha.Initialize();
                sha.TransformBlock(key, 0, key.Length, null, 0);
                sha.TransformFinalBlock(new byte[] { 0x02 }, 0, 1);
                hmacKey = sha.Hash;
            }

            byte[] iv = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(iv);

            byte[] cipher;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = aesKey;
                aes.IV = iv;
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plaintext, 0, plaintext.Length);
                    cs.FlushFinalBlock();
                    cipher = ms.ToArray();
                }
            }

            using (var hmac = new HMACSHA256(hmacKey))
            {
                using (var ms = new MemoryStream())
                {
                    ms.WriteByte(0x01); // version
                    ms.Write(iv, 0, iv.Length);
                    ms.Write(cipher, 0, cipher.Length);
                    byte[] toHmac = ms.ToArray();
                    byte[] tag = hmac.ComputeHash(toHmac);

                    using (var outMs = new MemoryStream())
                    {
                        outMs.WriteByte(0x01);
                        outMs.Write(iv, 0, iv.Length);
                        outMs.Write(tag, 0, tag.Length);
                        outMs.Write(cipher, 0, cipher.Length);
                        return outMs.ToArray();
                    }
                }
            }
        }

        static byte[] AesGcmDecrypt(byte[] blob, byte[] key)
        {
            if (blob == null || blob.Length < 1 + 16 + 32) throw new ArgumentException("Invalid data");
            int idx = 0;
            byte version = blob[idx++];
            if (version != 0x01) throw new ArgumentException("Unsupported version");

            byte[] iv = new byte[16];
            Array.Copy(blob, idx, iv, 0, iv.Length); idx += iv.Length;

            byte[] tag = new byte[32];
            Array.Copy(blob, idx, tag, 0, tag.Length); idx += tag.Length;

            byte[] cipher = new byte[blob.Length - idx];
            Array.Copy(blob, idx, cipher, 0, cipher.Length);

            // derive keys
            byte[] aesKey, hmacKey;
            using (var sha = SHA256.Create()) { sha.Initialize(); sha.TransformBlock(key, 0, key.Length, null, 0); sha.TransformFinalBlock(new byte[] { 0x01 }, 0, 1); aesKey = sha.Hash; }
            using (var sha = SHA256.Create()) { sha.Initialize(); sha.TransformBlock(key, 0, key.Length, null, 0); sha.TransformFinalBlock(new byte[] { 0x02 }, 0, 1); hmacKey = sha.Hash; }

            // verify HMAC over (version||iv||cipher)
            byte[] toHmac = new byte[1 + iv.Length + cipher.Length];
            toHmac[0] = 0x01;
            Array.Copy(iv, 0, toHmac, 1, iv.Length);
            Array.Copy(cipher, 0, toHmac, 1 + iv.Length, cipher.Length);

            using (var hmac = new HMACSHA256(hmacKey))
            {
                byte[] expected = hmac.ComputeHash(toHmac);
                if (!expected.SequenceEqual(tag)) throw new CryptographicException("HMAC validation failed");
            }

            // decrypt
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = aesKey;
                aes.IV = iv;
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipher, 0, cipher.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }
        static void HashGenerator()
        {
                string[] items = new string[] {
                    "[1] Hash text input",
                    "[2] Hash file (input path)"
                };
                DrawBoxedMenu("Hash Generator", items);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Option: ");
                Console.ResetColor();
                string o = (Console.ReadLine() ?? "").Trim();

            try
            {
                if (o == "1")
                {
                    Console.Write("Enter text to hash: ");
                    string plain = Console.ReadLine() ?? "";
                    string hash = ComputeSha256Hash(plain);
                    Console.WriteLine("\nSHA-256 Hash:");
                    Console.WriteLine(hash);
                }
                else if (o == "2")
                {
                    Console.Write("Input file path: ");
                    string inPath = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(inPath) || !File.Exists(inPath))
                    {
                        Console.WriteLine("Invalid input file. Press any key.");
                        Console.ReadKey();
                        return;
                    }
                    string hash = ComputeFileSha256Hash(inPath);
                    Console.WriteLine($"\nSHA-256 Hash of {inPath}:");
                    Console.WriteLine(hash);
                }
                else Console.WriteLine("Invalid option.");
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        static string ComputeFileSha256Hash(string filePath)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256Hash.ComputeHash(fs);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        builder.Append(hashBytes[i].ToString("x2"));
                    }
                    return builder.ToString();            }

    }
}
            static void HashCracker()
            {
                string[] items = new string[] {
                    "[1] Crack hash with wordlist"
                };
                DrawBoxedMenu("Hash Cracker", items);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Option: ");
                Console.ResetColor();
                string o = (Console.ReadLine() ?? "").Trim();

            try
            {
                if (o == "1")
                {
                    Console.Write("Enter hash to crack: ");
                    string targetHash = Console.ReadLine() ?? "";
                    Console.Write("Enter wordlist file path: ");
                    string wordlistPath = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(wordlistPath) || !File.Exists(wordlistPath))
                    {
                        Console.WriteLine("Invalid wordlist file. Press any key.");
                        Console.ReadKey();
                        return;
                    }

                    bool found = false;
                    foreach (string line in File.ReadLines(wordlistPath))
                    {
                        string hash = ComputeSha256Hash(line.Trim());
                        if (hash.Equals(targetHash, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"\nHash cracked! Plaintext: {line}");
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("\nHash not found in wordlist.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();            

    }
    static void SubdomainScanner()
    {
        string[] items = new string[] {
            "[1] Scan subdomains with wordlist"
        };
        DrawBoxedMenu("Subdomain Scanner", items);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Option: ");
        Console.ResetColor();
        string o = (Console.ReadLine() ?? "").Trim();

        try
        {
            if (o == "1")
            {
                Console.Write("Enter domain to scan (e.g., example.com): ");
                string domain = Console.ReadLine() ?? "";
                Console.Write("Enter subdomain wordlist file path: ");
                string wordlistPath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(wordlistPath) || !File.Exists(wordlistPath))
                {
                    Console.WriteLine("Invalid wordlist file. Press any key.");
                    Console.ReadKey();
                    return;
                }

                List<string> foundSubdomains = new List<string>();
                foreach (string line in File.ReadLines(wordlistPath))
                {
                    string subdomain = line.Trim() + "." + domain;
                    try
                    {
                        IPHostEntry entry = Dns.GetHostEntry(subdomain);
                        foundSubdomains.Add(subdomain);
                        Console.WriteLine($"Found: {subdomain} -> {string.Join(", ", entry.AddressList.Select(a => a.ToString()))}");
                    }
                    catch (Exception)
                    {
                        // Ignore not found
                    }
                }
                if (foundSubdomains.Count == 0)
                {
                    Console.WriteLine("\nNo subdomains found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
}
    static void PortScanner()
    {
        string[] items = new string[] {
            "[1] Scan common ports on target IP/domain",
            "[2] Exit to menu"
        };
        DrawBoxedMenu("Port Scanner", items);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Option: ");
        Console.ResetColor();
        string o = (Console.ReadLine() ?? "").Trim();

        try
        {
            if (o == "1")
            {
                Console.Write("Enter target IP or domain: ");
                string target = Console.ReadLine() ?? "";
                int[] commonPorts = { 21, 22, 23, 25, 53, 80, 110, 143, 443, 3306, 3389 };

                List<int> openPorts = new List<int>();
                foreach (int port in commonPorts)
                {
                    using (var client = new TcpClient())
                    {
                        try
                        {
                            var result = client.BeginConnect(target, port, null, null);
                            bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                            if (success && client.Connected)
                            {
                                openPorts.Add(port);
                                Console.WriteLine($"Port {port} is open.");
                            }
                        }
                        catch (Exception)
                        {
                            // Ignore connection errors
                        }
                    }
                }
                if (openPorts.Count == 0)
                {
                    Console.WriteLine("\nNo common ports found open.");
                }
            }
            else if (o == "2")
            {
                return; // exit to menu
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();

}
    static void GeneratorsMenu()
    {
        string[] items = new string[] {
            "[1] Random password generator",
            "[2] Random username generator",
            "[3] Random email generator",
        };
        DrawBoxedMenu("Generators", items);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Option: ");
        Console.ResetColor();
        string o = (Console.ReadLine() ?? "").Trim();

        try
        {
            if (o == "1")
            {
                string password = GenerateRandomPassword(12);
                Console.WriteLine($"\nGenerated password: {password}");
            }
            else if (o == "2")
            {
                string username = GenerateRandomUsername(8);
                Console.WriteLine($"\nGenerated username: {username}");
            }
            else if (o == "3")
            {
                string email = GenerateRandomEmail(8);
                Console.WriteLine($"\nGenerated email: {email}");
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }
    static string GenerateRandomPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        Random rand = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[rand.Next(s.Length)]).ToArray());
    }
    static string GenerateRandomUsername(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random rand = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[rand.Next(s.Length)]).ToArray());
    }
    static string GenerateRandomEmail(int length)
    {
        string username = GenerateRandomUsername(length);
        string domain = GenerateRandomUsername(length / 2) + ".com";
        return $"{username}@{domain}";
    }
    } // end of class Program
} // end of namespace Multitool

