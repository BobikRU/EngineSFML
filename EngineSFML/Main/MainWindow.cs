using System;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace EngineSFML.Main
{
    public sealed class MainWindow
    {
        private static readonly Lazy<MainWindow> instance =
            new Lazy<MainWindow>(() => new MainWindow());
        public static MainWindow Instance { get { return instance.Value; } }

        private RenderWindow renderWindow;
        public RenderWindow RenderWindow { get { return renderWindow; } }

        public delegate void update();
        public update Update;

        public delegate void draw();
        public draw Draw;

        private Clock clock;
        private float deltaTime;
        public float DeltaTime { get { return deltaTime; } }

        private bool isFullscreen;
        public bool IsFullscreen { get { return isFullscreen; } }


        private MainWindow()
        {
            var config = Settings.Instance;

            VideoMode videoMode = new VideoMode(uint.Parse(config.GetConfig("window.width")), uint.Parse(config.GetConfig("window.height")));

            isFullscreen = bool.Parse(Settings.Instance.GetConfig("window.fullscreen"));
            Styles styles = isFullscreen ? Styles.Fullscreen : Styles.Default;

            renderWindow = new RenderWindow(videoMode, config.GetConfig("window.title"), styles);

            renderWindow.SetVerticalSyncEnabled(bool.Parse(config.GetConfig("window.vertsync")));
            renderWindow.SetFramerateLimit(uint.Parse(config.GetConfig("window.framelimit")));

            renderWindow.Resized += (obj, e) => {float width = e.Width; float height = e.Height; renderWindow.SetView(new View(new FloatRect(0, 0, width, height))); };
            renderWindow.Closed += (obj, e) => { renderWindow.Close(); };

            clock = new Clock();
            deltaTime = 0.0f;
        }

        public void StartLoop()
        {
            while (RenderWindow.IsOpen)
            {
                deltaTime = clock.ElapsedTime.AsMilliseconds();
                clock.Restart();

                renderWindow.DispatchEvents();

                Update?.Invoke();
                renderWindow.Clear();
                Draw?.Invoke();
                renderWindow.Display();
            }
        }

    }
}
