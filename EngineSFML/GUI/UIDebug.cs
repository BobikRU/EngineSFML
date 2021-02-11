using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;
using SFML.Graphics;

using EngineSFML.Main;

namespace EngineSFML.GUI
{
    public class UIDebug : IUI
    {

        private bool isVisable;
        public bool IsVisable { get { return isVisable; } set { isVisable = value; } }

        public Vector2f pos;

        private Text debug;

        public static string DEBUG;

        public UIDebug()
        {
            pos = new Vector2f(Canvas.Instance.ZeroCoordX, Canvas.Instance.ZeroCoordY);
            isVisable = Game.Instance.IsDebug;

            debug = new Text();
            debug.Font = Canvas.Instance.font;
            debug.CharacterSize = 14;
            debug.FillColor = Color.White;
        }

        public void Update()
        {
            pos = new Vector2f(Canvas.Instance.ZeroCoordX, Canvas.Instance.ZeroCoordY);
            debug.Position = new Vector2f(pos.X + 12, pos.Y + 12);

            debug.DisplayedString = DEBUG;
            DEBUG = "";
        }

        public void Draw()
        {
            MainWindow.Instance.RenderWindow.Draw(debug);
        }

    }
}
