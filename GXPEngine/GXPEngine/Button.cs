using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Button : Sprite
    {
        public Button() : base("colors.png")
        {
            SetXY(545, 630);
            SetScaleXY(3, 1);
        }
    }
}
