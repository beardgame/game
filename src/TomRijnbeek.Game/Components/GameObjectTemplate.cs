namespace TomRijnbeek.Game.Components
{
    /// <summary>
    /// Wrapper class for game object definitions.
    /// </summary>
    public abstract class GameObjectTemplate
    {
        public GameObject Instantiate()
        {
            var obj = new GameObject();
            this.AddComponents(obj);
            return obj;
        }

        protected abstract void AddComponents(GameObject obj);
    }
}