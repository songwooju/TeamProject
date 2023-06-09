using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_PatternController : MonoBehaviour
{
    public bool isPattern = false;

    public AudioSource audioSrc;
    void Start()
    {
        BossPattern();
    }

    void BossPattern()
    {
        GameManager.instance.beforePatternHP = GameManager.instance.sharedCurrentHealth;
        int randomPatter = Random.Range(0, 2);
        switch (randomPatter)
        {
            case 0:
                GameObject.Find("Square").GetComponent<Pattern>().ShootManager();
                audioSrc.Play();
                break;
            case 1:
                GameObject.Find("Capsule").GetComponent<Pattern3>().ShootManager3();
                audioSrc.Play();
                break;
        }
        Invoke("BossPattern", 3f);
    }
}
