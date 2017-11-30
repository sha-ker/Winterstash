
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WintervorratWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<cVogel> meineVögel = new List<cVogel>();
        static List<cFeld> meineFelder = new List<cFeld>();
        static List<Rectangle> _rectangles = new List<Rectangle>();
        int waldLänge;
        int waldBreite;
        bool eingelesen = false;
        bool ausgewertet = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miEnde_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void miLaden_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select a Text File";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] dateiinhalt = File.ReadAllLines(openFileDialog1.FileName);
                
                string[] wald = dateiinhalt[0].Split(" ".ToCharArray());
                waldBreite = int.Parse(wald[0]);
                waldLänge = int.Parse(wald[1]);
                for (int i = 1; i <= waldBreite; i++)
                {
                    for (int j = 1; j <= waldLänge; j++)
                    {
                        cFeld tempFeld = new cFeld(i, j);
                        meineFelder.Add(tempFeld);
                    }
                }
                for (int i = 2; i < dateiinhalt.Length; i++)
                {
                    string[] zeile = dateiinhalt[i].Split(' ');
                    int startX = int.Parse(zeile[0]);
                    int startY = int.Parse(zeile[1]);
                    int startminute = int.Parse(zeile[2]);
                    cVogel tempVogel = new cVogel(startX, startY, startminute, zeile[3]);
                    meineVögel.Add(tempVogel);
                }
            }
            eingelesen = true;
        }

        private void miAuswerten_Click(object sender, RoutedEventArgs e)
        {
            for (int zeitindex = 1; zeitindex <= 12*60; zeitindex++)
            {
                foreach (cVogel Vogel in meineVögel)
                {
                    if (zeitindex >= Vogel.Startminute)
                    {
                        Vogel.Bewegen(waldBreite, waldLänge);
                    }
                }
                foreach (cFeld Feld in meineFelder)
                {
                    int stk = meineVögel.Count(vogel => vogel.StartX == Feld.PosX && vogel.StartY == Feld.PosY);
                    if (stk> 0)
                    {
                        if (Feld.SichereMinuten >= 30)
                        {
                            Feld.Sicher = true;
                            int sicherVon = zeitindex - Feld.SichereMinuten;
                            int sicherBis = zeitindex;
                            string sicherVonBis = Convert.ToString(sicherVon) + ' ' + Convert.ToString(sicherBis);
                            Feld.SichereZeiträume.Add(sicherVonBis);
                        }
                        Feld.SichereMinuten = 0;
                    }
                    else
                    {
                        Feld.SichereMinuten++;
                    }
                }
            }
            foreach (cFeld Feld in meineFelder)
            {
                if (Feld.SichereMinuten == 12*60)
                {
                    Feld.AbsolutSicher = true;
                }
            }
            ausgewertet = true;
        }

        private void miAusgeben_Click(object sender, RoutedEventArgs e)
        {
            if (ausgewertet == true && eingelesen == true)
            {
                double seitenlänge = grafik.Width / waldBreite;
                double seitenlängeY = grafik.Height / waldLänge;
                if (seitenlängeY < seitenlänge)
                {
                    seitenlänge = seitenlängeY;
                }
                for (int i = 0; i <= waldBreite; i++)
                {
                    Line tempLine = new Line();
                    tempLine.Stroke = Brushes.Black;
                    tempLine.X1 = i * seitenlänge;
                    tempLine.Y1 = grafik.Height;
                    tempLine.X2 = i * seitenlänge;
                    tempLine.Y2 = grafik.Height - seitenlänge * waldLänge;
                    tempLine.StrokeThickness = 2;
                    Canvas.SetZIndex(tempLine, 2);
                    grafik.Children.Add(tempLine);
                }
                for (int i = 0; i <= waldLänge; i++)
                {
                    Line tempLine = new Line();
                    tempLine.Stroke = Brushes.Black;
                    tempLine.X1 = 0;
                    tempLine.Y1 = grafik.Height - i * seitenlänge;
                    tempLine.X2 = seitenlänge * waldBreite;
                    tempLine.Y2 = grafik.Height - i * seitenlänge;
                    tempLine.StrokeThickness = 2;
                    Canvas.SetZIndex(tempLine, 2);
                    grafik.Children.Add(tempLine);
                }
                foreach (cFeld Feld in meineFelder)
                {
                    Rectangle tempRect = new Rectangle();
                    tempRect.Stroke = Brushes.White;
                    tempRect.StrokeThickness = seitenlänge * 0.2;
                    tempRect.Width = seitenlänge;
                    tempRect.Height = seitenlänge;
                    Canvas.SetBottom(tempRect, (Feld.PosY - 1) * seitenlänge);
                    Canvas.SetLeft(tempRect, (Feld.PosX - 1) * seitenlänge);
                    Canvas.SetZIndex(tempRect, 1);
                    if (Feld.Sicher == true)
                    {
                        string[] zeile = Feld.SichereZeiträume[0].Split(' ');
                        int sicherVon = int.Parse(zeile[0]);
                        int sicherBis = int.Parse(zeile[1]);
                        string ausgabe = "Das Feld x: " + Feld.PosX + " y: " + Feld.PosY + " ist sicher von Minute " + sicherVon + " bis Minute " + sicherBis + " !";
                        textAusgabe.Items.Add(ausgabe);
                        tempRect.Fill = Brushes.Yellow;
                    }
                    else if (Feld.AbsolutSicher == true)
                    {
                        string ausgabe = "Das Feld x: " + Feld.PosX + " y: " + Feld.PosY + " ist absolut sicher!";
                        textAusgabe.Items.Add(ausgabe);
                        tempRect.Fill = Brushes.Green;
                    }
                    else
                    {
                        tempRect.Fill = Brushes.DarkRed;
                    }
                    grafik.Children.Add(tempRect);
                }
            }
        }
    }
}
