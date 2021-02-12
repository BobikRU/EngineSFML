using System;
using System.Collections.Generic;
using System.Text;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class MenuSettings : IUI
    {
        private bool isVisable;
        public bool IsVisable { get { return isVisable; } }

        // called by main menu
        private bool isMenu;

        private Image background;

        private Button buttonBack;

        private Slider sliderResolution;

        private CheckBox checkBoxFullscreen;

        private Text text;

        public MenuSettings(bool _isMenu)
        {
            isVisable = true;
            isMenu = _isMenu;

            background = new Image(new Vector2f(Canvas.Instance.ZeroCoordX, Canvas.Instance.ZeroCoordY), "Resources\\Sprites\\MenuBackground.png")
            {
                Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                    (float)MainWindow.Instance.RenderWindow.Size.Y / 600f)
            };

            if (!isMenu)
                background.Color = new Color(Color.White) { A = 128 };

            Canvas.Instance.AddGUI(background);

            buttonBack = new Button(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32), "Назад");
            buttonBack.Pressed += (obj, e) => 
            { 
                if (isMenu) 
                {
                    Canvas.Instance.RemoveGUI(this);
                    Canvas.Instance.AddGUI(new MainMenu());
                }
                else
                {
                    Canvas.Instance.RemoveGUI(this);
                }
            };

            Canvas.Instance.AddGUI(buttonBack);

            string[] resolutions = new string[VideoMode.FullscreenModes.Length];
            VideoMode[] videoModes = VideoMode.FullscreenModes;
            int currentResolution = videoModes.Length - 1;
            for (int i = 0; i < VideoMode.FullscreenModes.Length; ++i)
            {
                currentResolution = (MainWindow.Instance.RenderWindow.GetView().Size.X == videoModes[i].Width && MainWindow.Instance.RenderWindow.GetView().Size.Y == videoModes[i].Height) ? i : currentResolution;
                resolutions[i] = videoModes[i].Width + "x" + videoModes[i].Height;
            }
            sliderResolution = new Slider(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 96), resolutions, currentResolution);
            Canvas.Instance.AddGUI(sliderResolution);

            sliderResolution.HasChanged += (obj, e) => 
            {
                Settings.Instance.SetConfig("window.width", "" + videoModes[e.variant].Width);
                Settings.Instance.SetConfig("window.height", "" + videoModes[e.variant].Height);
            };

            checkBoxFullscreen = new CheckBox(new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 8), "Fullscreen", bool.Parse(Settings.Instance.GetConfig("window.fullscreen")));
            Canvas.Instance.AddGUI(checkBoxFullscreen);

            checkBoxFullscreen.HasChanged += (obj, e) =>
            {
                Settings.Instance.SetConfig("window.fullscreen", e.isChecked.ToString());
            };

            text = new Text("(ДЛЯ ПРИМЕНЕНИЯ НАСТРОЕК ТРЕБУЕТСЯ ПЕРЕЗАПУСК)", Canvas.Instance.font)
            {
                CharacterSize = 14,
                Position = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 160, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 14)
        };
        }

        public void Draw()
        {
            MainWindow.Instance.RenderWindow.Draw(text);
        }

        public void Removed()
        {
            Canvas.Instance.RemoveGUI(background);
            Canvas.Instance.RemoveGUI(buttonBack);
            Canvas.Instance.RemoveGUI(sliderResolution);
            Canvas.Instance.RemoveGUI(checkBoxFullscreen);
        }

        public void Update()
        {
            if (isMenu)
                background.Scale = new Vector2f((float)MainWindow.Instance.RenderWindow.Size.X / 800f,
                                     (float)MainWindow.Instance.RenderWindow.Size.Y / 600f);
            buttonBack.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 32);
            sliderResolution.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 96, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 96);
            checkBoxFullscreen.Pos = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 64, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 - 8);
            text.Position = new Vector2f(Canvas.Instance.ZeroCoordX + MainWindow.Instance.RenderWindow.Size.X / 2 - 160, Canvas.Instance.ZeroCoordY + MainWindow.Instance.RenderWindow.Size.Y / 2 + 14);
        }
    }
}
