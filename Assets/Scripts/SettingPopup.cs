using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    //[SerializeField]
    //private Slider speedSlider;

    [SerializeField]
    private AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        //speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name)
    {

    }

    public void OnSpeedValue(float speed)
    {
        Messenger<float>.Boardcast(GameEvent.SPEED_CHANGED, speed);
    }

    public void OnSoundToggle()
    {
        Manager.Audio.soundMute = !Manager.Audio.soundMute;
        Manager.Audio.PlaySound(sound);
    }

    public void OnSoundValue(float volume)
    {
        Manager.Audio.soundVolume = volume;
    }

    public void OnPlayMusic(int selector)
    {
        Manager.Audio.PlaySound(sound);

        switch (selector)
        {
            case 1:
                {
                    Manager.Audio.PlayIntroMusic();
                    break;
                }
            case 2:
                {
                    Manager.Audio.PlayLevelMusic();
                    break;
                }
            default:
                {
                    Manager.Audio.StopMusic();
                    break;
                }
        }
    }

    public void OnMusicToggle()
    {
        Manager.Audio.musicMute = !Manager.Audio.musicMute;
        Manager.Audio.PlaySound(sound);
    }

    public void OnMusicValue(float volume)
    {
        Manager.Audio.musicVolume = volume;
    }
}
