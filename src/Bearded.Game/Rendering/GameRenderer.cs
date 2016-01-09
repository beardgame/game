using System.Collections.Generic;
using amulware.Graphics;
using amulware.Graphics.ShaderManagement;
using Bearded.Utilities;
using OpenTK.Graphics.OpenGL;

namespace Bearded.Game.Rendering
{
    /// <summary>
    /// Singleton game render manager.
    /// </summary>
    public class GameRenderer : Singleton<GameRenderer>
    {
        private readonly ICollection<IRenderer> renderers = new List<IRenderer>();

        /// <summary>
        /// Manager for all currently loaded shaders.
        /// </summary>
        public ShaderManager Shaders { get; }

        /// <summary>
        /// Creates a new GameRenderer instance.
        /// </summary>
        /// <param name="shaderPath">The path from which shaders should be loaded.</param>
        public GameRenderer(string shaderPath = null)
        {
            this.Shaders = new ShaderManager();
            this.Shaders.Add(ShaderFileLoader.CreateDefault(shaderPath ?? "data/shaders").Load(""));
        }

        /// <summary>
        /// Sets the viewport size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        /// <summary>
        /// Executes the render logic.
        /// </summary>
        /// <param name="e"></param>
        public void Render(UpdateEventArgs e)
        {
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            foreach (var r in this.renderers)
            {
                r.Render();
            }
        }

        /// <summary>
        /// Registers a new renderer.
        /// </summary>
        /// <param name="renderer"></param>
        public void RegisterRenderer(IRenderer renderer)
        {
            this.renderers.Add(renderer);
        }

        /// <summary>
        /// Unregisters a new renderer.
        /// </summary>
        /// <param name="renderer"></param>
        public void UnregisterRenderer(IRenderer renderer)
        {
            this.renderers.Remove(renderer);
        }
    }
}