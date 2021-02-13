using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class MainMenu : IUI
    {

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } }

        private Button buttonPlay;

        private Button buttonSettings;

        private Button buttonExit;

        private Image background;

        public MainMenu()
        {
            isVisable = true;

            background = new Image(new Vector2f(Canvas.Instance.ZeroCoordX, Canvas.Instance.ZeroCoordY), "Resources\\Sprites\\MenuBackground.png")
            {
                Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f, 
                                     (float)MainWindow.Instance.RenderWindow.Size.Y / 600f)
            };

            Canvas.Instance.AddGUI(background);

            buttonPlay = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64), "Играть");
            buttonPlay.Pressed += (obj, e) => 
            {
                Canvas.Instance.RemoveGUI(this);
                Canvas.Instance.AddGUI(new MenuStartGame());
            };

            Canvas.Instance.AddGUI(buttonPlay);

            buttonSettings = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16), "Настройки");
            buttonSettings.Pressed += (obj, e) => 
            {
                Canvas.Instance.RemoveGUI(this);
                Canvas.Instance.AddGUI(new MenuSettings(true));
            };
            Canvas.Instance.AddGUI(buttonSettings);

            buttonExit = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32), "Выйти");
            buttonExit.Pressed += (obj, e) => { Main.MainWindow.Instance.RenderWindow.Close(); };

            Canvas.Instance.AddGUI(buttonExit);
        }

        public void Update()
        {
            buttonPlay.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64);
            buttonSettings.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16);
            buttonExit.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32);
            background.Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                     (float)MainWindow.Instance.RenderWindow.Size.Y / 600f);
        }

        public void Draw()
        {
            
        }

        public void Removed()
        {
            Canvas.Instance.RemoveGUI(background);
            Canvas.Instance.RemoveGUI(buttonPlay);
            Canvas.Instance.RemoveGUI(buttonSettings);
            Canvas.Instance.RemoveGUI(buttonExit);
        }

    }
}
