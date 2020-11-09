using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 1; //количество жизней
    private int currentHealth; //текущая жизнь
    public GameObject Coins_deathEffect;
    public int deathSound; //мелодия разрушения

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void  TakeDamage()
    {
        currentHealth--; //если, жизни будут уменьшаться, тогда играть мелодию и уничтожать скелет

        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound); //мелодия разрушения
            Destroy(gameObject); //уничтожение объекта
            Instantiate(Coins_deathEffect, EnemyController.enemy.transform.position + new Vector3(0f, 1f, 0f), EnemyController.enemy.transform.rotation);
            PlayerController.instance.Bounce();
        }
    }

}
