using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class Slider : IUI
    {

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } set { isVisable = value; } }

        private Texture texture;

        private Sprite cursor;
        private Sprite[] sprites;

        private Text[] texts;
 
        public Vector2f Pos { get { return sprites[0].Position; } set { sprites[0].Position = value; } }

        private string[] variants;
        private int variantCount;
        private int selectedVariant;

        public int SelectedVariant { get { return selectedVariant; } }

        public class HasChangedArgs : EventArgs
        {
            public int variant;
            public HasChangedArgs(int _var)
            {
                variant = _var;
            }
        }

        public event EventHandler<HasChangedArgs> HasChanged;

        private EventHandler<MouseButtonEventArgs> mousePressed;

        public Slider(Vector2f _pos, string[] _variants, int _default = 0)
        {
            isVisable = true;

            variants = _variants;
            variantCount = variants.Length;
            selectedVariant = _default;

            texture = new Texture("Resources\\Sprites\\GUI\\Slider.png");

            sprites = new Sprite[variantCount];
            texts = new Text[variantCount];

            for (int i = 0; i < variantCount; ++i)
            {
                sprites[i] = new Sprite(texture)
                {
                    TextureRect = new IntRect(24, 0, 24, 24),
                    Position = new Vector2f(_pos.X + 24 * i, _pos.Y)
                };

                texts[i] = new Text(variants[i], Canvas.Instance.font)
                {
                    CharacterSize = 11,
                    Position = new Vector2f(_pos.X + 24 * i, _pos.Y + 28)
                };
            }

            cursor = new Sprite(texture)
            {
                TextureRect = new IntRect(0, 0, 24, 24),
                Position = new Vector2f(_pos.X + 24 * selectedVariant, _pos.Y - 4)
            };

            mousePressed = (obj, e) =>
            {
                if (e.Button == Mouse.Button.Left && isVisable)
                {
                    for (int i = 0; i < variantCount; ++i)
                    {
                        if (sprites[i].GetGlobalBounds().Contains(MainWindow.Instance.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y)).X, MainWindow.Instance.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y)).Y) && i != selectedVariant)
                        {
                            selectedVariant = i;
                            HasChanged?.Invoke(this, new HasChangedArgs(selectedVariant));
                        }
                    }
                }
            };
            MainWindow.Instance.RenderWindow.MouseButtonPressed += mousePressed;
        }

        public void Update()
        {
            for (int i = 0; i < variantCount; ++i)
            {
                sprites[i].Position = new Vector2f(Pos.X + 24 * i, Pos.Y);

                texts[i].Position = new Vector2f(Pos.X + 24 * i, Pos.Y + 28);

                texts[i].Origin = new Vector2f(texts[i].GetGlobalBounds().Width / 2, texts[i].GetGlobalBounds().Height / 2);
                texts[i].Rotation = 90;
            }

            cursor.Position = new Vector2f(Pos.X + 24 * selectedVariant, Pos.Y - 4);
        }

        public void Draw()
        { 
            for (int i = 0; i < variantCount; ++i)
            {
                MainWindow.Instance.RenderWindow.Draw(sprites[i]);
                
                MainWindow.Instance.RenderWindow.Draw(texts[i]);
            }
            
            MainWindow.Instance.RenderWindow.Draw(cursor);
        }

        public void Removed()
        {
            MainWindow.Instance.RenderWindow.MouseButtonPressed -= mousePressed;
            //Removed
        }

    }
}
