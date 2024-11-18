using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using GXPEngine;
public class HUD : Canvas
{
    
    bool isDead = false;

    Heart heart1;
    Heart heart2;
    Heart heart3;

    private Player _player;

    // Time variables
    int oldTime;
    int newTime;
    int currentTime;

    // Time variables
    int hp = 3;
    int oldHealth;
    int newHealth;

    int oldKills;
    int newKills;
    int killStreak;

    public HUD() : base(1280, 720, false)
    {
        setHearts();
    }
    public void getTime(int time)
    {
        newTime = time;
    }
    public void getPlayer(Player player)
    {
        _player = player;
    }

    void Update()
    {
        //current problem is that newHealth is set back to 3 while old health is still 2 so it doesnt update.

        VariableMath();

        Health();
        graphics.Clear(Color.Empty);
        Font font = new Font("Arial", 25);
        graphics.DrawString("Time: " + currentTime.ToString(), font, Brushes.White, 20, 130);
        graphics.DrawString("Enemies killed: " + killStreak.ToString(), font, Brushes.White, 20, 170);
    }

    private void VariableMath()
    {
        newHealth = _player.Health();


        if (newHealth < oldHealth)
        {
            hp = hp - 1;
            oldHealth = newHealth;
        }
        oldHealth = newHealth;

        if (oldTime < newTime)
        {
            currentTime = currentTime + 1;
            oldTime = newTime;
        }

        newKills = _player.enemiesKilled();
        if (newKills > oldKills)
        {
            killStreak = killStreak + 1;
            oldKills = newKills;
        }
        oldKills = newKills;


    }

    void Health()
    {
        if (hp == 2)
        {
            heart3.LateDestroy();
        }
        else if (hp == 1)
        {
            heart2.LateDestroy();
        } 
        else if (hp == 0)
        {
            isDead = true;
        }
    }

    private void setHearts()
    {
        if (hp == 3)
        {
            heart1 = new Heart();
            heart2 = new Heart();
            heart3 = new Heart();

            heart1.SetXY(10, 10);
            heart2.SetXY(130, 10);
            heart3.SetXY(250, 10);

            LateAddChild(heart1);
            LateAddChild(heart2);
            LateAddChild(heart3);
        }
    }

    public bool isDone()
    {
        if (hp == 0) return true; else return false;
    }

    public bool NoHearts()
    {
        if (isDead) return true; else return false;
    }
}

