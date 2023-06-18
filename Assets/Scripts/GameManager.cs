using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int sharedMaxHealth = 10;
    public int sharedCurrentHealth;
    public int sharedMP;

    public Sprite[] playerHpSprite;
    public Slider mpBar;
    float testMp = 90;

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
        PlayerHpBarUpdate();
    }

    private void Update()
    {
        //PlayerHpBarUpdate();
        MPUpdate();
    }

    void PlayerHpBarUpdate()
    {
        int random = Random.Range(0, 4);
        Debug.Log(random);

        switch (random) // random 말고 sharedCurrentHealth 들어갈 예정, start말고 update에 추가해야함
        {
            case 3:
                GameObject.Find("HP3").GetComponent<Image>().sprite = playerHpSprite[0];
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

        Invoke("PlayerHpBarUpdate", 1f);
    }

    void MPUpdate()
    {
        if (testMp <= 100f)
        {
            testMp += Time.deltaTime;
            mpBar.value = testMp / 100f;
        }
        else
            testMp = 0.0f;
    }
}