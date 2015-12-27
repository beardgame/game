using amulware.Graphics;

namespace TomRijnbeek.Game.Components
{
    /// <summary>
    /// A game based on component-based programming.
    /// </summary>
    public abstract class ComponentGame : Game
    {
        /// <summary>
        /// The game state.
        /// </summary>
        protected GameState GameState { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected ComponentGame(string title, int width = 1280, int height = 720)
            : base(title, width, height)
        {
            this.GameState = new GameState();
        }

        /// <summary>
        /// Runs a single update frame.
        /// </summary>
        /// <param name="e"></param>
        protected override void Update(UpdateEventArgs e)
        {
            this.GameState.Update(e);
        }
    }
}