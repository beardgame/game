using System;
using amulware.Graphics;
using amulware.Graphics.ShaderManagement;
using Bearded.Utilities.Input;
using OpenTK.Input;
using TomRijnbeek.Game.Rendering;

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
            // ReSharper disable once ObjectCreationAsStatement
            new GameRenderer();
            InputManager.Initialize(mouse);

            this.Load();
        }

        internal void OnResize()
        {
            GameRenderer.Instance.SetSize(this.window.Width, this.window.Height);
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
            GameRenderer.Instance.Render(e);
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
        /// Disposes the game.
        /// </summary>
        public void Dispose()
        {
            this.window.Dispose();
        }
    }
}