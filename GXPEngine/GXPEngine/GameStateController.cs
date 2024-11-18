using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

enum GameState
{
    None,
    StartMenu,
    RunLevel,
    EndScreen
}

class GameStateController : GameObject
{
    Menu menu;
    GameState currentState = GameState.None;
    LevelLoader Level;
    Button button;
    GameOver gameover;

    int thisLevel = 0;
    int goNextLevel = 0;
    int levelEndLocation = 15000;
    bool destroyLevel = false;
    bool menuActive = false;
    bool gameRunning = false;
    int Once = 0;
    bool callOnce = false;
    bool GameoverOnce = false;
    HUD hud;

    // -----------------------------------------------------------------------------------------------------------------
    //                                                  GameStateController();
    // -----------------------------------------------------------------------------------------------------------------
    public GameStateController()
    {
        currentState = GameState.StartMenu;
        
        

    }
    void Update()
    {
        Console.WriteLine(callOnce);
        LoadLevel();
        HandleState();
        
        //Hud settings
        if (gameRunning && hud != null)
        {
            updateHud();
            timer();
            hud.getTime(overallTime);
            hud.getPlayer(Level.player);
        }


        
        

    }
    void setHud()
    {
        hud = new HUD();
        
    }

    void updateHud()
    {
        hud.x = -game.x;
        hud.y = -game.y;
        
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  SetState();
    // -----------------------------------------------------------------------------------------------------------------
    public void SetState(GameState state)
    {
        if (currentState != state)
        {
            HandleStateSwitching(currentState, state);
            currentState = state;
        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  HandleStateSwitching();
    // -----------------------------------------------------------------------------------------------------------------
    private void HandleStateSwitching(GameState oldState, GameState newState)
    {
        if(oldState == GameState.EndScreen)
        {
            GameOverMenu();
        }
        if (oldState == GameState.StartMenu)
        {
            DestroyMenu();
        }
        if (newState == GameState.RunLevel)
        {
            gameRunning = true;
            LoadLevel();
            Sound Music;
            Music = new Sound("Background music file.mp3", false, false);
            Music.Play();

        }
        if (oldState == GameState.RunLevel)
        {
            /*DestroyLevel();*/
        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  LoadLevel();
    // -----------------------------------------------------------------------------------------------------------------
    void CallnewLevel()
    {

        if (destroyLevel == true)
        {
            Level.Destroy();
            destroyLevel = false;
        }
        //load level

        Level = new LevelLoader();
        AddChild(Level);
        destroyLevel = true;

        if (gameRunning && callOnce)
        {
            setHud();
            callOnce = false;
        }
        AddChildAt(hud, 400);
    }

    void LoadLevel()
    {
        if (game.x <= -levelEndLocation)
        {
            Console.WriteLine("restart");
            game.x = 0;
            goNextLevel += 1;
        }

        if (goNextLevel > thisLevel)
        {
            thisLevel += 1;
            CallnewLevel();
            
        }
        
    }
    float timeStarted = 0;
    int overallTime = 0;
    void timer()
    {
        if (Input.GetKey(Key.F2))
        {
            Console.WriteLine(timeStarted);
        }

        if (Time.time > timeStarted + 1000)
        {
            overallTime += 1;
            timeStarted = Time.time;
        }
    }

    // -----------------------------------------------------------------------------------------------------------------
    //                                                  DestroyLevel();
    // -----------------------------------------------------------------------------------------------------------------
    /*void DestroyLevel()
    {
        currentLevel.Destroy();
    }*/
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  HandleState();
    // -----------------------------------------------------------------------------------------------------------------
    private void HandleState()
    {
        switch (currentState)
        {
            case GameState.StartMenu:
                HandleStartMenu();
                break;

            case GameState.RunLevel:
                HandleRunLevel();
                LoadLevel();
                break;

            case GameState.EndScreen:
                HandleEndScreen();
                break;
        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  HandleStartMenu();
    // -----------------------------------------------------------------------------------------------------------------
    private void HandleStartMenu()
    {
        if (menuActive == false)
        {
            menu = new Menu();
            AddChild(menu);
            menuActive = true;
            button = new Button();
            AddChild(button);

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (button.HitTestPoint(Input.mouseX, Input.mouseY)){
                callOnce = true;
                SetState(GameState.RunLevel);
            }

        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  HandleRunLevel();
    // -----------------------------------------------------------------------------------------------------------------
    private void HandleRunLevel()
    {
        
        if (Once == 0)
        {
            CallnewLevel();
            StartGame();
            Once = 1;
        }

        if (Level.IsDone() || hud.isDone())
        {
            Level.Destroy();
            SetState(GameState.EndScreen);
        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  HandleEndScreen();
    // -----------------------------------------------------------------------------------------------------------------

    private void HandleEndScreen()
    {
        game.x = 0;
        game.y = 0;
        if (GameoverOnce == false)
        {
            callOnce = false;
            gameover = new GameOver();
            AddChild(gameover);
            button = new Button();
            AddChild(button);
            GameoverOnce = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (button.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                callOnce = true;
                SetState(GameState.RunLevel);

            }
        }
        }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  StartGame();
    // -----------------------------------------------------------------------------------------------------------------
    private void StartGame()
    {
        if (goNextLevel == 0)
        {
            goNextLevel += 1;
            timeStarted = Time.time;
        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    //                                                  DestroyMenu();
    // -----------------------------------------------------------------------------------------------------------------
    private void DestroyMenu()
    {
        menu.Destroy();
    }

    private void GameOverMenu()
    {
        gameover.Destroy();
    }

}
