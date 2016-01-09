namespace Bearded.Game.Rendering
{
    /// <summary>
    /// Something that can be rendered.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Executes the render logic for this object.
        /// </summary>
        void Render();
    }
}