using System;

namespace Maths
{
    public class Vector2f
    {
        private float x;
        private float y;

        public Vector2f()
        {
            x = 0.0f;
            y = 0.0f;
        }

        public Vector2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float this[int index]
        {
            get
            {
                if (index == 0)
                    return x;
                else if (index == 1)
                    return y;
                else
                    return 0.0f;
            }
        }

        public static Vector2f operator +(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2f operator -(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2f operator -(Vector2f vector)
        {
            return new Vector2f(-vector.x, -vector.y);
        }

        public static Vector2f operator *(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(lhs.x * rhs.x, lhs.y * rhs.y);
        }

        public static Vector2f operator /(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(lhs.x / rhs.x, lhs.y / rhs.y);
        }

        public static Vector2f operator *(Vector2f vector, float scalar)
        {
            return new Vector2f(vector.x * scalar, vector.y * scalar);
        }

        public static Vector2f operator /(Vector2f vector, float divisor)
        {
            return new Vector2f(vector.x / divisor, vector.y / divisor);
        }

/*        public static Vector2f operator +=(Vector2f lhs, Vector2f rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        public static Vector2f operator -=(Vector2f lhs, Vector2f rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        public static Vector2f operator *=(Vector2f vector, float scalar)
        {
            vector.x *= scalar;
            vector.y *= scalar;
            return vector;
        }

        public static Vector2f operator /=(Vector2f vector, float divisor)
        {
            vector.x /= divisor;
            vector.y /= divisor;
            return vector;
        }
*/
        public float Dot(Vector2f rhs)
        {
            return x * rhs.x + y * rhs.y;
        }

        public float Cross(Vector2f rhs)
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

        public Vector2f Normalize()
        {
            float magnitude = Magnitude();
            return new Vector2f((x / magnitude), (y / magnitude));
        }

        public float Distance(Vector2f rhs)
        {
            return (float)Math.Sqrt((x - rhs.x) * (x - rhs.x) + (y - rhs.y) * (y - rhs.y));
        }

        public float DistanceSquared(Vector2f rhs)
        {
            return (x - rhs.x) * (x - rhs.x) + (y - rhs.y) * (y - rhs.y);
        }

        public float Angle(Vector2f rhs)
        {
            return (float)Math.Acos(Dot(rhs) / (Magnitude() * rhs.Magnitude()));
        }

        public Vector2f Rotate(float angle)
        {
            float radianAngle = angle * (float)Math.PI / 180.0f;
            return new Vector2f(x * (float)Math.Cos(radianAngle) - y * (float)Math.Sin(radianAngle),
                               x * (float)Math.Sin(radianAngle) + y * (float)Math.Cos(radianAngle));
        }

        public static float Dot(Vector2f lhs, Vector2f rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static Vector2f Lerp(Vector2f lhs, Vector2f rhs, float alpha)
        {
            return new Vector2f(lhs.x * (1.0f - alpha) + rhs.x * alpha, lhs.y * (1.0f - alpha) + rhs.y * alpha);
        }

        public static Vector2f LerpUnclamped(Vector2f vector1f, Vector2f Vector2f, double t)
        {
            float floaterpolatedX = (float)((1.0f - t) * vector1f.x + t * Vector2f.x);
            float floaterpolatedY = (float)((1.0f - t) * vector1f.y + t * Vector2f.y);

            return new Vector2f(floaterpolatedX, floaterpolatedY);
        }

        public static Vector2f ClampMagnitude(Vector2f vector, double maxMagnitude)
        {
            double currentMagnitude = vector.Magnitude();

            if (currentMagnitude > maxMagnitude)
            {
                double scaleFactor = maxMagnitude / currentMagnitude;

                return new Vector2f((float)(vector.x * scaleFactor), (float)(vector.y * scaleFactor));
            }
            return new Vector2f(vector.x, vector.y);
        }

        public static Vector2f Max(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(lhs.x > rhs.x ? lhs.x : rhs.x, lhs.y > rhs.y ? lhs.y : rhs.y);
        }

        public static Vector2f Min(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(lhs.x < rhs.x ? lhs.x : rhs.x, lhs.y < rhs.y ? lhs.y : rhs.y);
        }

        public static Vector2f Reflect(Vector2f Vector2f, Vector2f normal)
        {
            float dotProduct = Dot(Vector2f, normal);
            float reflectX = Vector2f.x - 2 * dotProduct * normal.x;
            float reflectY = Vector2f.y - 2 * dotProduct * normal.y;

            return new Vector2f(reflectX, reflectY);
        }

        public static Vector2f Perpendicular(Vector2f Vector2f)
        {
            return new Vector2f(-Vector2f.y, Vector2f.x);
        }

        public static Vector2f Scale(Vector2f vector1f, Vector2f Vector2f)
        {
            return new Vector2f(vector1f.x * Vector2f.x, vector1f.y * Vector2f.y);
        }

        public static double SignedAngle(Vector2f from, Vector2f to)
        {
            double angle = Math.Atan2(from.y, from.x) - Math.Atan2(to.y, to.x);
            return angle;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public void SetX(float new_x)
        {
            x = new_x;
        }

        public void SetY(float new_y)
        {
            y = new_y;
        }

        public void SetXY(float new_x, float new_y)
        {
            x = new_x;
            y = new_y;
        }

        public void SetXY(Vector2f rhs)
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

        public static readonly Vector2f Zero = new Vector2f(0.0f, 0.0f);
        public static readonly Vector2f One = new Vector2f(1.0f, 1.0f);
        public static readonly Vector2f UnitX = new Vector2f(1.0f, 0.0f);
        public static readonly Vector2f UnitY = new Vector2f(0.0f, 1.0f);
        public static readonly Vector2f Up = new Vector2f(0.0f, 1.0f);
        public static readonly Vector2f Down = new Vector2f(0.0f, -1.0f);
        public static readonly Vector2f Left = new Vector2f(-1.0f, 0.0f);
        public static readonly Vector2f Right = new Vector2f(1.0f, 0.0f);
        public static readonly Vector2f DiagonaleLeft = new Vector2f(-1.0f, 1.0f);
    }
}