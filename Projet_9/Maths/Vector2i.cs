using System;

namespace Maths
{
    public class Vector2i
    {
        private int x;
        private int y;

        public Vector2i()
        {
            x = 0;
            y = 0;
        }

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return x;
                else if (index == 1)
                    return y;
                else
                    return 0;
            }
        }

        public static Vector2i operator +(Vector2i lhs, Vector2i rhs)
        {
            return new Vector2i(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2i operator -(Vector2i lhs, Vector2i rhs)
        {
            return new Vector2i(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2i operator -(Vector2i vector)
        {
            return new Vector2i(-vector.x, -vector.y);
        }

        public static Vector2i operator *(Vector2i lhs, Vector2i rhs)
        {
            return new Vector2i(lhs.x * rhs.x, lhs.y * rhs.y);
        }

        public static Vector2i operator /(Vector2i lhs, Vector2i rhs)
        {
            return new Vector2i(lhs.x / rhs.x, lhs.y / rhs.y);
        }

        public static Vector2i operator *(Vector2i vector, int scalar)
        {
            return new Vector2i(vector.x * scalar, vector.y * scalar);
        }

        public static Vector2i operator /(Vector2i vector, int divisor)
        {
            return new Vector2i(vector.x / divisor, vector.y / divisor);
        }

/*        public static Vector2i operator +=(Vector2i lhs, Vector2i rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        public static Vector2i operator -=(Vector2i lhs, Vector2i rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        public static Vector2i operator *=(Vector2i vector, int scalar)
        {
            vector.x *= scalar;
            vector.y *= scalar;
            return vector;
        }

        public static Vector2i operator /=(Vector2i vector, int divisor)
        {
            vector.x /= divisor;
            vector.y /= divisor;
            return vector;
        }
*/
        public float Dot(Vector2i rhs)
        {
            return x * rhs.x + y * rhs.y;
        }

        public float Cross(Vector2i rhs)
        {
            return x * rhs.y - y * rhs.x;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public float MagnitudeSquared()
        {
            return x * x + y * y;
        }

        public Vector2i Normalize()
        {
            float magnitude = Magnitude();
            return new Vector2i((int)(x / magnitude), (int)(y / magnitude));
        }

        public float Distance(Vector2i rhs)
        {
            return (float)Math.Sqrt((x - rhs.x) * (x - rhs.x) + (y - rhs.y) * (y - rhs.y));
        }

        public float DistanceSquared(Vector2i rhs)
        {
            return (x - rhs.x) * (x - rhs.x) + (y - rhs.y) * (y - rhs.y);
        }

        public float Angle(Vector2i rhs)
        {
            return (float)Math.Acos(Dot(rhs) / (Magnitude() * rhs.Magnitude()));
        }

        public Vector2i Rotate(int angle)
        {
            float radianAngle = angle * (float)Math.PI / 180.0f;
            return new Vector2i(x * (int)Math.Cos(radianAngle) - y * (int)Math.Sin(radianAngle),
                               x * (int)Math.Sin(radianAngle) + y * (int)Math.Cos(radianAngle));
        }

        public static float Dot(Vector2i lhs, Vector2i rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static Vector2i Lerp(Vector2i lhs, Vector2i rhs, float alpha)
        {
            return new Vector2i((int)lhs.x * (int)(1 - alpha) + (int)rhs.x * (int)alpha, (int)lhs.y * (int)(1 - alpha) + (int)rhs.y * (int)alpha);
        }

        public static Vector2i LerpUnclamped(Vector2i vector1, Vector2i Vector2i, double t)
        {
            int interpolatedX = (int)((1 - t) * vector1.x + t * Vector2i.x);
            int interpolatedY = (int)((1 - t) * vector1.y + t * Vector2i.y);

            return new Vector2i(interpolatedX, interpolatedY);
        }

        public static Vector2i ClampMagnitude(Vector2i vector, double maxMagnitude)
        {
            double currentMagnitude = vector.Magnitude();

            if (currentMagnitude > maxMagnitude)
            {
                double scaleFactor = maxMagnitude / currentMagnitude;

                return new Vector2i((int)(vector.x * scaleFactor), (int)(vector.y * scaleFactor));
            }
            return new Vector2i(vector.x, vector.y);
        }

        public static Vector2i Max(Vector2i lhs, Vector2i rhs)
        {
            return new Vector2i(lhs.x > rhs.x ? lhs.x : rhs.x, lhs.y > rhs.y ? lhs.y : rhs.y);
        }

        public static Vector2i Min(Vector2i lhs, Vector2i rhs)
        {
            return new Vector2i(lhs.x < rhs.x ? lhs.x : rhs.x, lhs.y < rhs.y ? lhs.y : rhs.y);
        }

        public static Vector2i Reflect(Vector2i Vector2i, Vector2i normal)
        {
            int dotProduct = (int)Dot(Vector2i, normal);
            int reflectX = Vector2i.x - 2 * dotProduct * normal.x;
            int reflectY = Vector2i.y - 2 * dotProduct * normal.y;

            return new Vector2i(reflectX, reflectY);
        }

        public static Vector2i Perpendicular(Vector2i Vector2i)
        {
            return new Vector2i(-Vector2i.y, Vector2i.x);
        }

        public static Vector2i Scale(Vector2i vector1, Vector2i Vector2i)
        {
            return new Vector2i(vector1.x * Vector2i.x, vector1.y * Vector2i.y);
        }

        public static double SignedAngle(Vector2i from, Vector2i to)
        {
            double angle = Math.Atan2(from.y, from.x) - Math.Atan2(to.y, to.x);
            return angle;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetX(int new_x)
        {
            x = new_x;
        }

        public void SetY(int new_y)
        {
            y = new_y;
        }

        public void SetXY(int new_x, int new_y)
        {
            x = new_x;
            y = new_y;
        }

        public void SetXY(Vector2i rhs)
        {
            x = rhs.x;
            y = rhs.y;
        }

        public void ShowVector()
        {
            string result = "Vector : (" + x.ToString() + "," + y.ToString() + ")";
            // Assuming GameLog is a class with a static method log.
            // Adjust as per your actual implementation.
            Console.WriteLine(result);
        }

        public static readonly Vector2i Zero = new Vector2i(0, 0);
        public static readonly Vector2i One = new Vector2i(1, 1);
        public static readonly Vector2i UnitX = new Vector2i(1, 0);
        public static readonly Vector2i UnitY = new Vector2i(0, 1);
        public static readonly Vector2i Up = new Vector2i(0, 1);
        public static readonly Vector2i Down = new Vector2i(0, -1);
        public static readonly Vector2i Left = new Vector2i(-1, 0);
        public static readonly Vector2i Right = new Vector2i(1, 0);
        public static readonly Vector2i DiagonaleLeft = new Vector2i(-1, 1);
    }
}