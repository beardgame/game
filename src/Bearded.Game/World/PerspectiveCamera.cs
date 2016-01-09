using Bearded.Utilities.Math;
using OpenTK;

namespace Bearded.Game.World
{
    /// <summary>
    /// A camera leaving the perspective intact.
    /// </summary>
    public class PerspectiveCamera : Camera
    {
        private float fovy;
        private float aspect;
        private float zNear;
        private float zFar;

        /// <summary>
        /// The field of view in degrees.
        /// </summary>
        public float FieldOfView
        {
            get { return this.fovy; }
            set
            {
                this.fovy = value;
                this.UpdateProjectionMatrix();
            }
        }

        /// <summary>
        /// The aspect ratio of the camera.
        /// </summary>
        public float AspectRatio
        {
            get { return this.aspect; }
            set
            {
                this.aspect = value;
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
        /// Creates a new PerspectiveCamera instance.
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        public PerspectiveCamera(float fovy = 60, float aspect = 16f / 9f, float zNear = .1f, float zFar = 512f)
        {
            this.fovy = fovy;
            this.aspect = aspect;
            this.zNear = zNear;
            this.zFar = zFar;
        }

        /// <summary>
        /// Calculates a new projection matrix.
        /// </summary>
        /// <returns></returns>
        protected override Matrix4 CalculateProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(Mathf.TwoPi * this.fovy / 360, this.aspect, this.zNear, this.zFar);
        }
    }
}