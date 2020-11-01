using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 respawnPosition; // переменная для получения позиции 
    public static GameManager instance; //переменная для переопределения класса
                                        // Start is called before the first frame update

    public GameObject deathEffect;
    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = false;//----------
        Cursor.lockState = CursorLockMode.Locked; // 2 строчки для скрытья и показа курсора 
        respawnPosition = PlayerController.instance.transform.position;// получение позиции игрока
        AddCoins(0);
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        newSpawnPoint.y += 2f;
        respawnPosition = newSpawnPoint;
    }

    public void Respawn()
    {
        HealthManager.instance.PlayerKilled();
        StartCoroutine(RespawnCo());
        Debug.Log("я респаун");

    }// вызов самого респавна
    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.theCMBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);

    } //Респавн с задержкой

    public void PauseUnpause()
    {
        if (UIManager.instance.optionsScreen.activeInHierarchy)
        {
            UIManager.instance.optionsScreen.SetActive(false);
        }
        else if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            UIManager.instance.blackScreen.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;
            UIManager.instance.blackScreen.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


}

