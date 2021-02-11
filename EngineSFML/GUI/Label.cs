using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;
using SFML.Graphics;

using SFML.Window;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class Label : IUI
    {

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } set { isVisable = value; } }

        private Texture texture;
        private Sprite sprite;

        public Vector2f Pos { get { return sprite.Position; } set { sprite.Position = value; } }

        ///

        private bool isFocused;

        private string enteredText;

        private string previewText;

        private Text text;

        private bool cursorShowed = false;
        private float cursorShowedTime;

        public string EnteredText
        {
            get { return enteredText; }
        }

        public Label(Vector2f _pos, string _prevText)
        {
            isVisable = true;
            isFocused = false;
            enteredText = "";
            previewText = _prevText;

            texture = new Texture("Resources\\Sprites\\GUI\\Label.png");
            sprite = new Sprite(texture);

            Pos = _pos;

            text = new Text(previewText, Canvas.Instance.font);
            text.CharacterSize = 14;

            float textHeight = text.GetGlobalBounds().Height;
            float textWidth = text.GetGlobalBounds().Width;

            text.Position = new Vector2f(Pos.X + ((sprite.Texture.Size.X / 2) - textWidth / 2), Pos.Y + ((sprite.Texture.Size.Y / 2) - textHeight / 2 - 3));

            MainWindow.Instance.RenderWindow.MouseButtonPressed += (obj, e) =>
            {
                
                if (e.Button == Mouse.Button.Left && sprite.GetGlobalBounds().Contains(e.X, e.Y) && !isFocused)
                    isFocused = true;

                if (e.Button == Mouse.Button.Left && !sprite.GetGlobalBounds().Contains(e.X, e.Y) && isFocused)
                    isFocused = false;
            };

            MainWindow.Instance.RenderWindow.TextEntered += (obj, e) =>
            {
                if (isFocused)
                {
                    if ((int)e.Unicode[0] == 8 && enteredText.Length != 0)
                        enteredText = enteredText.Remove(enteredText.Length - 1);
                    if ((int)e.Unicode[0] == 22 && Clipboard.Contents != null)
                        enteredText += Clipboard.Contents;
                    else if (!Char.IsControl(e.Unicode[0]) && text.GetGlobalBounds().Width / 2 + 5 < sprite.Texture.Size.X / 2)
                        enteredText += e.Unicode;
                }
            };
        }

        public void Update()
        {

            if (enteredText == "")
                text.DisplayedString = previewText;

            if (enteredText != "")
                text.DisplayedString = enteredText;

            float textHeight = 12;
            float textWidth = text.GetGlobalBounds().Width;

            text.Position = new Vector2f(Pos.X + ((sprite.Texture.Size.X / 2) - textWidth / 2), Pos.Y + ((sprite.Texture.Size.Y / 2) - textHeight / 2 - 3));

            if (isFocused)
            {

                if (cursorShowedTime >= 200)
                {
                    cursorShowedTime = 0; 
                    cursorShowed = false;
                }

                if (cursorShowedTime <= -400)
                {
                    cursorShowed = true;
                    cursorShowedTime = 0;
                }

                if (cursorShowed)
                {
                    cursorShowedTime += MainWindow.Instance.DeltaTime;
                    text.DisplayedString += "|";
                }

                if (!cursorShowed)
                    cursorShowedTime -= MainWindow.Instance.DeltaTime;
            }
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
