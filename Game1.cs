using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace miniRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        public float Speed = 2f;
        bool firstesc = true, egh = true, win;
        int optionsCounter = 1, helth1 = 5, attack = 1, ahero = 4, level = 1, money = 0;
        private Player _player1;
        Texture2D healthTexture;
        Texture2D gameNew;
        Texture2D gameResume;
        Texture2D gameExit;
        Rectangle HealthRectangle;

        List<Monster> monsters = new List<Monster>();
        List<Drak> draks = new List<Drak>();
        List<Tree> trees = new List<Tree>();
        List<Brevno> brevs = new List<Brevno>();
        SpriteFont fontMenu;
        Map map;
        KeyboardState crrKS;
        KeyboardState preKS;

        GameState gameState = GameState.Menu;
        MenuOption option = MenuOption.New;

        int OptionsCounter
        {
            get
            {
                return optionsCounter;
            }
            set
            {
                if (value > 3)
                {
                    optionsCounter = 3;
                }
                else  if (value < 1)
                {
                    optionsCounter = 1;
                }
                else
                {
                    optionsCounter = value;
                }
                if (optionsCounter == 1)
                {
                    option = MenuOption.New;
                }
                else if (optionsCounter == 2)
                {
                    option = MenuOption.Resume;
                }
                else
                {
                    option = MenuOption.Exit;
                }
            }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = ScreenWidth; //Длина экрана
            graphics.PreferredBackBufferHeight = ScreenHeight; // Ширина экрана
            graphics.IsFullScreen = false; // ФуллСкрин
            Window.Title = "TechProg Kurs RPG"; // Название окна
            IsMouseVisible = true; //Видно ли мышку
            Window.AllowUserResizing = true; // Изменение окна мышкой
        }
        static class Shared
        {
            public static readonly Random Random = new Random();
        }
        protected override void Initialize()
        {
            map = new Map();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Monster.LoadContent(Content);
            Drak.LoadContent(Content);
            Tree.LoadContent(Content);
            Brevno.LoadContent(Content);
            Tiles.Content = Content;
            gameNew = Content.Load<Texture2D>("NewGame");
            gameResume = Content.Load<Texture2D>("Resume");
            gameExit = Content.Load<Texture2D>("Exit");
            fontMenu = Content.Load<SpriteFont>("FontMenu");
            _player1 = new Player(Content.Load<Texture2D>("Day/Human"), new Vector2(0, ScreenHeight / 2),150);
            healthTexture = Content.Load<Texture2D>("Health");



        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            crrKS = Keyboard.GetState();
            if (crrKS.IsKeyDown(Keys.Escape) && preKS.IsKeyUp(Keys.Escape))
            {
                if (firstesc == false)
                {
                    firstesc = false;
                    if (gameState == GameState.Game)
                    {
                        gameState = GameState.Menu;
                    }
                    else
                    {
                        gameState = GameState.Game;
                    }
                }
            }
            switch (gameState)
            {
                case GameState.Game:
                    if (egh == true)
                    {
                        int aaa = ScreenWidth / 39 + 1;
                        int bbb = ScreenHeight / 39 + 1;
                        Random ran = new Random();
                        int[,] myArr = new int[bbb, aaa];
                        for (int j = 0; j < bbb; j++)
                        {
                            for (int i = 0; i < aaa; i++)
                            {
                                myArr[j, i] = ran.Next(1, 3);
                            }
                        }
                        map.Generate(myArr, 39);
                        for (int i = 0; i < 4; i++)
                        {
                            float xPosition = Shared.Random.Next(0, ScreenWidth);
                            float yPosition = Shared.Random.Next(0, ScreenHeight);
                            monsters.Add(new Monster(new Vector2(xPosition, yPosition)));
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            float xPosition = Shared.Random.Next(0, ScreenWidth);
                            float yPosition = Shared.Random.Next(0, ScreenHeight);
                            draks.Add(new Drak(new Vector2(xPosition, yPosition)));
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            float xPosition = Shared.Random.Next(0, ScreenWidth);
                            float yPosition = Shared.Random.Next(0, ScreenHeight);
                            trees.Add(new Tree(new Vector2(xPosition, yPosition)));
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            float xPosition = Shared.Random.Next(0, ScreenWidth);
                            float yPosition = Shared.Random.Next(0, ScreenHeight);
                            brevs.Add(new Brevno(new Vector2(xPosition, yPosition)));
                        }
                        egh = false;
                    }
                    foreach (Monster monster in monsters)
                    {
                        monster.Update();
                       
                    }

                    foreach (Drak drak in draks)
                    {
                        drak.Update();
                    }
                    foreach (Tree tree in trees)
                    {
                        tree.Update();
                    }
                    foreach (Brevno brevno in brevs)
                    {
                        brevno.Update();
                    }
                    HealthRectangle = new Rectangle(15, 15, _player1.health, 20);
                    _player1.Update();
                    buy();
                    CheckForCollision();
                    levelUP();
                    break;

                case GameState.Menu:
                    if (crrKS.IsKeyDown(Keys.Up) && preKS.IsKeyUp(Keys.Up))
                    {
                        OptionsCounter--;
                    }
                    if (crrKS.IsKeyDown(Keys.Down) && preKS.IsKeyUp(Keys.Down))
                    {
                        OptionsCounter++;
                    }
                    if (crrKS.IsKeyDown(Keys.Enter) && preKS.IsKeyUp(Keys.Enter))
                    {
                        switch (option)
                        {
                            case MenuOption.New:
                                firstesc = false;
                                StartNewGame();
                                gameState = GameState.Game;
                                break;
                            case MenuOption.Resume:
                                firstesc = false;
                                gameState = GameState.Game;
                                break;
                            case MenuOption.Exit:
                                Exit();
                                break;
                        }
                    }
                    break;
            }
            preKS = crrKS;
            base.Update(gameTime);
        }
        private void StartNewGame()
        {
           helth1 = 5;
           attack = 1;
           ahero = 4;
           level = 1;
           money = 0;
           _player1.Position.X = 0;
           _player1.Position.Y = ScreenHeight / 2;
            _player1.health = 150;
            draks.Clear();
            monsters.Clear();
            trees.Clear();
            brevs.Clear();
            egh = true;
        }
        private void buy()
        {
            Random rnd = new Random();
            if (crrKS.IsKeyDown(Keys.J) && preKS.IsKeyUp(Keys.J))
            {
                if (money >= 5)
                {
                    money -= 5;
                    helth1++;
                    _player1.health += 30;
                }
            }
            if (crrKS.IsKeyDown(Keys.H) && preKS.IsKeyUp(Keys.H))
            {
                if (money >= 3)
                {
                    money -= 3;
                    ahero += rnd.Next(1, 4);
                }
            }
        }
        public void CheckForCollision()
        {
            Random rdn = new Random();
            foreach (Drak drak in draks)
            {
                BoundingBox bb1 = new BoundingBox(new Vector3(_player1.Position.X - (_player1._texture.Width / 2), _player1.Position.Y - (_player1._texture.Height / 2), 0), new Vector3(_player1.Position.X + (_player1._texture.Width / 2), _player1.Position.Y + (_player1._texture.Height / 2), 0));
                BoundingBox bb2 = new BoundingBox(new Vector3(drak._position.X - (Drak._texture1.Width / 2), drak._position.Y - (Drak._texture1.Height / 2), 0), new Vector3(drak._position.X + (Drak._texture1.Width / 2), drak._position.Y + (Drak._texture1.Height / 2), 0));
                if (bb1.Intersects(bb2))
                {
                    if (crrKS.IsKeyDown(Keys.Space) && preKS.IsKeyUp(Keys.Space))
                    {
                        if (ahero < attack)
                        {
                            if (rdn.Next(100) < 40)
                            {
                                win = true;
                            }
                        }
                        else if (ahero == attack)
                        {
                            if (rdn.Next(100) < 50)
                            {
                                win = true;
                            }
                        }
                        else if (ahero > attack)
                        {
                            if (rdn.Next(100) < 70)
                            {
                                win = true;
                            }
                        }
                        if (win == true)
                        {
                            win = false;
                            attack += rdn.Next(1, 3);
                            money++;
                            draks.Remove(drak);//Убираю объект Drak из списка Draks
                            break;
                        }
                        else
                        {
                            helth1--;
                            _player1.health -= 30;
                            if (helth1 == 0)
                            {
                            gameState = GameState.Menu;
                            }
                            break;
                        }
                    
                    }
                }     
            }
            foreach (Monster monster in monsters)
            {
                BoundingBox bb1 = new BoundingBox(new Vector3(_player1.Position.X - (_player1._texture.Width / 2), _player1.Position.Y - (_player1._texture.Height / 2), 0), new Vector3(_player1.Position.X + (_player1._texture.Width / 2), _player1.Position.Y + (_player1._texture.Height / 2), 0));
                BoundingBox bb2 = new BoundingBox(new Vector3(monster._position.X - (Monster._texture1.Width / 2), monster._position.Y - (Monster._texture1.Height / 2), 0), new Vector3(monster._position.X + (Monster._texture1.Width / 2), monster._position.Y + (Monster._texture1.Height / 2), 0));
                if (bb1.Intersects(bb2))
                {
                    if (crrKS.IsKeyDown(Keys.Space) && preKS.IsKeyUp(Keys.Space))
                    {
                        if (ahero < attack)
                        {
                            if (rdn.Next(100) < 40)
                            {
                                win = true;
                            }
                        }
                        else if (ahero == attack)
                        {
                            if (rdn.Next(100) < 50)
                            {
                                win = true;
                            }
                        }
                        else if (ahero > attack)
                        {
                            if (rdn.Next(100) < 70)
                            {
                                win = true;
                            }
                        }
                        if (win == true)
                        {
                            win = false;
                            attack += rdn.Next(1, 3);
                            money++;
                            monsters.Remove(monster);//Убираю объект Drak из списка Draks
                            break;
                        }
                        else
                        {
                            helth1--;
                            _player1.health -= 30;
                            break;
                        }
                    }
                }
            }
        }
        private void levelUP()
        {
            if (draks.Count == 0 & monsters.Count == 0)
            {
                level++;
                draks.Clear();
                monsters.Clear();
                trees.Clear();
                brevs.Clear();
                egh = true;
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Game:
                    map.Draw(spriteBatch);
                    foreach (Monster monster in monsters)
                    {
                        monster.Draw(spriteBatch);
                    }

                    foreach (Drak drak in draks)
                    {
                        drak.Draw(spriteBatch);
                    }
                    foreach (Tree tree in trees)
                    {
                        tree.Draw(spriteBatch);
                    }
                    foreach (Brevno brevno in brevs)
                    {
                        brevno.Draw(spriteBatch);
                    }
                    _player1.Draw(spriteBatch);
                    spriteBatch.Draw(healthTexture, HealthRectangle, Color.White);
                    spriteBatch.DrawString(fontMenu, $"Attack Monster: {attack}", new Vector2(170, 15), Color.DarkRed);
                    spriteBatch.DrawString(fontMenu, $"Attack Hero: {ahero}", new Vector2(320, 15), Color.DarkRed);
                    spriteBatch.DrawString(fontMenu, $"Level: {level}", new Vector2(ScreenWidth/2, 15), Color.DarkRed);
                    spriteBatch.DrawString(fontMenu, $"Money: {money}", new Vector2(ScreenWidth-90, 15), Color.DarkRed);
                    spriteBatch.DrawString(fontMenu, $"Press H ", new Vector2(ScreenWidth - 210, ScreenHeight - 50), Color.Red);
                    spriteBatch.DrawString(fontMenu, $"Press J ", new Vector2(ScreenWidth - 400, ScreenHeight - 50), Color.Red);
                    spriteBatch.DrawString(fontMenu, $"Buy UP Attack Hero = 3 money ", new Vector2(ScreenWidth - 250, ScreenHeight - 30), Color.Black);
                    spriteBatch.DrawString(fontMenu, $"Buy Health = 5 money ", new Vector2(ScreenWidth - 450, ScreenHeight -30), Color.Black);
                    break;
                case GameState.Menu:
                    spriteBatch.Draw(gameNew, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.40)),Color.White);
                    if (option == MenuOption.New)
                    {
                        spriteBatch.Draw(gameNew, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.40)), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(gameNew, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.40)), Color.LightBlue);
                    }
                    if (option == MenuOption.Resume)
                    {
                        spriteBatch.Draw(gameResume, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.50)), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(gameResume, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.50)), Color.LightBlue);
                    }
                    if (option == MenuOption.Exit)
                    {
                        spriteBatch.Draw(gameExit, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.60)), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(gameExit, new Vector2((int)(ScreenWidth * 0.40), (int)(ScreenHeight * 0.60)), Color.LightBlue);
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}