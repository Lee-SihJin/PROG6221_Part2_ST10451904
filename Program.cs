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

        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
        {
            { "password", new List<string> { "Use strong, unique passwords and enable two-factor authentication.",
                                             "Avoid using personal information in your passwords.",
                                             "Consider using a password manager to keep track of your credentials." } },
            { "scam", new List<string> { "Be cautious of unsolicited emails requesting personal info.",
                                             "Verify the source before clicking on suspicious links." } },
            { "phishing", new List<string> { "Be cautious of emails asking for personal information.",
                                             "Scammers may pose as trusted organizations—always verify links.",
                                             "Do not click on suspicious links or attachments." } },
            { "privacy", new List<string> { "Adjust your social media privacy settings for better protection.",
                                           "Limit the amount of personal information you share online.",
                                           "Use secure and encrypted messaging apps." } },
        };

        static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
        {
            { "worried", "It's completely understandable to feel that way, " + userName + ". Let me share some tips to help you stay safe." },
            { "curious", "Curiosity is great, " + userName + "! Let's dive into the topic together." },
            { "frustrated", "Don't worry, " + userName + "— these concepts can be tricky. I'm here to help!" }
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
            TypeEffect($"\nWelcome, {userName}! Let's explore how to stay safe online together.");
        }

        static void ResponseSystem()
        {
            Random rnd = new Random();
             
            
            while (true)
            {
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeEffect("\nAsk me a question:(or enter <bye>,<exit>,<quit> to exit) ");
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
                // Keyword recognition
                foreach (var keyword in keywordResponses.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        string response = keywordResponses[keyword][rnd.Next(keywordResponses[keyword].Count)];
                        Console.ForegroundColor = ConsoleColor.Green;
                        TypeEffect(response);
                        Console.ResetColor();

                        userInterest = keyword; // Save user interest
                        foundMatch = true;
                    }
                }

                // Sentiment detection
                foreach (var sentiment in sentimentResponses.Keys)
                {
                    if (input.Contains(sentiment))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TypeEffect(sentimentResponses[sentiment].Replace(userName, userName)); // Use name
                        Console.ResetColor();
                        foundMatch = true;
                    }
                }

                // Memory recall
                if (input.Contains("tell me more") && !string.IsNullOrWhiteSpace(userInterest))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeEffect($"{userName}, since you're interested in {userInterest}, here's a tip: Always update your apps to patch security vulnerabilities.");
                    Console.ResetColor();
                    foundMatch = true;
                }

                // Fallback
                if (!foundMatch)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeEffect("I'm not sure I understand. Can you rephrase or ask something about cybersecurity?");
                    Console.ResetColor();
                }

                // Respond to "what's my name?" queries
                if (input.Contains("name"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    if (!string.IsNullOrWhiteSpace(userName))
                        TypeEffect($"Just kidding! Your name is {userName}! I didn’t forget. ");
                    else
                        TypeEffect("I don’t think you told me your name yet.");
                    Console.ResetColor();
                    foundMatch = true;
                }



            }
        }
    }
}
