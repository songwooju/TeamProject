using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int sharedMaxHealth = 3;
    public int sharedCurrentHealth;
    public int sharedMP;

    public Sprite[] playerHpSprite;
    public Slider mpBar;
    float testMp = 0; 
    public int beforePatternHP;
    public bool isAvoidPattern = false;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sharedCurrentHealth = sharedMaxHealth;
    }

    private void Update()
    {
        PlayerHpBarUpdate();
        MPUpdate();
    }

    void PlayerHpBarUpdate()
    {
        switch (sharedCurrentHealth) 
        {
            case 3:
                GameObject.Find("HP3").GetComponent<Image>().sprite = playerHpSprite[1];
                GameObject.Find("HP2").GetComponent<Image>().sprite = playerHpSprite[1];
                GameObject.Find("HP1").GetComponent<Image>().sprite = playerHpSprite[1];
                break;

            case 2:
                GameObject.Find("HP3").GetComponent<Image>().sprite = playerHpSprite[0];
                GameObject.Find("HP2").GetComponent<Image>().sprite = playerHpSprite[1];
                GameObject.Find("HP1").GetComponent<Image>().sprite = playerHpSprite[1];
                break;

            case 1:
                GameObject.Find("HP3").GetComponent<Image>().sprite = playerHpSprite[0];
                GameObject.Find("HP2").GetComponent<Image>().sprite = playerHpSprite[0];
                GameObject.Find("HP1").GetComponent<Image>().sprite = playerHpSprite[1];
                break;

            case 0:
                GameObject.Find("HP3").GetComponent<Image>().sprite = playerHpSprite[0];
                GameObject.Find("HP2").GetComponent<Image>().sprite = playerHpSprite[0];
                GameObject.Find("HP1").GetComponent<Image>().sprite = playerHpSprite[0];
                break;
        }
    }

    void MPUpdate()
    {
        mpBar.value = testMp / 100f;
        if (isAvoidPattern)
        {
            if (beforePatternHP != sharedCurrentHealth)
            {
                isAvoidPattern = false;
                return;
            }
            testMp += 20.0f;
            Debug.Log(testMp);
            isAvoidPattern = false;
        }

        if (testMp > 100.0f) testMp = 0.0f;
    }
}