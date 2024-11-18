using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Level : GameObject
    {
        /*LevelLoader levelloader;
        loadBackground loadbackground;

        int currentLevel = 1;
        int goNextLevel = 1;
        int levelEndLocation = 25000;*/

        public Level()
        {
            /*if (currentLevel == 1)
            {
                loadbackground = new loadBackground();
                AddChild(loadbackground);

                //Add Levels
                levelloader = new LevelLoader();
                AddChild(levelloader);
            }*/
        }
       /* void CallnewLevel()
		{
			//load background sequence
			loadbackground = new loadBackground();
			AddChild(loadbackground);

			//Add Levels
			levelloader = new LevelLoader();
			AddChild(levelloader);

		}*/

        void Update()
        {
            /*Console.WriteLine(game.x);
            if (game.x <= -levelEndLocation)
            { 
                goNextLevel += 1;
            }

            if (goNextLevel > currentLevel)
            {
                currentLevel += 1;
                CallnewLevel();
            }*/
        }	
    }
}
