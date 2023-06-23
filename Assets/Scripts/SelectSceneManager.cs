using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void SceneChange2()
    {
        SceneManager.LoadScene("Stage1_Warrior_Socerer");
    }

    public void SceneChange3()
    {
        SceneManager.LoadScene("Stage1_Archer_Socerer");
    }
}
