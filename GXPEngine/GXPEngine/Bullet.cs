using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
public class Bullet : Sprite
{

    int bulletspeed;
    int enemiesKilled = 0;


    public Bullet(string filename, int bulspeed) : base(filename)
    {
        SetScaleXY(2);
        SetOrigin(width / 2, height / 2);
        bulletspeed = bulspeed;
    }

    void Update()
    {
        Collide();
        
        Vector2 screenPosition = TransformPoint(0, 0);
        if (screenPosition.x < -width || screenPosition.x > game.width + width || screenPosition.y < -height || screenPosition.y > game.height + height)
        {
            Destroy();
        }
    }

    public int Killcount()
    {
        return enemiesKilled;
    }

    void Collide()
    {
        Collision Hi = MoveUntilCollision(15, 0);
        if (Hi != null && Hi.other is Enemy)
        {
            Hi.other.LateDestroy();
            LateDestroy();
            enemiesKilled = enemiesKilled + 1;
            Sound Die;
            Die = new Sound("Flying dying.wav", false, false);
            Die.Play();
        }
        if (Hi != null)
        {
            LateDestroy();
        }
    }
}

