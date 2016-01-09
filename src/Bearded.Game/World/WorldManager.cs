using Bearded.Utilities;

namespace Bearded.Game.World
{
    /// <summary>
    /// Manages the rendering of the world space.
    /// </summary>
    public class WorldManager : Singleton<WorldManager>
    {
        /// <summary>
        /// The camera from which the world is rendered.
        /// </summary>
        public Camera Camera;

        /// <summary>
        /// Creates a new instance of the world manager.
        /// </summary>
        /// <param name="camera"></param>
        public WorldManager(Camera camera)
        {
            this.Camera = camera;
        }
    }
}