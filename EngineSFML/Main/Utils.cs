using System;
using System.Collections.Generic;
using System.Text;

using SFML.System;

namespace EngineSFML.Main
{
    public static class Utils
    {

        public enum Direction
        {
            right,
            top,
            left,
            bottom
        }

        public static readonly Vector2f RightVector = new Vector2f(1.0f, 0.0f);
        public static readonly Vector2f TopVector = new Vector2f(0.0f, -1.0f);
        public static readonly Vector2f LeftVector = new Vector2f(-1.0f, 0.0f);
        public static readonly Vector2f BottomVector = new Vector2f(0.0f, 1.0f);

        public static readonly Vector2f[] SidesVectors = { RightVector, TopVector, LeftVector, BottomVector };

        public static Vector2f Normalize(Vector2f vec)
        {
            float vectorLen = MathF.Sqrt(MathF.Pow(vec.X, 2.0f) + MathF.Pow(vec.Y, 2.0f));
            Vector2f outVec = vec / vectorLen;

            return outVec;
        }

        public static float[] AnalazeVector(Vector2f vec)
        {
            float[] angles = new float[4];

            for (int i = 0; i < 4; ++i)
            {
                angles[i] = MathF.Acos((vec.X * SidesVectors[i].X) + (vec.Y * SidesVectors[i].Y));
            }

            return angles;
        }

        public static float AngleBetween(Vector2f vec1, Vector2f vec2)
        {
            return MathF.Acos((vec1.X * vec2.X) + (vec1.Y + vec2.Y));
        }

        public static Vector2f DirectionByAngle(float angle)
        {
            return new Vector2f(MathF.Cos(angle), MathF.Sin(angle));
        }
        public static float MinMax(float _min, float _max, float value)
        {
            return MathF.Min(MathF.Max(value, _min), _max);
        }

        public static float MinMaxOne(float value)
        {
            return MinMax(-1.0f, 1.0f, value);
        }

        public static float RadToGrad(float rad)
        {
            return rad * (180.0f / MathF.PI);
        }

        public static float GradToRad(float grad)
        {
            return grad * (MathF.PI / 180.0f);
        }

        public static int GetMinIndexInArray(float[] arr)
        {
            float minValue = arr[0];
            int index = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i] < minValue)
                {
                    minValue = arr[i];
                    index = i;
                }
            }

            return index;
        }

        public static int GetMaxIndexInArray(float[] arr)
        {
            float maxValue = arr[0];
            int index = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i] > maxValue)
                {
                    maxValue = arr[i];
                    index = i;
                }
            }

            return index;
        }

        public static float Distance(Vector2f point1, Vector2f point2)
        {
            return MathF.Sqrt(MathF.Pow(point2.X - point1.X, 2) + MathF.Pow(point2.Y - point1.Y, 2));
        }
    }
}
