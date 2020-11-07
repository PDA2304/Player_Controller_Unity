using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackScreen;
    public float fadeSpeed = 1f;
    public bool fadeToBlack, fadeFromBlack;
    public Text healthText;
    public Image healthImage;
    public Text coinText;
    public GameObject pauseScreen, optionsScreen;
    public Slider musicVolSlider, sfxVolSlider;
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fadeFromBlack = true;
        pauseScreen.SetActive(false);
    }
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }

    public void Rescume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void LevelSelect()
    {

    }

    public void MainMenu()
    { 
    
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

}
