using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class MenuCreateRoom : IUI
    {
        private bool isVisable;
        public bool IsVisable { get { return isVisable; } }

        private Image background;

        private Button buttonBack;

        private Label labelNickname;

        private Button buttonCreate;

        public MenuCreateRoom()
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
                Canvas.Instance.AddGUI(new MenuStartGame());
            };

            Canvas.Instance.AddGUI(buttonBack);

            labelNickname = new Label(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64), "Nickname");
            Canvas.Instance.AddGUI(labelNickname);

            buttonCreate = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16), "Создать");
            buttonCreate.Pressed += (obj, e) =>
            {
                if (labelNickname.EnteredText != "")
                {
                    Canvas.Instance.RemoveGUI(this);
                    Game.Instance.CreateRoom(labelNickname.EnteredText);
                }
            };
            Canvas.Instance.AddGUI(buttonCreate);
        }

        public void Update()
        {
            background.Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                     (float)MainWindow.Instance.RenderWindow.Size.Y / 600f);
            buttonBack.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32);
            buttonCreate.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16);
            labelNickname.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64);
        }

        public void Draw()
        {

        }

        public void Removed()
        {
            Canvas.Instance.RemoveGUI(background);
            Canvas.Instance.RemoveGUI(buttonBack);
            Canvas.Instance.RemoveGUI(buttonCreate);
            Canvas.Instance.RemoveGUI(labelNickname);
        }
    }
}
