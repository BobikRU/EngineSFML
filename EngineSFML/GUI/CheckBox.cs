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

        private bool isChecked;

        private EventHandler<MouseButtonEventArgs> mousePressed;

        public bool IsChecked { get { return isChecked; } }

        public class HasChangedArgs : EventArgs
        {
            public bool isChecked;
            public HasChangedArgs(bool _isChecked)
            {
                isChecked = _isChecked;
            }
        }

        public event EventHandler<HasChangedArgs> HasChanged;

        public CheckBox(Vector2f _pos, string _name, bool _default = false)
        {
            isVisable = true;
            isChecked = _default;
            stringText = _name;

            texture = new Texture("Resources\\Sprites\\GUI\\CheckBox.png");
            sprite = new Sprite(texture);

            Pos = _pos;

            text = new Text(stringText, Canvas.Instance.font);
            text.CharacterSize = 14;

            text.Position = new Vector2f(Pos.X + 24, Pos.Y);

            mousePressed = (obj, e) =>
            {
                if (sprite.GetGlobalBounds().Contains(MainWindow.Instance.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y)).X, MainWindow.Instance.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y)).Y) && e.Button == Mouse.Button.Left && isVisable)
                {
                    isChecked = !isChecked;
                    HasChanged?.Invoke(this, new HasChangedArgs(IsChecked));
                }
            };
            MainWindow.Instance.RenderWindow.MouseButtonPressed += mousePressed;
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
            MainWindow.Instance.RenderWindow.Draw(sprite);
            MainWindow.Instance.RenderWindow.Draw(text);
        }

        public void Removed()
        {
            MainWindow.Instance.RenderWindow.MouseButtonPressed -= mousePressed;
            // Removed
        }
    }
}
