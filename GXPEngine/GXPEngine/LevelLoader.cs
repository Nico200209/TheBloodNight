using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

public class LevelLoader : GameObject
{
    Pivot[] layers;

    public Player player;
    /*public HUD hud;*/

    public LevelLoader() : base()
    {
        RandomNextLevel();
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        player = new Player();
        AddChild(player);

        /*hud = new HUD(player);
        
        AddChild(hud);*/
    }

    void Update()
    {
        for (int i=0;i<layers.Length;i++) {
            Pivot piv = layers[i];
            float layerSpeed = 0.5f + 0.25f * i;
            if (Input.GetKey(Key.F1))
                Console.WriteLine("Layer {0} speed: {1}",i,layerSpeed);
            piv.x = game.width / 5 - player.x * layerSpeed;
        }
        /*hud.x = -game.x;
        hud.y = -game.y;*/
    }

    public bool IsDone()
    {
        if (player.IsDead()) return true; else return false;
    }

    void LoadLevel(string filename)
    {
        TiledLoader loader = new TiledLoader(filename, this);
        loader.OnObjectCreated += ObjectCallBack;
        loader.addColliders = false;

        int numLayers = loader.NumImageLayers;
        layers = new Pivot[numLayers];
        
        for (int i = 0; i < numLayers; i++)
        {
            layers[i] = new Pivot();
            AddChild(layers[i]);
            loader.rootObject = layers[i];

            loader.LoadImageLayers(i);
            if (true)
            {
                Map map = loader.map;
                string imagename = map.ImageLayers[i].Image.FileName;

                ScrollingBackground bg = new ScrollingBackground(imagename);
                layers[i].AddChild(bg);

                bg = new ScrollingBackground(imagename);
                layers[i].AddChild(bg);
                bg.x = bg.width;
            }
        }

        //load floor
        loader.addColliders = true;
        loader.LoadTileLayers();

        //load collision level

        loader.addColliders = false;
        loader.autoInstance = true;
        loader.LoadObjectGroups();

        Enemy[] enemies = FindObjectsOfType<Enemy>();
    }

    void RandomNextLevel()
    {
        int id;
        id = Utils.Random(1, 4);
        switch (id)
        {
            case 1:
                LoadLevel("Level2.tmx");
                break;
            case 2:
                LoadLevel("Level3.tmx");
                break;
            case 3:
                LoadLevel("Level4.tmx");
                break;

        }
    }
    void ObjectCallBack(Sprite sprite, TiledObject obj)
    {
        Console.WriteLine("Created a " + sprite + " from" + obj);
    }
}
