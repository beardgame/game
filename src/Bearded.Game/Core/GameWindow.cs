using System;
using amulware.Graphics;
using OpenTK;
using OpenTK.Graphics;

namespace Bearded.Game
{
    /// <summary>
    /// Program wrapper that forwards calls to the game.
    /// </summary>
    public class GameWindow : Program
    {
        private readonly Game game;

        /// <summary>
        /// Creates a new game window instance.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameWindow(Game game, string title, int width = 1280, int height = 720)
            : base(width, height, GraphicsMode.Default, title, GameWindowFlags.Default)
        {
            this.game = game;
        }

        /// <summary>
        /// Called when the NativeWindow is resized.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.game.OnResize();
        }

        /// <summary>
        /// Called after an OpenGL context has been established, but before entering the main loop.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.game.OnLoad(this.Mouse);
        }

        /// <summary>
        /// Runs when an update frame is requested.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdate(UpdateEventArgs e)
        {
            this.game.OnUpdate(e);
        }

        /// <summary>
        /// Runs when a render frame is requested.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRender(UpdateEventArgs e)
        {
            this.game.OnRender(e);

            this.SwapBuffers();
        }
    }
}