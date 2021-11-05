using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private AudioSource soundSource;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private string introBGMusic;

    [SerializeField]
    private string levelBGMusic;

    private float _musicVolume;

    [SerializeField]
    private AudioSource music2Source;

    private AudioSource _activeMusic;

    private AudioSource _inactiveMusic;

    public float crossFadeRate = 1.5f;

    private bool _crossFading;

    public ManagerStatus status
    {
        get;
        private set;
    }

    public float soundVolume
    {
        get
        {
            return AudioListener.volume;
        }

        set
        {
            AudioListener.volume = value;
        }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public float musicVolume
    {
        get
        {
            return _musicVolume;
        }

        set
        {
            _musicVolume = value;

            if (musicSource != null)
            {
                musicSource.volume = _musicVolume;
                music2Source.volume = _musicVolume;
            }
        }
    }

    public bool musicMute
    {
        get { return (musicSource != null) ? musicSource.mute : false; }
        set { 
            if (musicSource != null)
            {
                musicSource.mute = value;
                music2Source.mute = value;
            }
        }
    }

    public void Startup()
    {
        Debug.Log("Audio manager starting...");

        musicSource.ignoreListenerVolume = true;
        musicSource.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;

        soundVolume = 1f;
        musicVolume = 1f;

        _activeMusic = musicSource;
        _inactiveMusic = music2Source;

        status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_crossFading)
        {
            return;
        }

        StartCoroutine(CrossFadeRate(clip));
    }

    private IEnumerator CrossFadeRate(AudioClip clip)
    {
        _crossFading = true;

        _inactiveMusic.clip = clip;
        _inactiveMusic.volume = 0;
        _inactiveMusic.Play();

        float scaledRate = crossFadeRate * _musicVolume;

        while (_activeMusic.volume > 0)
        {
            _activeMusic.volume -= scaledRate * Time.deltaTime;
            _inactiveMusic.volume += scaledRate * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = _activeMusic;

        _activeMusic = _inactiveMusic;
        _inactiveMusic = temp;

        _inactiveMusic.Stop();

        _crossFading = false;
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load<AudioClip>($"Music/{introBGMusic}"));
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load<AudioClip>($"Music/{levelBGMusic}"));
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
