using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSFML.Main
{
    public sealed class Inputs
    {

        public static readonly string[] KEYS_NAMES = { "up", "down", "left", "right", "close" };

        private static readonly Lazy<Inputs> instance =
            new Lazy<Inputs>(() => new Inputs());
        public static Inputs Instance { get { return instance.Value; } }

        private Dictionary<string, bool> isKeyPressed;

        private Dictionary<string, bool> isKeyReleased;

        public bool IsKeyPressed(string key)
        {
            if (isKeyPressed.ContainsKey(key))
                return isKeyPressed[key];
            else
                return false;
        }

        public bool IsKeyReleased(string key)
        {
            if (isKeyReleased.ContainsKey(key))
                return isKeyReleased[key];
            else
                return false;
        }

        private Inputs()
        {
            isKeyPressed = new Dictionary<string, bool>();
            isKeyReleased = new Dictionary<string, bool>();

            foreach(string name in KEYS_NAMES)
            {
                isKeyPressed.Add(name, false);
                isKeyReleased.Add(name, false);
            }

            MainWindow.Instance.RenderWindow.KeyPressed += (obj, e) => 
            {
                foreach (string key in KEYS_NAMES)
                {
                    if (e.Code == (Keyboard.Key)uint.Parse(Settings.Instance.GetConfig("control." + key)))
                    {
                        isKeyPressed[key] = true;
                        isKeyReleased[key] = false;
                    }
                }
            };

            MainWindow.Instance.RenderWindow.KeyReleased += (obj, e) =>
            {
                foreach (string key in KEYS_NAMES)
                {
                    if (e.Code == (Keyboard.Key)uint.Parse(Settings.Instance.GetConfig("control." + key)))
                    {
                        isKeyPressed[key] = false;
                        isKeyReleased[key] = true;
                    }
                }
            };


        }
    }
}
