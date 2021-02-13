using System;

using EngineSFML.Main;
using EngineSFML.Networking;

namespace EngineSFML
{
    public class Program
    {
        private static Game game;


        private static bool isStarted = false;
        public static bool IsStarted { get { return isStarted; } }

        private static void Start(bool _debug)
        {
            if (!isStarted)
            {
                game = Game.Instance;

                isStarted = true;
                game.Start(_debug);
                isStarted = false;
            }
        }

        public static void Main(string[] args)
        {
            bool isDebug = false;
            
            foreach (string arg in args)
            {
                if (arg.ToLower() == "debug")
                    isDebug = true;
            }

            Start(isDebug);
        }
    }
}
