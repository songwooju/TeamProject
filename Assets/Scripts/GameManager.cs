using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int sharedMaxHealth = 10;
    public int sharedCurrentHealth;
    public int sharedMP;

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
}