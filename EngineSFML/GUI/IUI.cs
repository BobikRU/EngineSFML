using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSFML.GUI
{
    public interface IUI
    {
        public bool IsVisable { get; }

        public void Update();

        public void Draw();

    }
}
