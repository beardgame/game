using amulware.Graphics;
using Bearded.Utilities.Collections;

namespace TomRijnbeek.Game.Components
{
    /// <summary>
    /// The current game state.
    /// </summary>
    public class GameState
    {
        private readonly DeletableObjectList<GameObject> gameObjects = new DeletableObjectList<GameObject>();

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public GameState()
        {
            
        }

        /// <summary>
        /// Updates all the game objects.
        /// </summary>
        /// <param name="e"></param>
        public void Update(UpdateEventArgs e)
        {
            foreach (var obj in this.gameObjects)
                obj.Update(e.ElapsedTimeInSf);
        }

        /// <summary>
        /// Adds a new game object to the game state.
        /// </summary>
        /// <param name="obj"></param>
        public void AddGameObject(GameObject obj)
        {
            this.gameObjects.Add(obj);
        }

        /// <summary>
        /// Instantiates a game object and adds it to the gamestate.
        /// </summary>
        /// <typeparam name="T">The type of the game object definition.</typeparam>
        /// <returns>The instantiated game object.</returns>
        public GameObject InstantiateGameObject<T>() where T : GameObjectTemplate, new()
        {
            var obj = GameObject.Instantiate<T>();
            this.AddGameObject(obj);
            return obj;
        }
    }
}