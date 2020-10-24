using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 respawnPosition; // переменная для получения позиции 
    public static GameManager instance; //переменная для переопределения класса
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = false;//----------
        Cursor.lockState = CursorLockMode.Locked; // 2 строчки для скрытья и показа курсора 
        respawnPosition = PlayerController.instance.transform.position;// получение позиции игрока
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        Debug.Log("я респаун");
    }// вызов самого респавна
    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
    } //Респавн с задержкой
}

