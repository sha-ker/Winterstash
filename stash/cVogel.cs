using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wintervorrat
{
    public class cVogel
    {
        int startX;
        int startY;
        int zeitverzögerung;
        string richtung;

        public cVogel(int _startX, int _startY, int _zeitverzögerung, string _richtung)
        {
            startX = _startX;
            startY = _startY;
            zeitverzögerung = _zeitverzögerung;
            richtung = _richtung;

        }
    }
}
