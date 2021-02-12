using System;
using System.Collections.Generic;
using System.Text;

using EngineSFML.Main;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace EngineSFML.GUI
{
    public class Button : IUI
    {

        private readonly Color freeColor = Color.White;

        private readonly Color enteredColor;

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } set { isVisable = value; } }

        private Texture texture;
        private Sprite sprite;

        public Vector2f Pos { get { return sprite.Position; } set { sprite.Position = value; } }

        private string stringText;

        private Text text;

        private bool isEntered;

        public event EventHandler Pressed;

        private EventHandler<MouseMoveEventArgs> mouseMoved;
        private EventHandler<MouseButtonEventArgs> mousePressed;

        public Button(Vector2f _pos, string _text)
        {
            Color c = new Color(Color.Cyan);
            c.G = 128;
            c.A = 255;
            enteredColor = c;


            texture = new Texture("Resources\\Sprites\\GUI\\Button.png");
            sprite = new Sprite(texture);

            isVisable = true;
            isEntered = false;

            stringText = _text;

            text = new Text(stringText, Canvas.Instance.font);
            text.CharacterSize = 14;
            text.FillColor = Color.White;

            Pos = _pos;

            mouseMoved = (obj, e) =>
            {
                if (sprite.GetGlobalBounds().Contains(MainWindow.Instance.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y)).X, MainWindow.Instance.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y)).Y) && isVisable)
                    isEntered = true;
                else
                    isEntered = false;
            };

            mousePressed = (obj, e) =>
            {
                if (isEntered && isVisable && e.Button == Mouse.Button.Left)
                {
                    Pressed?.Invoke(this, null);
                }
            };
            MainWindow.Instance.RenderWindow.MouseMoved += mouseMoved;
            MainWindow.Instance.RenderWindow.MouseButtonPressed += mousePressed;
        }

        public void Update()
        {
            float textHeight = 12;
            float textWidth = text.GetGlobalBounds().Width;

            text.Position = new Vector2f(Pos.X + ((sprite.Texture.Size.X / 2) - textWidth / 2), Pos.Y + ((sprite.Texture.Size.Y / 2) - textHeight / 2 - 3));

            if (isEntered)
                sprite.Color = enteredColor;
            else
                sprite.Color = freeColor;
        }

        public void Draw()
        {
            MainWindow.Instance.RenderWindow.Draw(sprite);
            MainWindow.Instance.RenderWindow.Draw(text);
        }

        public void Removed()
        {
            MainWindow.Instance.RenderWindow.MouseMoved -= mouseMoved;
            MainWindow.Instance.RenderWindow.MouseButtonPressed -= mousePressed;
            // Removed
        }
    }
}
