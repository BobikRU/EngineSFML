using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class CheckBox : IUI
    {
        private bool isVisable;
        public bool IsVisable { get { return isVisable; } set { isVisable = value; } }

        private Texture texture;
        private Sprite sprite;

        public Vector2f Pos { get { return sprite.Position; } set { sprite.Position = value; } }

        private string stringText;

        private Text text;

        private bool isPressed;
        private bool isChecked;


        public bool IsChecked { get { return isChecked; } }

        public CheckBox(Vector2f _pos, string _name, bool _default = false)
        {
            isVisable = true;
            isChecked = _default;
            stringText = _name;
            isPressed = false;

            texture = new Texture("Resources\\Sprites\\GUI\\CheckBox.png");
            sprite = new Sprite(texture);

            Pos = _pos;

            text = new Text(stringText, Canvas.Instance.font);
            text.CharacterSize = 14;

            text.Position = new Vector2f(Pos.X + 24, Pos.Y);

            MainWindow.Instance.RenderWindow.MouseButtonPressed += (obj, e) =>
            {
                if (sprite.GetGlobalBounds().Contains(e.X, e.Y) && e.Button == Mouse.Button.Left)
                {
                    isPressed = true;
                    isChecked = !isChecked;
                }
            };

            MainWindow.Instance.RenderWindow.MouseButtonReleased += (obj, e) =>
            {
                if (isPressed && e.Button == Mouse.Button.Left)
                {
                    isPressed = false;
                }
            };
        }

        public void Update()
        {
            if (isChecked)
                sprite.TextureRect = new IntRect(16, 0, 16, 16);
            else
                sprite.TextureRect = new IntRect(0, 0, 16, 16);

            text.Position = new Vector2f(Pos.X + 24, Pos.Y);
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
