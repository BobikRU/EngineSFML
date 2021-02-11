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

            CheckBox checkBox = new CheckBox(new Vector2f(300, 90), "reverse");
            Canvas.Instance.AddGUI(checkBox);

            string[] variants = { "Nothing", "to lower", "to upper" };
            Slider slider = new Slider(new Vector2f(460, 90), variants);
            Canvas.Instance.AddGUI(slider);

            Button button = new Button("CLICK HERE!", new Vector2f(300, 128));
            button.Pressed += () => 
            {
                string oString = "";
                
                if (checkBox.IsChecked)
                {
                    for (int i = label.EnteredText.Length - 1; i >= 0; --i)
                        oString += label.EnteredText[i];
                }
                else
                    oString = label.EnteredText;

                if (slider.SelectedVariant == 0)
                    Console.WriteLine(oString);
                else if (slider.SelectedVariant == 1)
                    Console.WriteLine(oString.ToLower());
                else if (slider.SelectedVariant == 2)
                    Console.WriteLine(oString.ToUpper());
            };
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
