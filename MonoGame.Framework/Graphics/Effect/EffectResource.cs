// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.IO;
using MonoGame.Framework.Utilities;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Internal helper for accessing the bytecode for stock effects.
    /// </summary>
    internal partial class EffectResource
    {
        public static readonly EffectResource AlphaTestEffect = new(AlphaTestEffectName);
        public static readonly EffectResource BasicEffect = new(BasicEffectName);
        public static readonly EffectResource DualTextureEffect = new(DualTextureEffectName);
        public static readonly EffectResource EnvironmentMapEffect = new(EnvironmentMapEffectName);
        public static readonly EffectResource SkinnedEffect = new(SkinnedEffectName);
        public static readonly EffectResource SpriteEffect = new(SpriteEffectName);

        private readonly object _locker = new();
        private readonly string _name;
        private volatile byte[] _bytecode;

        private EffectResource(string name)
            => _name = name;

        public byte[] Bytecode
        {
            get
            {
                if (_bytecode == null)
                {
                    lock (_locker)
                    {
                        if (_bytecode != null)
                            return _bytecode;

<<<<<<< HEAD
                        var assembly = ReflectionHelpers.GetAssembly(typeof(EffectResource));

                        var stream = assembly.GetManifestResourceStream(_name);
                        using var ms = new MemoryStream();
                        stream.CopyTo(ms);
                        _bytecode = ms.ToArray();
=======
                        _bytecode = PlatformGetBytecode(_name);
>>>>>>> c1ae93de0ab4fd0b60ebf4a693bc4ea5fb69a791
                    }
                }

                return _bytecode;
            }
        }

#if !NATIVE
        private static byte[] PlatformGetBytecode(string name)
        {            
            var assembly = ReflectionHelpers.GetAssembly(typeof(EffectResource));

            var stream = assembly.GetManifestResourceStream(name);
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
#endif
    }
}
