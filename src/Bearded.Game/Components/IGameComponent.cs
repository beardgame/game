namespace Bearded.Game.Components
{
    /// <summary>
    /// The interface for game components that can be added to a game object.
    /// </summary>
    public interface IGameComponent
    {
        /// <summary>
        /// The update method that should be called every frame.
        /// </summary>
        /// <param name="dt">The amount of time passed since the last update call.</param>
        void Update(float dt);
    }
}