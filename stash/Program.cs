using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wintervorrat
{
    class Program
    {
        static List<cVogel> meineVögel = new List<cVogel>();
        static string[] dateiinhalt;
        static void Main(string[] args)
        {
            bool beenden = false;
            do
            {
                int Menue = Hauptmenue();

                if (Menue == 1)
                {
                    dateiinhalt = Einlesen();
                }

                if (Menue == 2)
                {
                    Auswertung();
                }

                if (Menue == 3)
                {
                    Ausgabe();
                }

                if (Menue == 4)
                {
                    beenden = true;
                }

                if (Menue < 1 || Menue > 4)
                {
                    Console.WriteLine("Bitte geben Sie nur 1, 2, 3 oder 4 ein!");
                }
            } while (!beenden);

        }

        public static int Hauptmenue()
        {
            Console.WriteLine("Bitte waehlen Sie einen Menüpunkt aus:");
            Console.WriteLine("1: Daten einlesen\n2: Daten auswerten\n3: Ergebnis ausgeben\n4: Beenden");
            int Auswahl = Convert.ToInt32(Console.ReadLine());
            return Auswahl;
        }

        public static string[] Einlesen()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\Finn\Documents\wintervorrat");
            FileInfo[] dateien = di.GetFiles("*.txt");
            for (int i = 0; i < dateien.Length; i++)
            {
                int zeilennummer = i + 1;
                string zeile = zeilennummer.ToString() + ". " + dateien[i].Name;
                Console.WriteLine(zeile);
            }
            Console.WriteLine("BItte wählen Sie eine Datei aus!");
            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;
            string[] DateiAuswahl = File.ReadAllLines(dateien[Auswahl].FullName);
            Console.WriteLine("Daten eingelesen!");
            return DateiAuswahl;
        }

        public static void Auswertung()
        {
            string[] wald = dateiinhalt[0].Split(" ".ToCharArray());
            for (int i = 2; i < dateiinhalt.Length; i++)
            {
                string[] zeile = dateiinhalt[i].Split(' ');
                int startX = int.Parse(zeile[0]);
                int startY = int.Parse(zeile[1]);
                int zeitverzögerung = int.Parse(zeile[2]);
                cVogel tempVogel = new cVogel(startX, startY, zeitverzögerung, zeile[3]);
                meineVögel.Add(tempVogel);
            }
        }

        public static void Ausgabe()
        {
            
        }
    }
}
