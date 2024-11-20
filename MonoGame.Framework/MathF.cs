// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace System
{
#if (!NETCOREAPP && !NETSTANDARD2_1) || NETCOREAPP1_0 || NETCOREAPP1_1
    internal static class MathF
    {
        public const float E = MathF.E;
        public const float PI = MathF.PI;

        public static float Sqrt(float f)
            => MathF.Sqrt(f);

        public static float Pow(float x, float y)
            => MathF.Pow(x, y);

        public static float Sin(float f)
            => MathF.Sin(f);

        public static float Cos(float f)
            => MathF.Cos(f);

        public static float Tan(float f)
            => MathF.Tan(f);

        public static float Asin(float f)
            => MathF.Asin(f);

        public static float Acos(float f)
            => MathF.Acos(f);

        public static float Atan(float f)
            => MathF.Atan(f);

        public static float Round(float f)
            => MathF.Round(f);

        public static float Ceiling(float f)
            => MathF.Ceiling(f);

        public static float Floor(float f)
            => MathF.Floor(f);
    }
#endif
}
