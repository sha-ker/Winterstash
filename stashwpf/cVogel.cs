using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WintervorratWPF
{
    public class cVogel
    {
        int startX;
        int startY;
        int startminute;
        string richtung;

        public int Startminute
        {
            get
            {
                return startminute;
            }
        }

        public int StartY
        {
            get
            {
                return startY;
            }
        }

        public int StartX
        {
            get
            {
                return startX;
            }
        }

        public cVogel(int _startX, int _startY, int _startminute, string _richtung)
        {
            startminute = _startminute;
            richtung = _richtung;

                if (richtung == "N")
                {
                startY = _startY - 1;
                startX = _startX;
                }
                if (richtung == "S")
                {
                startY = _startY + 1;
                startX = _startX;
                }
                if (richtung == "W")
                {
                startX = _startX + 1;
                startY = _startY;
                }
                if (richtung == "O")
                {
                startX = _startX - 1;
                startY = _startY;
                }
        }

        public void Bewegen(int maxX, int maxY)
        {
            if (richtung == "N")
            {
                startY += 1;
                if (StartY > maxY)
                {
                    richtung = "S";
                    startY -= 2;
                }
            }
            else if (richtung == "S")
            {
                startY -= 1;
                if (StartY < 1)
                {
                    richtung = "N";
                    startY += 2;
                }
            }
            else if (richtung == "W")
            {
                startX -= 1;
                if (StartX < 1)
                {
                    richtung = "O";
                    startX += 2;
                }
            }
            else if (richtung == "O")
            {
                startX += 1;
                if (StartX > maxX)
                {
                    richtung = "W";
                    startX -= 2;
                }
            }
        }
    }
}
