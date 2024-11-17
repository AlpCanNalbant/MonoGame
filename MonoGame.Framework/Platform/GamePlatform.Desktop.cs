// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework
{
    partial class GamePlatform
    {
        internal static GamePlatform PlatformCreate(Game game)
        {
            // (WCS Edit) For desktop platforms, set macro control check priority to DirectX instead of OpenGL.
#if WINDOWS && DIRECTX
            return new MonoGame.Framework.WinFormsGamePlatform(game);
#elif DESKTOPGL || ANGLE
            return new SdlGamePlatform(game);
#endif
        }
   }
}
