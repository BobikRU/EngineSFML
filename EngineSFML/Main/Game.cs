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
        }

        private void Init()
        {
            Canvas.Instance.AddGUI(new MainMenu());
        }
        
        public void Update()
        {
            if (level != null)
                level.Update();
            Canvas.Instance.Update();
        }

        public void Draw()
        {
            if (level != null)
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
