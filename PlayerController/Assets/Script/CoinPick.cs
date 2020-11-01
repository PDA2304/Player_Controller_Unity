using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPick : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFullCoin  ;
    public GameObject deathEffect;
    void Start()
    {
        
    }
    public int value;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.AddCoins(value);
            Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);
            AudioManager.instance.PlaySFX(4);
        }
    }
}
