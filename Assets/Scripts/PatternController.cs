using UnityEngine;
using System.Collections;

public class PatternController : MonoBehaviour
{
    public bool isPattern = false;
    void Start()
    {
        BossPattern();
    }

    void BossPattern()
    {
        GameManager.instance.beforePatternHP = GameManager.instance.sharedCurrentHealth;
        int randomPatter = Random.Range(0, 3);
        switch (randomPatter)
        {
            case 0:
                GameObject.Find("Circle").GetComponent<Pattern2>().ShootManager2();
                break;
            case 1:
                GameObject.Find("Square").GetComponent<Pattern>().ShootManager();
                break;
            case 2:
                GameObject.Find("Capsule").GetComponent<Pattern3>().ShootManager3();
                break;
        }
        Invoke("BossPattern", 3f);
    }
}
