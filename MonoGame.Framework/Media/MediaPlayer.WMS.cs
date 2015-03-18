// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using SharpDX;
using SharpDX.MediaFoundation;
using SharpDX.Win32;

namespace Microsoft.Xna.Framework.Media
{
    public static partial class MediaPlayer
    {
        //RAYB: This probably needs to get flipped back into a readonly.
        private static MediaSession _session;
        private static AudioStreamVolume _volumeController;
        private static PresentationClock _clock;

        // HACK: Need SharpDX to fix this.
        private static readonly Guid MRPolicyVolumeService = Guid.Parse("f8b5fa2f-32ef-46f5-b172-1321212fb2c4");
        private static readonly Guid AudioStreamVolumeGuid = Guid.Parse("76B1BBDB-4EC8-4f36-B106-70A9316DF593");

        private static Callback _callback;

        private class Callback : IAsyncCallback
        {
            public void Dispose()
            {
            }

            public IDisposable Shadow { get; set; }
            public void Invoke(AsyncResult asyncResultRef)
            {
                var ev = _session.EndGetEvent(asyncResultRef);

                if (ev.TypeInfo == MediaEventTypes.EndOfPresentation)
                    OnSongFinishedPlaying(null, null);

                _session.BeginGetEvent(this, null);
            }

            public AsyncCallbackFlags Flags { get; private set; }
            public WorkQueueId WorkQueueId { get; private set; }
        }

        private static void PlatformInitialize()
        {
            MediaManagerState.CheckStartup();
            MediaFactory.CreateMediaSession(null, out _session);
        }

        #region Properties

        private static bool PlatformGetIsMuted()
        {
            return _isMuted;
        }

        private static void PlatformSetIsMuted(bool muted)
        {
            _isMuted = muted;

            SetVolumeControllerChannelVolume();
        }

        private static bool PlatformGetIsRepeating()
        {
            return _isRepeating;
        }

        private static void PlatformSetIsRepeating(bool repeating)
        {
            _isRepeating = repeating;
        }

        private static bool PlatformGetIsShuffled()
        {
            return _isShuffled;
        }

        private static void PlatformSetIsShuffled(bool shuffled)
        {
            _isShuffled = shuffled;
        }

        private static TimeSpan PlatformGetPlayPosition()
        {
            return _clock != null ? TimeSpan.FromTicks(_clock.Time) : TimeSpan.Zero;
        }

        private static bool PlatformGetGameHasControl()
        {
            // TODO: Fix me!
            return true;
        }

        private static MediaState PlatformGetState()
        {
            return _state;
        }

        private static float PlatformGetVolume()
        {
            return _volume;
        }

        private static void SetVolumeControllerChannelVolume()
        {
            if (_volumeController != null)
            {
                float volume = _volume;
                if (IsMuted)
                    volume = 0.0f;

                for (int i = 0; i < _volumeController.ChannelCount; i++)
                {
                    _volumeController.SetChannelVolume(i, volume);
                }
            }
        }

        private static void PlatformSetVolume(float volume)
        {
            _volume = volume;
            SetVolumeControllerChannelVolume();
        }

        #endregion

        private static void PlatformPause()
        {
            _session.Pause();
        }

        private static void PlatformPlaySong(Song song)
        {
            // Cleanup the last song first.
            if (State != MediaState.Stopped)
            {
                _session.Stop();
                _session.ClearTopologies();
                _session.Close();
                _volumeController.Dispose();
                _clock.Dispose();
            }

            // Set the new song.
            _session.SetTopology(SessionSetTopologyFlags.Immediate, song.Topology);

            // Get the volume interface.
            IntPtr volumeObj;
            try
            {
                MediaFactory.GetService(_session, MRPolicyVolumeService, AudioStreamVolumeGuid, out volumeObj);
            }
            catch
            {
                MediaFactory.GetService(_session, MRPolicyVolumeService, AudioStreamVolumeGuid, out volumeObj);
            }

            _volumeController = CppObject.FromPointer<AudioStreamVolume>(volumeObj);
            SetVolumeControllerChannelVolume();

            // Get the clock.
            _clock = _session.Clock.QueryInterface<PresentationClock>();

            //create the callback if it hasn't been created yet
            if (_callback == null)
            {
                _callback = new Callback();
                _session.BeginGetEvent(_callback, null);
            }

            // Start playing.
            var varStart = new Variant();
            _session.Start(null, varStart);
        }

        private static void PlatformResume()
        {
            var varStart = new Variant();
            _session.Start(null, varStart);
        }

        private static void PlatformStop()
        {
            _session.ClearTopologies();
            _session.Stop();
            _session.Close();
            _volumeController.Dispose();
            _volumeController = null;
            _clock.Dispose();
            _clock = null;
        }
    }
}
