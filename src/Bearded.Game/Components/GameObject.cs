using System;
using System.Collections.Generic;
using System.Linq;
using Bearded.Utilities.Collections;
using OpenTK;

namespace Bearded.Game.Components
{
    /// <summary>
    /// A game object represents an object in the game of which the behaviour is completely decided by its components.
    /// </summary>
    public sealed class GameObject : IDeletable
    {
        /// <summary>
        /// The position of the game object in world space.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The rotation of the game object in world space.
        /// </summary>
        public Quaternion Rotation;

        /// <summary>
        /// The scale of the game object in world space.
        /// </summary>
        public Vector3 Scale = Vector3.One;

        private readonly List<IGameComponent> components = new List<IGameComponent>();

        /// <summary>
        /// Whether the game object has been destroyed.
        /// </summary>
        public bool Deleted { get; private set; }

        /// <summary>
        /// Updates the game object.
        /// </summary>
        /// <param name="dt">The amount of time passed since the last update call.</param>
        public void Update(float dt)
        {
            foreach (var c in this.components)
                c.Update(dt);
        }

        /// <summary>
        /// Adds a new component of the specified type to the game object.
        /// </summary>
        /// <typeparam name="T">The type of component to be added.</typeparam>
        /// <returns>The added component.</returns>
        public T AddComponent<T>() where T : IGameComponent, new()
        {
            T ret;
            this.components.Add(ret = new T());
            return ret;
        }

        /// <summary>
        /// Adds an existing component to the game object.
        /// </summary>
        /// <param name="component">The component to be added.</param>
        public void AddComponent(IGameComponent component)
        {
            this.components.Add(component);
        }

        /// <summary>
        /// Removes a component from the game object.
        /// </summary>
        /// <param name="component">The component to be removed.</param>
        /// <returns>True if the component is succesfully removed; false otherwise. Also returns false if the component was not present.</returns>
        public bool RemoveComponent(IGameComponent component)
        {
            return this.components.Remove(component);
        }

        /// <summary>
        /// Removes all components of a specified type from the game object.
        /// </summary>
        /// <typeparam name="T">The type of components to remove.</typeparam>
        /// <returns>The amount of components removed.</returns>
        public int RemoveComponents<T>()
        {
            return this.components.RemoveAll(c => c is T);
        }

        /// <summary>
        /// Gets the first component of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <returns>The first component of the specified type if it exists, null otherwise.</returns>
        public T GetComponent<T>() where T : IGameComponent
        {
            return (T)this.components.FirstOrDefault(c => c is T);
        }

        /// <summary>
        /// Gets all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the components to retrieve.</typeparam>
        /// <returns>An array of all components of the specified type.</returns>
        public T[] GetComponents<T>()
        {
            return this.components.OfType<T>().ToArray();
        }

        /// <summary>
        /// Sends a message to all components that are listening for the specified message.
        /// </summary>
        /// <param name="message">The contents of the message.</param>
        /// <typeparam name="T">TThe type of message.</typeparam>
        public void SendMessage<T>(T message)
        {
            foreach (var c in this.GetComponents<IListener<T>>())
                c.Listen(message);
        }

        /// <summary>
        /// Destroys the game object, removing it from the game state.
        /// </summary>
        public void Destroy()
        {
            if (this.Deleted)
                return;

            this.Deleted = true;

            foreach (var c in this.GetComponents<IDisposable>())
                c.Dispose();
        }

        #region Instantiating
        private static readonly Dictionary<Type, GameObjectTemplate> templates = new Dictionary<Type, GameObjectTemplate>(); 

        /// <summary>
        /// Instantiates a new game object with the specified definition.
        /// </summary>
        /// <typeparam name="T">The type of the game object definition.</typeparam>
        /// <returns></returns>
        public static GameObject Instantiate<T>() where T : GameObjectTemplate, new()
        {
            GameObjectTemplate template;
            if (GameObject.templates.TryGetValue(typeof (T), out template))
                return template.Instantiate();

            template = new T();
            GameObject.templates.Add(typeof(T), template);

            return template.Instantiate();
        }
        #endregion
    }
}