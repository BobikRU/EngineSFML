using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class Image : IUI
    {
        private bool isVisable;
        public bool IsVisable { get { return isVisable; } }

        private Texture imageTexture;
        private Sprite imageSprite;

        public Vector2f Pos { get { return imageSprite.Position; } set { imageSprite.Position = value; } }

        public Vector2f Scale { get { return imageSprite.Scale; } set { imageSprite.Scale = value; } }

        public Image(Vector2f _pos, string _filename)
        {
            isVisable = true;

            imageTexture = new Texture(_filename);
            imageSprite = new Sprite(imageTexture)
            {
                Position = _pos
            };
        }

        public void Update()
        {

        }

        public void Draw()
        {
            MainWindow.Instance.RenderWindow.Draw(imageSprite);
        }
    }
}
