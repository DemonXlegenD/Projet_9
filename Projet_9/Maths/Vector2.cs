using System;

namespace Maths
{
    public class Vector2
    {
        public int x;
        public int y;

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(int x, int y)
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

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.x, -vector.y);
        }

        public static Vector2 operator *(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x * rhs.x, lhs.y * rhs.y);
        }

        public static Vector2 operator /(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x / rhs.x, lhs.y / rhs.y);
        }

        public static Vector2 operator *(Vector2 vector, int scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }

        public static Vector2 operator /(Vector2 vector, int divisor)
        {
            return new Vector2(vector.x / divisor, vector.y / divisor);
        }

/*        public static Vector2 operator +=(Vector2 lhs, Vector2 rhs)
        {
            lhs.x += rhs.x;
            lhs.y += rhs.y;
            return lhs;
        }

        public static Vector2 operator -=(Vector2 lhs, Vector2 rhs)
        {
            lhs.x -= rhs.x;
            lhs.y -= rhs.y;
            return lhs;
        }

        public static Vector2 operator *=(Vector2 vector, int scalar)
        {
            vector.x *= scalar;
            vector.y *= scalar;
            return vector;
        }

        public static Vector2 operator /=(Vector2 vector, int divisor)
        {
            vector.x /= divisor;
            vector.y /= divisor;
            return vector;
        }
*/
        public float Dot(Vector2 rhs)
        {
            return x * rhs.x + y * rhs.y;
        }

        public float Cross(Vector2 rhs)
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

        public Vector2 Normalize()
        {
            float magnitude = Magnitude();
            return new Vector2((int)(x / magnitude), (int)(y / magnitude));
        }

        public float Distance(Vector2 rhs)
        {
            return (float)Math.Sqrt((x - rhs.x) * (x - rhs.x) + (y - rhs.y) * (y - rhs.y));
        }

        public float DistanceSquared(Vector2 rhs)
        {
            return (x - rhs.x) * (x - rhs.x) + (y - rhs.y) * (y - rhs.y);
        }

        public float Angle(Vector2 rhs)
        {
            return (float)Math.Acos(Dot(rhs) / (Magnitude() * rhs.Magnitude()));
        }

        public Vector2 Rotate(int angle)
        {
            float radianAngle = angle * (float)Math.PI / 180.0f;
            return new Vector2(x * (int)Math.Cos(radianAngle) - y * (int)Math.Sin(radianAngle),
                               x * (int)Math.Sin(radianAngle) + y * (int)Math.Cos(radianAngle));
        }

        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static Vector2 Lerp(Vector2 lhs, Vector2 rhs, float alpha)
        {
            return new Vector2((int)lhs.x * (int)(1 - alpha) + (int)rhs.x * (int)alpha, (int)lhs.y * (int)(1 - alpha) + (int)rhs.y * (int)alpha);
        }

        public static Vector2 LerpUnclamped(Vector2 vector1, Vector2 vector2, double t)
        {
            int interpolatedX = (int)((1 - t) * vector1.x + t * vector2.x);
            int interpolatedY = (int)((1 - t) * vector1.y + t * vector2.y);

            return new Vector2(interpolatedX, interpolatedY);
        }

        public static Vector2 ClampMagnitude(Vector2 vector, double maxMagnitude)
        {
            double currentMagnitude = vector.Magnitude();

            if (currentMagnitude > maxMagnitude)
            {
                double scaleFactor = maxMagnitude / currentMagnitude;

                return new Vector2((int)(vector.x * scaleFactor), (int)(vector.y * scaleFactor));
            }
            return new Vector2(vector.x, vector.y);
        }

        public static Vector2 Max(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x > rhs.x ? lhs.x : rhs.x, lhs.y > rhs.y ? lhs.y : rhs.y);
        }

        public static Vector2 Min(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x < rhs.x ? lhs.x : rhs.x, lhs.y < rhs.y ? lhs.y : rhs.y);
        }

        public static Vector2 Reflect(Vector2 vector2, Vector2 normal)
        {
            int dotProduct = (int)Dot(vector2, normal);
            int reflectX = vector2.x - 2 * dotProduct * normal.x;
            int reflectY = vector2.y - 2 * dotProduct * normal.y;

            return new Vector2(reflectX, reflectY);
        }

        public static Vector2 Perpendicular(Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }

        public static Vector2 Scale(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x * vector2.x, vector1.y * vector2.y);
        }

        public static double SignedAngle(Vector2 from, Vector2 to)
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

        public void SetXY(Vector2 rhs)
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

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 DiagonaleLeft = new Vector2(-1, 1);
    }
}