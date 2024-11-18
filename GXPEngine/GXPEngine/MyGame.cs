using System;									
using System.Drawing;                           
using GXPEngine;								

public class MyGame : Game
{
    GameStateController gameStateController;

    public MyGame() : base(1280, 720, false, false)
    {
        targetFps = 60;
        gameStateController = new GameStateController();
        AddChild(gameStateController);
    }

	void Update()
    {
        

    }

    static void Main()							
	{
		new MyGame().Start();					
	}
}