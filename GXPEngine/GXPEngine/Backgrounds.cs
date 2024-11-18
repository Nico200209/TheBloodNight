using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Background1 : Sprite
{
    public Background1() : base("Background1.png", false, false)
    {
        
    }
}

public class Background2 : Sprite
{
    public Background2() : base("Background2.jpg")
    {
    }
}

public class Background3 : Sprite
{
    public Background3() : base("Background3.jpg")
    {

    }
}


public class loadBackground : GameObject
{
    public loadBackground()
    {
        for (int i = 0; i <= 25600; i += 2560)
        {
            RandomPicture(i);
        }
    }

    void RandomPicture(int pos)
    {
        int id;
        id = Utils.Random(1, 2);
        switch (id)
        {
            case 1:
                Background1 background1 = new Background1();
                AddChild(background1);
                background1.x = pos;
                background1.y = -500;
                break;
        }

    }
}
