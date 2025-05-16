using DiscordRPC.Logging;
using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magisterka2
{
    public class Discord
    {
        static DiscordRpcClient client;
        public static void Initialise()
        {
            // Podaj swój Client ID z Discord Developer Portal
            client = new DiscordRpcClient("1369010254985171036");

            // Opcjonalnie: ustaw logger do debugowania
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            // Zarejestruj event Ready
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Rich Presence połączone z Discordem jako " + e.User.Username);
            };

            // Połącz się z Discordem
            client.Initialize();

            // Ustaw Rich Presence
            SetRichPresence("Przetwarzam dane w aplikacji", "Wczytywanie baz danych");
        }

        public static void SetRichPresence(string detail, string state)
        {
            client.SetPresence(new RichPresence()
            {
                Details = detail,
                State = state,
                Timestamps = Timestamps.Now, // licznik czasu od teraz
            });
        }

        public static void SetRichPresence(string detail,string state, int current, int max)
        {
            client.SetPresence(new RichPresence()
            {
                Details = detail,
                State = state,
                Timestamps = Timestamps.Now, // licznik czasu od teraz
                Party = new Party()
                {
                    ID = "przetwarzaniedanychdomagisterki",
                    Size = current,
                    Max = max,
                }
            });
        }
        public static void SetRichPresenceUpdate(string detail, string state, int current, int max)
        {
            client.SetPresence(new RichPresence()
            {
                Details = detail,
                State = state,
                Party = new Party()
                {
                    ID = "przetwarzaniedanychdomagisterki",
                    Size = current,
                    Max = max,
                }
            });
        }
    }
}
