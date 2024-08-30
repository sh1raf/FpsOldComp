using System.Collections;
using Plugins.Audio.Utils;
using UnityEngine;

namespace Plugins.Audio.Core
{
    public class UnityAudioProvider : AudioProvider
    {
        private readonly SourceAudio _sourceAudio;
        private readonly AudioSource _unitySource;
        
        public override float Volume
        {
            get => _unitySource.volume;
            set => _unitySource.volume = value;
        }

        public override bool Mute
        {
            get => _unitySource.mute;
            set => _unitySource.mute = value;
        }

        public override bool Loop
        {
            get => _unitySource.loop;
            set => _unitySource.loop = value;
        }

        public override float Pitch
        {
            get => _unitySource.pitch;
            set => _unitySource.pitch = value;
        }

        public override float Time 
        { 
            get => _unitySource.time;
            set => _unitySource.time = value;
        }

        private Coroutine _playRoutine;

        public UnityAudioProvider(SourceAudio sourceAudio)
        {
            _sourceAudio = sourceAudio;

            if (_sourceAudio.TryGetComponent(out _unitySource) == false)
            {
                _unitySource = sourceAudio.gameObject.AddComponent<AudioSource>();
            }
        }

        public override void RefreshSettings(SourceAudio.AudioSettings settings)
        {
            base.RefreshSettings(settings);

            _unitySource.SetData(settings);
        }

        public override void Play(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                AudioManagement.Instance.LogError("key is empty, Source Audio PlaySound: " + _sourceAudio.name);
                return;
            }
            
            if (_playRoutine != null)
            {
                _sourceAudio.StopCoroutine(_playRoutine);
            }
            
            _playRoutine = _sourceAudio.StartCoroutine(PlayRoutine(key));
        }

        public override void PlayOneShot(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                AudioManagement.Instance.LogError("key is empty, Source Audio PlaySound: " + _sourceAudio.name);
                return;
            }

            _playRoutine = _sourceAudio.StartCoroutine(PlayOneShotRoutine(key));
        }

        public override void Stop()
        {
            _unitySource.Stop();
        }

        private IEnumerator PlayRoutine(string key)
        {
            AudioClip _clip = null;
            
            yield return AudioManagement.Instance.GetClip(key, audioClip => _clip = audioClip);

            if (_clip == null)
            {
                AudioManagement.Instance.LogError("Audio Management not found clip at key: " + key + ",\n Source Audio PlaySound: " + _sourceAudio.name);

                yield break;
            }

            _unitySource.clip = _clip;
            _unitySource.Play();
        }

        private IEnumerator PlayOneShotRoutine(string key)
        {
            AudioClip _clip = null;
            
            yield return AudioManagement.Instance.GetClip(key, audioClip => _clip = audioClip);

            if (_clip == null)
            {
                AudioManagement.Instance.LogError("Audio Management not found clip at key: " + key + ",\n Source Audio PlaySound: " + _sourceAudio.name);

                yield break;
            }

            _unitySource.PlayOneShot(_clip);
        }
    }
}