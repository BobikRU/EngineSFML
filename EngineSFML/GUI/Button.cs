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

        public delegate void ButtonPressed();

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } set { isVisable = value; } }

        private Texture texture;
        private Sprite sprite;

        public Vector2f Pos { get { return sprite.Position; } set { sprite.Position = value; } }

        private string stringText;

        private Text text;

        private bool isEntered;

        private bool isPressed;

        public ButtonPressed Pressed;

        public Button(string _text, Vector2f _pos)
        {
            Color c = new Color(Color.Cyan);
            c.A = 128;
            enteredColor = c;


            texture = new Texture("Resources\\Sprites\\GUI\\Button.png");
            sprite = new Sprite(texture);

            isVisable = true;
            isEntered = false;
            isPressed = false;

            stringText = _text;

            text = new Text(stringText, Canvas.Instance.font);
            text.CharacterSize = 14;
            text.FillColor = Color.White;

            Pos = _pos;

            MainWindow.Instance.RenderWindow.MouseMoved += (obj, e) =>
            {
                if (sprite.GetGlobalBounds().Contains(e.X, e.Y) && isVisable)
                    isEntered = true;
                else
                    isEntered = false;
            };

            MainWindow.Instance.RenderWindow.MouseButtonPressed += (obj, e) =>
            {
                if (isEntered && !isPressed && isVisable && e.Button == Mouse.Button.Left)
                {
                    isPressed = true;
                    Pressed?.Invoke();
                }
            };

            MainWindow.Instance.RenderWindow.MouseButtonReleased += (obj, e) =>
            {
                if (isPressed && isVisable)
                {
                    isPressed = false;
                }
            };
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
            if (isVisable)
            {
                MainWindow.Instance.RenderWindow.Draw(sprite);
                MainWindow.Instance.RenderWindow.Draw(text);
            }
        }

    }
}
