using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace Chatbot
{
    internal class Program
    {
        static string userName = "";
        static string userInterest = "";

        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> { "Use strong, unique passwords for each account.", "Avoid personal details in your passwords.", "Enable two-factor authentication for extra security." } },
            { "scam", new List<string> { "Be cautious of unsolicited emails requesting personal info.", "Verify the source before clicking on suspicious links." } },
            { "privacy", new List<string> { "Review your privacy settings on all social media accounts.", "Limit personal information shared online." } },
            { "phishing", new List<string> { "Watch out for urgent requests in emails.", "Verify email sender addresses before trusting links.", "Never enter personal info on untrusted websites." } },
        };

        static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>()
        {
            { "worried", "It's okay to feel worried. Small cybersecurity steps make a big difference!" },
            { "curious", "Curiosity is great! Let's keep learning together." },
            { "frustrated", "I understand it can be frustrating. I'm here to help you step by step." }
        };

        static void Main(string[] args)
        {
            // Play greeting audio
            SoundPlayer player = new SoundPlayer(@"C:\Users\lab_services_student\Desktop\Chatbot2\PROG6221_Part2_ST10451904\08pgn-1x0pn.wav");
            player.PlaySync();

            TypeEffect("Starting Cybersecurity Awareness Bot...");
            DisplayAsciiArt();
            TextGreeting();
            ResponseSystem();
        }

        static void TypeEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        static void DisplayAsciiArt()
        {
            string asciiArt = @"
  ____      _                                        _ _            
 / ___|   _| |__   ___ _ __ ___  ___  ___ _   _ _ __(_) |_ _   _    
| |  | | | | '_ \ / _ \ '__/ __|/ _ \/ __| | | | '__| | __| | | |   
| |__| |_| | |_) |  __/ |  \__ \  __/ (__| |_| | |  | | |_| |_| |   
 \____\__, |_.__/ \___|_|  |___/\___|\___|\__,_|_|  |_|\__|\__, |   
   / \|___/   ____ _ _ __ ___ _ __   ___  ___ ___  | __ )  |___/ |_ 
  / _ \ \ /\ / / _` | '__/ _ \ '_ \ / _ \/ __/ __| |  _ \ / _ \| __|
 / ___ \ V  V / (_| | | |  __/ | | |  __/\__ \__ \ | |_) | (_) | |_ 
/_/   \_\_/\_/ \__,_|_|  \___|_| |_|\___||___/___/ |____/ \___/ \__| 
    ";
            Console.WriteLine(asciiArt);
        }

        static void TextGreeting()
        {
            TypeEffect("\nWhat's your name? ");
            userName = Console.ReadLine();

            TypeEffect($"\nWelcome, {userName}! What's your favourite cybersecurity topic? ");
            userInterest = Console.ReadLine();

            TypeEffect($"Great! I'll remember you're interested in {userInterest}.");
        }

        static void ResponseSystem()
        {
            Random rnd = new Random();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeEffect("\nAsk me a question: (or enter <bye>,<exit>,<quit> to exit)");
                string input = Console.ReadLine()?.ToLower();
                Console.ResetColor();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeEffect("I didn't quite understand that. Could you rephrase?(or enter <bye>,<exit>,<quit> to exit)");
                    Console.ResetColor();
                    continue;
                }

                if (input.Contains("exit") || input.Contains("quit") || input.Contains("bye"))
                {
                    TypeEffect($"Thank you for chatting, {userName}! Stay safe online.");
                    break;
                }

                bool foundMatch = false;

                //Keyword Recognition: Output all matching keywords
                foreach (var keyword in keywordResponses.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        string response = keywordResponses[keyword][rnd.Next(keywordResponses[keyword].Count)];
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypeEffect(response);
                        Console.ResetColor();
                        foundMatch = true;
                    }
                }

                //  Sentiment Detection: Output all matching sentiments
                foreach (var sentiment in sentimentResponses.Keys)
                {
                    if (input.Contains(sentiment))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TypeEffect(sentimentResponses[sentiment]);
                        Console.ResetColor();
                        foundMatch = true;
                    }
                }

                // 🧠 Memory Recall follow-up
                if (input.Contains("tell me more"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeEffect($"Since you're interested in {userInterest}, here's a tip: Always update your apps to patch security vulnerabilities.");
                    Console.ResetColor();
                    foundMatch = true;
                }

                // ❓ Default fallback if nothing was recognized
                if (!foundMatch)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeEffect("I'm not sure I understand. Can you rephrase or ask something about cybersecurity?");
                    Console.ResetColor();
                }
            }
        }

    }
}


