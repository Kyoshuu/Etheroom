using Microsoft.Xna.Framework;

namespace Etheroom.Utility
{
    public class VectorRef
    {
        private Vector3 vector;

        public float X
        {
            get
            {
                return vector.X;
            }
        }

        public float Y
        {
            get
            {
                return vector.Y;
            }
        }

        public float Z
        {
            get
            {
                return vector.Z;
            }
        }

        public Vector2 Vector2
        {
            get
            {
                return new Vector2(vector.X, vector.Y);
            }
        }

        public Vector3 Vector3
        {
            get
            {
                return vector;
            }
        }

        public VectorRef()
        {
            vector = Vector3.Zero;
        }

        public VectorRef(Vector2 vector)
        {
            SetPosition(vector);
        }

        public VectorRef(Vector3 vector)
        {
            SetPosition(vector);
        }

        public void SetPosition(Vector2 position)
        {
            vector.X = position.X;
            vector.Y = position.Y;
        }

        public void SetPosition(Vector3 vector)
        {
            this.vector.X = vector.X;
            this.vector.Y = vector.Y;
            this.vector.Z = vector.Z;
        }

        public override string ToString()
        {
            return $"Vector3({X},{Y},{Z})";
        }
    }
}
