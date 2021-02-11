using System;
using System.Collections.Generic;
using System.Text;


using SFML.Graphics;
using SFML.Window;
using SFML.System;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public sealed class Canvas
    {
        private static readonly Lazy<Canvas> instance =
            new Lazy<Canvas>(() => new Canvas());

        public static Canvas Instance { get { return instance.Value; } }

        private int zeroCoordX;
        public int ZeroCoordX { get {return zeroCoordX; } }

        private int zeroCoordY;
        public int ZeroCoordY { get { return zeroCoordY; } }

        public Font font;

        private List<IUI> uis;

        private Canvas()
        {
            font = new Font("Resources\\Fonts\\arial.ttf");

            uis = new List<IUI>();
        }

        public void Update()
        {

            View view = MainWindow.Instance.RenderWindow.GetView();

            zeroCoordX = (int)view.Center.X - (int)(MainWindow.Instance.RenderWindow.Size.X / 2);
            zeroCoordY = (int)view.Center.Y - (int)(MainWindow.Instance.RenderWindow.Size.Y / 2);

            for (int i = 0; i < uis.Count; ++i)
            {
                uis[i].Update();
            }
        }

        public void AddGUI(IUI ui)
        {
            if (!uis.Contains(ui))
                uis.Add(ui);
        }

        public void RemoveGUI(IUI ui)
        {
            if (uis.Contains(ui))
                uis.Remove(ui);
        }

        public void Draw()
        {
            for (int i = 0; i < uis.Count; ++i)
            {
                uis[i].Draw();
            }
        }

    }
}
