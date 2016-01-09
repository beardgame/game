using OpenTK;

namespace Bearded.Game.World
{
    /// <summary>
    /// A camera that uses ortographic projection.
    /// </summary>
    public class OrthographicCamera : Camera
    {
        private float width;
        private float height;
        private float zNear;
        private float zFar;

        /// <summary>
        /// The width of the camera viewport.
        /// </summary>
        public float Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
                this.UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// The height of the camera viewport.
        /// </summary>
        public float Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
                this.UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// The near clipping plane.
        /// </summary>
        public float ClipNear
        {
            get { return this.zNear; }
            set
            {
                this.zNear = value;
                this.UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// The far clipping plane.
        /// </summary>
        public float ClipFar
        {
            get { return this.zFar; }
            set
            {
                this.zFar = value;
                this.UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// Creates a new OrtographicCamera instance.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        public OrthographicCamera(float width = 1280, float height = 720, float zNear = .1f, float zFar = 512f)
        {
            this.width = width;
            this.height = height;
            this.zNear = zNear;
            this.zFar = zFar;
        }

        /// <summary>
        /// Calculates a new projection matrix.
        /// </summary>
        /// <returns></returns>
        protected override Matrix4 CalculateProjectionMatrix()
        {
            return Matrix4.CreateOrthographic(this.width, this.height, this.zNear, this.zFar);
        }
    }
}