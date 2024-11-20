// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// An efficient mathematical representation for three dimensional rotations.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct Quaternion : IEquatable<Quaternion>
    {
        #region Private Fields

        private static readonly Quaternion _identity = new(0f, 0f, 0f, 1f);

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember]
        public float X;

        /// <summary>
        /// The y coordinate of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember]
        public float Y;

        /// <summary>
        /// The z coordinate of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember]
        public float Z;

        /// <summary>
        /// The rotation component of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember]
        public float W;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a quaternion with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a quaternion with X, Y, Z from <see cref="Vector3"/> and rotation component from a scalar.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(Vector3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a quaternion from <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space and the rotation component.</param>
        public Quaternion(Vector4 value)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = value.W;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a quaternion representing no rotation.
        /// </summary>
        public static Quaternion Identity
            => _identity;

        #endregion

        #region Internal Properties

        internal readonly string DebugDisplayString
        {
            get
            {
                if (this == Quaternion._identity)
                {
                    return "Identity";
                }

                return string.Concat(
                    this.X.ToString(), " ",
                    this.Y.ToString(), " ",
                    this.Z.ToString(), " ",
                    this.W.ToString()
                );
            }
        }

        #endregion

        #region Public Methods

        #region Add

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion addition.</returns>
        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
            => new(
                quaternion1.X + quaternion2.X,
                quaternion1.Y + quaternion2.Y,
                quaternion1.Z + quaternion2.Z,
                quaternion1.W + quaternion2.W
            );

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion addition as an output parameter.</param>
        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
            => result = new(
                quaternion1.X + quaternion2.X,
                quaternion1.Y + quaternion2.Y,
                quaternion1.Z + quaternion2.Z,
                quaternion1.W + quaternion2.W
            );

        #endregion

        #region Concatenate

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
        /// <returns>The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation.</returns>
        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
            => new(
                ((value2.X * value1.W) + (value1.X * value2.W)) + ((value2.Y * value1.Z) - (value2.Z * value1.Y)),
                ((value2.Y * value1.W) + (value1.Y * value2.W)) + ((value2.Z * value1.X) - (value2.X * value1.Z)),
                ((value2.Z * value1.W) + (value1.Z * value2.W)) + ((value2.X * value1.Y) - (value2.Y * value1.X)),
                (value2.W * value1.W) - (((value2.X * value1.X) + (value2.Y * value1.Y)) + (value2.Z * value1.Z))
            );

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="result">The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation as an output parameter.</param>
        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
            => result = new(
                ((value2.X * value1.W) + (value1.X * value2.W)) + ((value2.Y * value1.Z) - (value2.Z * value1.Y)),
                ((value2.Y * value1.W) + (value1.Y * value2.W)) + ((value2.Z * value1.X) - (value2.X * value1.Z)),
                ((value2.Z * value1.W) + (value1.Z * value2.W)) + ((value2.X * value1.Y) - (value2.Y * value1.X)),
                (value2.W * value1.W) - (((value2.X * value1.X) + (value2.Y * value1.Y)) + (value2.Z * value1.Z))
            );

        #endregion

        #region Conjugate

        /// <summary>
        /// Transforms this quaternion into its conjugated version.
        /// </summary>
        public void Conjugate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <returns>The conjugate version of the specified quaternion.</returns>
        public static Quaternion Conjugate(Quaternion value)
            => new(-value.X, -value.Y, -value.Z, value.W);

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <param name="result">The conjugated version of the specified quaternion as an output parameter.</param>
        public static void Conjugate(ref Quaternion value, out Quaternion result)
            => result = new(
                -value.X,
                -value.Y,
                -value.Z,
                value.W
            );

        #endregion

        #region CreateFromAxisAngle

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>The new quaternion builded from axis and angle.</returns>
        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            float half = angle * 0.5f;
            float sin = MathF.Sin(half);
            return new Quaternion(axis.X * sin, axis.Y * sin, axis.Z * sin, MathF.Cos(half));
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <param name="result">The new quaternion builded from axis and angle as an output parameter.</param>
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            float half = angle * 0.5f;
            float sin = MathF.Sin(half);
            result = new(axis.X * sin, axis.Y * sin, axis.Z * sin, MathF.Cos(half));
        }

        #endregion

        #region CreateFromRotationMatrix

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>A quaternion composed from the rotation part of the matrix.</returns>
        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            Quaternion quaternion;
            float sqrt;
            float half;
            float scale = matrix.M11 + matrix.M22 + matrix.M33;

            if (scale > 0.0f)
            {
                sqrt = MathF.Sqrt(scale + 1.0f);
                quaternion.W = sqrt * 0.5f;
                sqrt = 0.5f / sqrt;

                quaternion.X = (matrix.M23 - matrix.M32) * sqrt;
                quaternion.Y = (matrix.M31 - matrix.M13) * sqrt;
                quaternion.Z = (matrix.M12 - matrix.M21) * sqrt;

                return quaternion;
            }
            if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
                half = 0.5f / sqrt;
                return new(
                    0.5f * sqrt,
                    (matrix.M12 + matrix.M21) * half,
                    (matrix.M13 + matrix.M31) * half,
                    (matrix.M23 - matrix.M32) * half
                );
            }
            if (matrix.M22 > matrix.M33)
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
                half = 0.5f / sqrt;
                return new(
                    (matrix.M21 + matrix.M12) * half,
                    0.5f * sqrt,
                    (matrix.M32 + matrix.M23) * half,
                    (matrix.M31 - matrix.M13) * half
                );
            }
            sqrt = MathF.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
            half = 0.5f / sqrt;
            return new(
                (matrix.M31 + matrix.M13) * half,
                (matrix.M32 + matrix.M23) * half,
                0.5f * sqrt,
                (matrix.M12 - matrix.M21) * half
            );
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <param name="result">A quaternion composed from the rotation part of the matrix as an output parameter.</param>
        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            float sqrt;
            float half;
            float scale = matrix.M11 + matrix.M22 + matrix.M33;

            if (scale > 0.0f)
            {
                sqrt = MathF.Sqrt(scale + 1.0f);
                result.W = sqrt * 0.5f;
                sqrt = 0.5f / sqrt;

                result.X = (matrix.M23 - matrix.M32) * sqrt;
                result.Y = (matrix.M31 - matrix.M13) * sqrt;
                result.Z = (matrix.M12 - matrix.M21) * sqrt;
            }
            else
            if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
                half = 0.5f / sqrt;

                result = new(
                    0.5f * sqrt,
                    (matrix.M12 + matrix.M21) * half,
                    (matrix.M13 + matrix.M31) * half,
                    (matrix.M23 - matrix.M32) * half
                );
            }
            else if (matrix.M22 > matrix.M33)
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
                half = 0.5f / sqrt;

                result = new(
                    (matrix.M21 + matrix.M12) * half,
                    0.5f * sqrt,
                    (matrix.M32 + matrix.M23) * half,
                    (matrix.M31 - matrix.M13) * half
                );
            }
            else
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
                half = 0.5f / sqrt;

                result = new(
                    (matrix.M31 + matrix.M13) * half,
                    (matrix.M32 + matrix.M23) * half,
                    0.5f * sqrt,
                    (matrix.M12 - matrix.M21) * half
                );
            }
        }

        #endregion

        #region CreateFromYawPitchRoll

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <returns>A new quaternion from the concatenated yaw, pitch, and roll angles.</returns>
        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            float halfRoll = roll * 0.5f;
            float halfPitch = pitch * 0.5f;
            float halfYaw = yaw * 0.5f;

            float sinRoll = MathF.Sin(halfRoll);
            float cosRoll = MathF.Cos(halfRoll);
            float sinPitch = MathF.Sin(halfPitch);
            float cosPitch = MathF.Cos(halfPitch);
            float sinYaw = MathF.Sin(halfYaw);
            float cosYaw = MathF.Cos(halfYaw);

            return new Quaternion((cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
                                  (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
                                  (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll),
                                  (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll));
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <param name="result">A new quaternion from the concatenated yaw, pitch, and roll angles as an output parameter.</param>
 		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            float halfRoll = roll * 0.5f;
            float halfPitch = pitch * 0.5f;
            float halfYaw = yaw * 0.5f;

            float sinRoll = MathF.Sin(halfRoll);
            float cosRoll = MathF.Cos(halfRoll);
            float sinPitch = MathF.Sin(halfPitch);
            float cosPitch = MathF.Cos(halfPitch);
            float sinYaw = MathF.Sin(halfYaw);
            float cosYaw = MathF.Cos(halfYaw);

            result = new(
                (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
                (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
                (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll),
                (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll)
            );
        }

        #endregion

        #region Divide

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion"/>.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
		    float num5 = 1f / ((((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W));
		    float num4 = -quaternion2.X * num5;
		    float num3 = -quaternion2.Y * num5;
		    float num2 = -quaternion2.Z * num5;
		    float num = quaternion2.W * num5;
            return new Quaternion(
		        ((quaternion1.X * num) + (num4 * quaternion1.W)) + ((quaternion1.Y * num2) - (quaternion1.Z * num3)),
		        ((quaternion1.Y * num) + (num3 * quaternion1.W)) + ((quaternion1.Z * num4) - (quaternion1.X * num2)),
		        ((quaternion1.Z * num) + (num2 * quaternion1.W)) + ((quaternion1.X * num3) - (quaternion1.Y * num4)),
		        (quaternion1.W * num) - (((quaternion1.X * num4) + (quaternion1.Y * num3)) + (quaternion1.Z * num2))
            );
        }

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of dividing the quaternions as an output parameter.</param>
        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            float num5 = 1f / ((((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W));
            float num4 = -quaternion2.X * num5;
            float num3 = -quaternion2.Y * num5;
            float num2 = -quaternion2.Z * num5;
            float num = quaternion2.W * num5;
            result = new(
                ((quaternion1.X * num) + (num4 * quaternion1.W)) + ((quaternion1.Y * num2) - (quaternion1.Z * num3)),
                ((quaternion1.Y * num) + (num3 * quaternion1.W)) + ((quaternion1.Z * num4) - (quaternion1.X * num2)),
                ((quaternion1.Z * num) + (num2 * quaternion1.W)) + ((quaternion1.X * num3) - (quaternion1.Y * num4)),
                (quaternion1.W * num) - (((quaternion1.X * num4) + (quaternion1.Y * num3)) + (quaternion1.Z * num2))
            );
        }

        #endregion

        #region Dot

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <returns>The dot product of two quaternions.</returns>
        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
            => ((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W));

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <param name="result">The dot product of two quaternions as an output parameter.</param>
        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out float result)
            => result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);

        #endregion

        #region Equals

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override readonly bool Equals(object obj)
        {
            if (obj is Quaternion quaternion)
                return Equals(quaternion);
            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="other">The <see cref="Quaternion"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public readonly bool Equals(Quaternion other)
            => X == other.X &&
               Y == other.Y &&
               Z == other.Z &&
               W == other.W;

        #endregion

        /// <summary>
        /// Gets the hash code of this <see cref="Quaternion"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Quaternion"/>.</returns>
        public override readonly int GetHashCode()
            => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();

        #region Inverse

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <returns>The inverse quaternion.</returns>
        public static Quaternion Inverse(Quaternion quaternion)
        {
            float num = 1f / ((((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W));
            return new(
                -quaternion.X * num,
                -quaternion.Y * num,
                -quaternion.Z * num,
                quaternion.W * num
            );
        }

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The inverse quaternion as an output parameter.</param>
        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            float num = 1f / ((((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W));
            result = new(
                -quaternion.X * num,
                -quaternion.Y * num,
                -quaternion.Z * num,
                quaternion.W * num
            );
        }

        #endregion

        /// <summary>
        /// Returns the magnitude of the quaternion components.
        /// </summary>
        /// <returns>The magnitude of the quaternion components.</returns>
        public readonly float Length()
            => MathF.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));

        /// <summary>
        /// Returns the squared magnitude of the quaternion components.
        /// </summary>
        /// <returns>The squared magnitude of the quaternion components.</returns>
        public readonly float LengthSquared()
            => (X * X) + (Y * Y) + (Z * Z) + (W * W);

        #region Lerp

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <returns>The result of linear blending between two quaternions.</returns>
        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            float num = amount;
            float num2 = 1f - num;
            Quaternion quaternion;
            if (((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W)) >= 0f)
            {
                quaternion = new(
                    (num2 * quaternion1.X) + (num * quaternion2.X),
                    (num2 * quaternion1.Y) + (num * quaternion2.Y),
                    (num2 * quaternion1.Z) + (num * quaternion2.Z),
                    (num2 * quaternion1.W) + (num * quaternion2.W)
                );
            }
            else
            {
                quaternion = new(
                    (num2 * quaternion1.X) - (num * quaternion2.X),
                    (num2 * quaternion1.Y) - (num * quaternion2.Y),
                    (num2 * quaternion1.Z) - (num * quaternion2.Z),
                    (num2 * quaternion1.W) - (num * quaternion2.W)
                );
            }
            float num3 = 1f / MathF.Sqrt((((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W));
            return new(
                quaternion.X * num3,
                quaternion.Y * num3,
                quaternion.Z * num3,
                quaternion.W * num3
            );
        }

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <param name="result">The result of linear blending between two quaternions as an output parameter.</param>
        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            float num = amount;
            float num2 = 1f - num;
            if (((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W)) >= 0f)
            {
                result = new(
                    (num2 * quaternion1.X) + (num * quaternion2.X),
                    (num2 * quaternion1.Y) + (num * quaternion2.Y),
                    (num2 * quaternion1.Z) + (num * quaternion2.Z),
                    (num2 * quaternion1.W) + (num * quaternion2.W)
                );
            }
            else
            {
                result = new(
                    (num2 * quaternion1.X) - (num * quaternion2.X),
                    (num2 * quaternion1.Y) - (num * quaternion2.Y),
                    (num2 * quaternion1.Z) - (num * quaternion2.Z),
                    (num2 * quaternion1.W) - (num * quaternion2.W)
                );
            }
            float num3 = 1f / MathF.Sqrt((((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W));
            result = new(
                result.X * num3,
                result.Y * num3,
                result.Z * num3,
                result.W * num3
            );
        }

        #endregion

        #region Slerp

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <returns>The result of spherical linear blending between two quaternions.</returns>
        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            float num2;
            float num3;
            float num = amount;
            float num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            bool flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                float num5 = MathF.Acos(num4);
                float num6 = (1.0f / MathF.Sin(num5));
                num3 = MathF.Sin((1f - num) * num5) * num6;
                num2 = flag ? (-MathF.Sin(num * num5) * num6) : (MathF.Sin(num * num5) * num6);
            }
            return new(
                (num3 * quaternion1.X) + (num2 * quaternion2.X),
                (num3 * quaternion1.Y) + (num2 * quaternion2.Y),
                (num3 * quaternion1.Z) + (num2 * quaternion2.Z),
                (num3 * quaternion1.W) + (num2 * quaternion2.W)
            );
        }

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <param name="result">The result of spherical linear blending between two quaternions as an output parameter.</param>
        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            float num2;
            float num3;
            float num = amount;
            float num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            bool flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                float num5 = MathF.Acos(num4);
                float num6 = (1.0f / MathF.Sin(num5));
                num3 = MathF.Sin((1f - num) * num5) * num6;
                num2 = flag ? (-MathF.Sin(num * num5) * num6) : (MathF.Sin(num * num5) * num6);
            }
            result = new(
                (num3 * quaternion1.X) + (num2 * quaternion2.X),
                (num3 * quaternion1.Y) + (num2 * quaternion2.Y),
                (num3 * quaternion1.Z) + (num2 * quaternion2.Z),
                (num3 * quaternion1.W) + (num2 * quaternion2.W)
            );
        }

        #endregion

        #region Subtract

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion subtraction.</returns>
        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
            => new(
                quaternion1.X - quaternion2.X,
                quaternion1.Y - quaternion2.Y,
                quaternion1.Z - quaternion2.Z,
                quaternion1.W - quaternion2.W
            );

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion subtraction as an output parameter.</param>
        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
            => result = new(
                quaternion1.X - quaternion2.X,
                quaternion1.Y - quaternion2.Y,
                quaternion1.Z - quaternion2.Z,
                quaternion1.W - quaternion2.W
            );
        #endregion

        #region Multiply

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion multiplication.</returns>
        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
            => new(
                ((quaternion1.X * quaternion2.W) + (quaternion2.X * quaternion1.W)) + ((quaternion1.Y * quaternion2.Z) - (quaternion1.Z * quaternion2.Y)),
                ((quaternion1.Y * quaternion2.W) + (quaternion2.Y * quaternion1.W)) + ((quaternion1.Z * quaternion2.X) - (quaternion1.X * quaternion2.Z)),
                ((quaternion1.Z * quaternion2.W) + (quaternion2.Z * quaternion1.W)) + ((quaternion1.X * quaternion2.Y) - (quaternion1.Y * quaternion2.X)),
                (quaternion1.W * quaternion2.W) - (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z))
            );

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the quaternion multiplication with a scalar.</returns>
        public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
            => new(
                quaternion1.X * scaleFactor,
                quaternion1.Y * scaleFactor,
                quaternion1.Z * scaleFactor,
                quaternion1.W * scaleFactor
            );

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the quaternion multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
            => result = new(
                quaternion1.X * scaleFactor,
                quaternion1.Y * scaleFactor,
                quaternion1.Z * scaleFactor,
                quaternion1.W * scaleFactor
            );

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion multiplication as an output parameter.</param>
        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
            => result = new(
                ((quaternion1.X * quaternion2.W) + (quaternion2.X * quaternion1.W)) + ((quaternion1.Y * quaternion2.Z) - (quaternion1.Z * quaternion2.Y)),
                ((quaternion1.Y * quaternion2.W) + (quaternion2.Y * quaternion1.W)) + ((quaternion1.Z * quaternion2.X) - (quaternion1.X * quaternion2.Z)),
                ((quaternion1.Z * quaternion2.W) + (quaternion2.Z * quaternion1.W)) + ((quaternion1.X * quaternion2.Y) - (quaternion1.Y * quaternion2.X)),
                (quaternion1.W * quaternion2.W) - (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z))
            );

        #endregion

        #region Negate

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static Quaternion Negate(Quaternion quaternion)
            => new(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion negation as an output parameter.</param>
        public static void Negate(ref Quaternion quaternion, out Quaternion result)
            => result = new(
                -quaternion.X,
                -quaternion.Y,
                -quaternion.Z,
                -quaternion.W
            );

        #endregion

        #region Normalize

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        public void Normalize()
        {
            float num = 1f / MathF.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
            X *= num;
            Y *= num;
            Z *= num;
            W *= num;
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <returns>The unit length quaternion.</returns>
        public static Quaternion Normalize(Quaternion quaternion)
        {
            float num = 1f / MathF.Sqrt((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W));
            return new(quaternion.X * num, quaternion.Y * num, quaternion.Z * num, quaternion.W * num);
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The unit length quaternion an output parameter.</param>
        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            float num = 1f / MathF.Sqrt((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W));
            result = new(quaternion.X * num, quaternion.Y * num, quaternion.Z * num, quaternion.W * num);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Quaternion"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Z:[<see cref="Z"/>] W:[<see cref="W"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Quaternion"/>.</returns>
        public override readonly string ToString()
            => "{X:" + X + " Y:" + Y + " Z:" + Z + " W:" + W + "}";

        /// <summary>
        /// Gets a <see cref="Vector4"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Vector4"/> representation for this object.</returns>
        public readonly Vector4 ToVector4()
            => new(X, Y, Z, W);

        /// <summary>
        /// Deconstruction method for <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public readonly void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Quaternion"/>.
        /// </summary>
        public readonly System.Numerics.Quaternion ToNumerics()
            => new(this.X, this.Y, this.Z, this.W);

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Quaternion"/> to a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator Quaternion(System.Numerics.Quaternion value)
            => new(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the add sign.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
            => new(
                quaternion1.X + quaternion2.X,
                quaternion1.Y + quaternion2.Y,
                quaternion1.Z + quaternion2.Z,
                quaternion1.W + quaternion2.W
            );

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the div sign.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            float num5 = 1f / ((((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W));
            float num4 = -quaternion2.X * num5;
            float num3 = -quaternion2.Y * num5;
            float num2 = -quaternion2.Z * num5;
            float num = quaternion2.W * num5;
            return new(
                ((quaternion1.X * num) + (num4 * quaternion1.W)) + ((quaternion1.Y * num2) - (quaternion1.Z * num3)),
                ((quaternion1.Y * num) + (num3 * quaternion1.W)) + ((quaternion1.Z * num4) - (quaternion1.X * num2)),
                ((quaternion1.Z * num) + (num2 * quaternion1.W)) + ((quaternion1.X * num3) - (quaternion1.Y * num4)),
                (quaternion1.W * num) - (((quaternion1.X * num4) + (quaternion1.Y * num3)) + (quaternion1.Z * num2))
            );
        }

        /// <summary>
        /// Compares whether two <see cref="Quaternion"/> instances are equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="Quaternion"/> instance on the left of the equal sign.</param>
        /// <param name="quaternion2"><see cref="Quaternion"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
            => ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));

        /// <summary>
        /// Compares whether two <see cref="Quaternion"/> instances are not equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="Quaternion"/> instance on the left of the not equal sign.</param>
        /// <param name="quaternion2"><see cref="Quaternion"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
            => quaternion1.X != quaternion2.X || quaternion1.Y != quaternion2.Y || quaternion1.Z != quaternion2.Z || (quaternion1.W != quaternion2.W);

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the mul sign.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/> on the right of the mul sign.</param>
        /// <returns>Result of the quaternions multiplication.</returns>
        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
            => new(
                ((quaternion1.X * quaternion2.W) + (quaternion2.X * quaternion1.W)) + ((quaternion1.Y * quaternion2.Z) - (quaternion1.Z * quaternion2.Y)),
                ((quaternion1.Y * quaternion2.W) + (quaternion2.Y * quaternion1.W)) + ((quaternion1.Z * quaternion2.X) - (quaternion1.X * quaternion2.Z)),
                ((quaternion1.Z * quaternion2.W) + (quaternion2.Z * quaternion1.W)) + ((quaternion1.X * quaternion2.Y) - (quaternion1.Y * quaternion2.X)),
                (quaternion1.W * quaternion2.W) - (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z))
            );

        /// <summary>
        /// Multiplies the components of quaternion by a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Vector3"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the quaternion multiplication with a scalar.</returns>
        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
            => new(
                quaternion1.X * scaleFactor,
                quaternion1.Y * scaleFactor,
                quaternion1.Z * scaleFactor,
                quaternion1.W * scaleFactor
            );

        /// <summary>
        /// Subtracts a <see cref="Quaternion"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Vector3"/> on the left of the sub sign.</param>
        /// <param name="quaternion2">Source <see cref="Vector3"/> on the right of the sub sign.</param>
        /// <returns>Result of the quaternion subtraction.</returns>
        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
            => new(
                quaternion1.X - quaternion2.X,
                quaternion1.Y - quaternion2.Y,
                quaternion1.Z - quaternion2.Z,
                quaternion1.W - quaternion2.W
            );

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/> on the right of the sub sign.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static Quaternion operator -(Quaternion quaternion)
            => new(
                -quaternion.X,
                -quaternion.Y,
                -quaternion.Z,
                -quaternion.W
            );

        #endregion
    }
}
