using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public float invincibleLength = 2f;
    private float invincCounter;

    public int currentHealth, maxHealth;
    // Start is called before the first frame update

    public Sprite[] healtBarImages;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();

        switch (currentHealth)
        {
            case 5:
                {
                    UIManager.instance.healthImage.enabled = true;
                    UIManager.instance.healthImage.sprite = healtBarImages[4];
                    break;
                }
            case 4:
                {
                    UIManager.instance.healthImage.sprite = healtBarImages[3];
                    break;
                }
            case 3:
                {
                    UIManager.instance.healthImage.enabled = true;
                    UIManager.instance.healthImage.sprite = healtBarImages[2];
                    break;
                }
            case 2:
                {
                    UIManager.instance.healthImage.sprite = healtBarImages[1];
                    break;
                }
            case 1:
                {
                    UIManager.instance.healthImage.sprite = healtBarImages[0];
                    break;
                }
            case 0:
                {
                    UIManager.instance.healthImage.enabled = false;
                    break;
                }
        }

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
            UpdateUI();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateUI();
    }

}
