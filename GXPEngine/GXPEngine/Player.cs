using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
using GXPEngine.Core;

public class Player : AnimationSprite
{
    float speedY;
    float speedX = 2;
    bool grounded = false;
    int nextShootTimeMs = 0;
    int shootIntervalMs = 700;
    bool dead = false;
    int hp = 3;
    Bullet bullet;
    float lowestPoint = 1030f;

    int oldKills;
    int newKills;
    int killStreak;


    public Player() : base("FinalCharacter.png", 5, 5)
    {
        SetXY(300, 1000);
        SetOrigin(width / 2, height);
        SetCycle(0, 12, 5);
    }
    public int enemiesKilled()
    {
        
        return killStreak;
        
    }

    public int Health()
    {
        return hp;
    }

    public bool IsDead()
    {
        if (dead == true) return true; else return false;
    }


    void Update()
    {
        //Animate(); Is for GXPEngine to start the animation set in Tiled
        //If we want to change the frames, we can either do it with the notes or inside Tiled
        KillstreakMath();

        Animate();
        Shoot();
        Movement();
       

        //Start Camera follow player
        game.x = game.width / 5 - this.x * scaleX;

        if (this.y <= 1170)
        {
            game.y = game.height / 2 - this.y * scaleY;
        }

        

        //End camera follow player
        Death();
        Collide();

        if (Input.GetKey(Key.F))
        {
            Console.WriteLine(game.y - 720);
        }


    }

    void KillstreakMath()
    {
        if (bullet != null) 
        { 
            newKills = bullet.Killcount();
            if (newKills > oldKills)
            {
                killStreak = killStreak + 1;
                oldKills = newKills;
            }
            oldKills = newKills;
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(Key.SPACE) && Time.time > nextShootTimeMs)
        {
            bullet = new Bullet("bullet.png", 25);
            parent.AddChild(bullet);
            bullet.SetXY(x + 170, y - 64);
            SetCycle(12, 12, 3);
            nextShootTimeMs = Time.time + shootIntervalMs;
            Sound Dispara;
            Dispara = new Sound("Shooting.wav", false, false);
            Dispara.Play();
        }
        if (Input.GetKeyUp(Key.SPACE))
        {
            SetCycle(0, 12, 5);
        }
    }
    void Movement()
    {
        if (speedY <= 25)
        {
            speedY = speedY + 1;
        }
        Collision col = MoveUntilCollision(0, speedY);
        if (col != null && col.other is Enemy) 
        {
            col.other.LateDestroy();
            hp = hp - 1;
        }

        if (col != null)
        {
            speedY = 0;
            if (col.normal.y < 0)
            {
                grounded = true;
            }
            else
            {
               
                grounded = false;
            }
        }
        else
        { 
            grounded = false;
        }

        if (Input.GetKeyDown(Key.W) && grounded== true)
        {
            speedY = -25;
            SetCycle(24, 1, 0);
            Sound Saltar;
            Saltar = new Sound("Jumping.wav", false, false);
            Saltar.Play();
        }
        if (Input.GetKeyUp(Key.W))
        {
            SetCycle(0, 12, 5);
        }

    }
    void Collide()
    {
        Collision Hi = MoveUntilCollision(speedX, 0);
        if (Hi != null && Hi.other is Enemy)
        {
            Hi.other.LateDestroy();
            hp = hp - 1;
        }
        if(Hi != null)
        {
            speedX = 0;
            speedY = speedY + 1;
        }
        speedX = 7;
    }



    void Death()
    {
        if (this.y >= 1472)
        {
            dead = true;
        }
    }
}
