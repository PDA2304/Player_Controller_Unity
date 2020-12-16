using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPuckup : MonoBehaviour
{
    public int value;
    public int soundToPlay;
    public GameObject coinsEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.AddCoins(value);
            Destroy(gameObject);
            Instantiate(coinsEffect, transform.position, transform.rotation);

            AudioManager.instance.PlaySFX(5);
        }
    }
}
