using System;
using System.Collections.Generic;
using System.Text;

using EngineSFML.GameObjects.Entities;
using EngineSFML.GameObjects.Blocks;

using SFML.System;
using SFML.Graphics;

using EngineSFML.Levels;

using EngineSFML.GUI;

namespace EngineSFML.Main
{
    public sealed class Game
    {
        private static readonly Lazy<Game> instance = 
            new Lazy<Game>(() => new Game());
        public static Game Instance { get { return instance.Value; } }

        private bool isStarted;
        public bool IsStarted { get { return isStarted; } }

        private MainWindow mainWindow;
        public MainWindow Window { get { return mainWindow; } }

        private bool isDebug;
        public bool IsDebug { get { return isDebug; } }

        private Level level;
        public Level Level { get { return level; } }

        private Game()
        {
            isStarted = false;

            mainWindow = MainWindow.Instance;

            mainWindow.Update += Update;
            mainWindow.Draw += Draw;

            level = new Level("test");
        }

        private void Init()
        {
            level.LoadLevel();

            Canvas.Instance.AddGUI(new UIDebug());

            Label label = new Label(new Vector2f(300, 32), "WRITE HERE!");
            Canvas.Instance.AddGUI(label);

            Button button = new Button("CLICK HERE!", new Vector2f(300, 128));
            button.Pressed += () => { Console.WriteLine(label.EnteredText); };
            Canvas.Instance.AddGUI(button);
        }
        
        public void Update()
        {
            level.Update();
            Canvas.Instance.Update();
        }

        public void Draw()
        {
            level.Draw();
            Canvas.Instance.Draw();
        }

        public void Start(bool _isDebug)
        {
            if (!isStarted)
            {
                isDebug = _isDebug;
                isStarted = true;
                Init();
                mainWindow.StartLoop();
            }
        }

    }
}
