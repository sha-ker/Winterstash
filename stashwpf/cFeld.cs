using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WintervorratWPF
{
    public class cFeld
    {
        int posX;
        int posY;
        int sichereMinuten;
        bool sicher;
        bool absolutSicher;
        List<string> sichereZeiträume = new List<string>();

        public cFeld(int _posX, int _posY)
        {
            posX = _posX;
            posY = _posY;
            sichereMinuten = 0;
            sicher = false;
            absolutSicher = false;
        }

        public int PosX
        {
            get
            {
                return posX;
            }

        }

        public int PosY
        {
            get
            {
                return posY;
            }

        }

        public int SichereMinuten
        {
            get
            {
                return sichereMinuten;
            }

            set
            {
                sichereMinuten = value;
            }
        }

        public bool Sicher
        {
            get
            {
                return sicher;
            }

            set
            {
                sicher = value;
            }
        }

        public bool AbsolutSicher
        {
            get
            {
                return absolutSicher;
            }

            set
            {
                absolutSicher = value;
            }
        }

        public List<string> SichereZeiträume
        {
            get
            {
                return sichereZeiträume;
            }

            set
            {
                sichereZeiträume = value;
            }
        }
    }
}
