using UnityEngine;
using System.Collections;

public class PatternController : MonoBehaviour
{
    void Start()
    {
        BossPattern();
    }

    void BossPattern()
    {
        int randomPatter = Random.Range(0, 2);
        switch (randomPatter)
        {
            case 0:
                GameObject.Find("Circle").GetComponent<Pattern2>().ShootManager2();
                Debug.Log("0");
                break;
            case 1:
                GameObject.Find("Square").GetComponent<Pattern>().ShootManager();
                Debug.Log("1");
                break;
        }

        Invoke("BossPattern", 3f);
    }
}