using System;
using amulware.Graphics;
using Bearded.Utilities.Input;
using OpenTK.Input;

namespace TomRijnbeek.Game
{
    /// <summary>
    /// A simplified interface to the amulware.Graphics.Program class.
    /// </summary>
    public abstract class Game : IDisposable
    {
        private readonly GameWindow window;

        /// <summary>
        /// Creates a new instance of the game.
        /// </summary>
        /// <param name="title">Window title.</param>
        /// <param name="width">Window width.</param>
        /// <param name="height">Window height.</param>
        protected Game(string title, int width = 1280, int height = 720)
        {
            this.window = new GameWindow(this, title, width, height);
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            this.window.Run();
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="targetFps"></param>
        public void Run(double targetFps)
        {
            this.window.Run(targetFps);
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="targetUpdateFps"></param>
        /// <param name="targetDrawFps"></param>
        public void Run(double targetUpdateFps, double targetDrawFps)
        {
            this.window.Run(targetUpdateFps, targetDrawFps);
        }

        internal void OnLoad(MouseDevice mouse)
        {
            InputManager.Initialize(mouse);

            this.Load();
        }

        internal void OnUpdate(UpdateEventArgs e)
        {
            if (this.window.Focused)
            {
                InputManager.Update();
            }

            this.Update(e);
        }

        internal void OnRender(UpdateEventArgs e)
        {
            this.Render(e);
        }

        /// <summary>
        /// Loads the game resources.
        /// </summary>
        protected abstract void Load();

        /// <summary>
        /// Runs a single update frame.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void Update(UpdateEventArgs e);

        /// <summary>
        /// Runs a single render frame.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void Render(UpdateEventArgs e);

        /// <summary>
        /// Disposes the game.
        /// </summary>
        public void Dispose()
        {
            this.window.Dispose();
        }
    }
}