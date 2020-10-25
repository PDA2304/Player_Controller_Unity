﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public float invincibleLength = 2f;
    private float invincCounter;

    public int currentHealth, maxHealth;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {

            invincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPrieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPrieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPrieces[i].SetActive(false);
                }


                if (invincCounter <= 0)
                {
                    PlayerController.instance.playerPrieces[i].SetActive(true);
                }
            }

        }
    }
    public void Hurt()
    {
        if (invincCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.Knockback();
                invincCounter = invincibleLength;
            }
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}