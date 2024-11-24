// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace System
{
#if (!NETCOREAPP && !NETSTANDARD2_1) || NETCOREAPP1_0 || NETCOREAPP1_1
    internal static class MathF
    {
        public const float E = (float)Math.E;
        public const float PI = (float)Math.PI;

        public static float Sqrt(float f)
            => (float)Math.Sqrt(f);

        public static float Pow(float x, float y)
            => (float)Math.Pow(x, y);

        public static float Sin(float f)
            => (float)Math.Sin(f);

        public static float Cos(float f)
            => (float)Math.Cos(f);


        public static float Tan(float f)
            => (float)Math.Tan(f);

        public static float Asin(float f)
        {
            return (float)Math.Asin(f);
        }

        public static float Acos(float f)
            => (float)Math.Acos(f);

        public static float Atan(float f)
            => (float)Math.Atan(f);

        public static float Round(float f)
            => (float)Math.Round(f);

        public static float Ceiling(float f)
            => (float)Math.Ceiling(f);

        public static float Floor(float f)
            => (float)Math.Floor(f);
    }
#endif
}
