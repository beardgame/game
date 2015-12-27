using System;
using amulware.Graphics;
using Bearded.Utilities.Math;
using OpenTK;

namespace TomRijnbeek.Game.World
{
    /// <summary>
    /// Object representing a rendering camera.
    /// </summary>
    public abstract class Camera
    {
        #region Members
        #region Camera transform
        private Vector3 position;
        private Quaternion rotation;

        /// <summary>
        /// Camera position in world space.
        /// </summary>
        public Vector3 Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                this.UpdateViewMatrix();
            }
        }

        /// <summary>
        /// Camera rotation in world space, assuming +z is forward.
        /// </summary>
        public Quaternion Rotation
        {
            get { return this.rotation; }
            set
            {
                this.rotation = value;
                this.UpdateViewMatrix();
            }
        }
        #endregion

        #region Matrices
        /// <summary>
        /// The view matrix representing the world transformation of this camera.
        /// </summary>
        public Matrix4Uniform View { get; }

        /// <summary>
        /// The projection matrix of this camera.
        /// </summary>
        public Matrix4Uniform Projection { get; }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Camera instance.
        /// </summary>
        protected Camera()
        {
            this.Rotation = Quaternion.Identity;

            this.View = new Matrix4Uniform("viewMatrix");
            this.Projection = new Matrix4Uniform("projectionMatrix");
        }
        #endregion

        #region Methods
        #region Camera Transform
        /// <summary>
        /// Sets the camera transformation based on look at parameters.
        /// </summary>
        /// <param name="eye">The world position of the camera.</param>
        /// <param name="target">The point the camera is looking at.</param>
        /// <param name="up">The up direction of the camera.</param>
        public void SetLookAt(Vector3 eye, Vector3 target, Vector3 up)
        {
            this.position = eye;

            if (Math.Abs(up.LengthSquared - 1) <= float.Epsilon)
            {
                up = up.Normalized();
            }

            this.rotation = Camera.createLookAt(Vector3.Normalize(target - eye), up);

            this.UpdateViewMatrix();
        }
        #endregion

        #region Matrices
        /// <summary>
        /// Updates the view matrix based on the world transformation.
        /// </summary>
        protected void UpdateViewMatrix()
        {
            this.View.Matrix = Matrix4.CreateTranslation(this.position) * Matrix4.CreateFromQuaternion(this.rotation);
        }

        /// <summary>
        /// Calculates a new projection matrix.
        /// </summary>
        /// <returns></returns>
        protected abstract Matrix4 CalculateProjectionMatrix();

        /// <summary>
        /// Updates the projection matrix.
        /// </summary>
        protected void UpdateProjectionMatrix()
        {
            this.Projection.Matrix = this.CalculateProjectionMatrix();
        }
        #endregion

        #region Helpers
        private static Quaternion createLookAt(Vector3 forward, Vector3 up)
        {
            var dot = Vector3.Dot(forward, Vector3.UnitZ);

            if (Math.Abs(dot + 1) <= float.Epsilon)
            {
                // -z axis.
                return new Quaternion(up, Mathf.Pi);
            }
            if (Math.Abs(dot - 1) <= float.Epsilon)
            {
                // +z axis.
                return Quaternion.Identity;
            }

            var rotationAngle = Mathf.Acos(dot);
            var rotationAxis = Vector3.Cross(Vector3.UnitZ, forward).Normalized();
            return Quaternion.FromAxisAngle(rotationAxis, rotationAngle);
        }
        #endregion
        #endregion
    }
}