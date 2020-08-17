using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour, IAnyPlayAudioListener
{
    [SerializeField] private List<AudioClip> _audioClips;

    private GameEntity _audioListener;

    private void Start()
    {
        var contexts = Contexts.sharedInstance;

        _audioListener = contexts.game.CreateEntity();
        _audioListener.AddAnyPlayAudioListener(this);
    }

    public void OnAnyPlayAudio(GameEntity entity, string value)
    {
        var indexOf = _audioClips.FindIndex(x => x.name == value);

        if (indexOf >= 0)
        {
            var audioClip = _audioClips[indexOf];

            AudioSource audioSource = null;
            var availableAudioSources = GetComponents<AudioSource>();

            foreach (var availableAudioSource in availableAudioSources)
            {
                if (!availableAudioSource.isPlaying) audioSource = availableAudioSource;
            }

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = audioSource.pitch = 1;

            audioSource.clip = audioClip;
            audioSource.Play();
        }

        entity.Destroy();
    }
}