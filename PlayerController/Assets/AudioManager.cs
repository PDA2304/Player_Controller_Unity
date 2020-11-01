using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioManager instance;

    public AudioSource[] misic;
    public AudioSource[] sfx;
    public int levelMusicToPlay;
    private int currentTrack;
    public AudioMixerGroup musicMixer, sfxMixer;

    public void Awake()
    {
        instance = this;
    }


    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentTrack++;
            if (currentTrack == 10) currentTrack = 0;
            PlayMusic(currentTrack);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            for (int i = 0; i < misic.Length; i++)
            {
                misic[i].Stop();
            }
        }
    }
    public void PlayMusic(int musicToPlay)
    {

        for (int i = 0; i < misic.Length; i++)
        {
            misic[i].Stop();
        }
        misic[musicToPlay].Play();
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSlider.value);
    }

    public void PlaySFX(int sfxToPlay)
    {

        //инача   
        sfx[sfxToPlay].Play();
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.instance.sfxVolSlider.value);
    }

}
