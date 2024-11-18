using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core; //for Vector2

public class ScrollingBackground : Sprite
{
    public ScrollingBackground(string filename) : base(filename, true, false)
    {
        
    }

    void Update()
    {
        // check if outside screen:
        Vector2 topright = TransformPoint(width, 0); // the screen position of the top right point of the sprite
        if (topright.x<0) // outside of screen to the left?
        {
            x += width * 2; // then move
        }
    }
}

