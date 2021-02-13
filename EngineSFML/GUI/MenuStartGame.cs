using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;
using SFML.Graphics;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class MenuStartGame : IUI
    {

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } }

        private Image background;

        private Button buttonBack;

        private Button buttonCreateRoom;

        private Button buttonConnect;

        public MenuStartGame()
        {
            isVisable = true;

            background = new Image(new Vector2f(Canvas.Instance.ZeroCoordX, Canvas.Instance.ZeroCoordY), "Resources\\Sprites\\MenuBackground.png")
            {
                Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                    (float)MainWindow.Instance.RenderWindow.Size.Y / 600f)
            };

            Canvas.Instance.AddGUI(background);

            buttonBack = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32), "Назад");
            buttonBack.Pressed += (obj, e) =>
            {
                Canvas.Instance.RemoveGUI(this);
                Canvas.Instance.AddGUI(new MainMenu());
            };

            Canvas.Instance.AddGUI(buttonBack);

            buttonConnect = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16), "Подключиться");
            buttonConnect.Pressed += (obj, e) =>
            {
                Canvas.Instance.RemoveGUI(this);
                Canvas.Instance.AddGUI(new MenuConnect());
            };

            Canvas.Instance.AddGUI(buttonConnect);

            buttonCreateRoom = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64), "Создать");
            buttonCreateRoom.Pressed += (obj, e) =>
            {
                Canvas.Instance.RemoveGUI(this);
                Canvas.Instance.AddGUI(new MenuCreateRoom());
            };

            Canvas.Instance.AddGUI(buttonCreateRoom);
        }

        public void Update()
        {
            background.Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                     (float)MainWindow.Instance.RenderWindow.Size.Y / 600f);
            buttonBack.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32);
            buttonConnect.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16);
            buttonCreateRoom.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64);
        }

        public void Draw()
        {

        }

        public void Removed()
        {
            Canvas.Instance.RemoveGUI(background);
            Canvas.Instance.RemoveGUI(buttonBack);
            Canvas.Instance.RemoveGUI(buttonConnect);
            Canvas.Instance.RemoveGUI(buttonCreateRoom);
        }
    }
}
