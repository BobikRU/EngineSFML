using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;
using SFML.Graphics;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class MenuConnect : IUI
    {
        private bool isVisable;
        public bool IsVisable { get { return isVisable; } }

        private Image background;

        private Button buttonBack;

        private Label labelIp;

        private Label labelNickname;

        private Button buttonConnect;

        public MenuConnect()
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

            labelIp = new Label(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64), "IP-адрес");
            Canvas.Instance.AddGUI(labelIp);

            labelNickname = new Label(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16), "Nickname");
            Canvas.Instance.AddGUI(labelNickname);

            buttonConnect = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 + 128, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16), "Подключиться");
            buttonConnect.Pressed += (obj, e) => 
            {
                if (labelIp.EnteredText != "" && labelNickname.EnteredText != "")
                {
                    Canvas.Instance.RemoveGUI(this);
                    Game.Instance.ConnectRoom(labelNickname.EnteredText, labelIp.EnteredText);
                }
            };

            Canvas.Instance.AddGUI(buttonConnect);
        }

        public void Update()
        {
            background.Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                     (float)MainWindow.Instance.RenderWindow.Size.Y / 600f);
            buttonBack.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32);
            labelIp.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 64);
            labelNickname.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16);
            buttonConnect.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 + 128, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 16);
        }

        public void Draw()
        {

        }

        public void Removed()
        {
            Canvas.Instance.RemoveGUI(background);
            Canvas.Instance.RemoveGUI(buttonBack);
            Canvas.Instance.RemoveGUI(labelIp);
            Canvas.Instance.RemoveGUI(labelNickname);
            Canvas.Instance.RemoveGUI(buttonConnect);
        }
    }
}
