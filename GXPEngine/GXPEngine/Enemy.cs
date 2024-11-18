using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

public class Enemy : AnimationSprite
{
    GameObject target;
    public Enemy(string filename, int col, int rows, TiledObject obj) : base(filename, col, rows)
    {
   
    }
    void Update()
    {
        Animate();
    }
    public void SetTarget(GameObject pTarget)
    {
        target = pTarget;
    }
}
