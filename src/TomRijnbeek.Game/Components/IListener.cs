namespace TomRijnbeek.Game.Components
{
    /// <summary>
    /// Interface that specifies that this object listens to messages of a given type.
    /// </summary>
    /// <typeparam name="T">The type of the messages listened to.</typeparam>
    public interface IListener<in T>
    {
        /// <summary>
        /// Listen to messages sent from the game object.
        /// </summary>
        /// <param name="message">The sent message.</param>
        /// <typeparam name="T">The type of the sent message.</typeparam>
        void Listen(T message);
    }
}