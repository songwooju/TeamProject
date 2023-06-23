using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Stage1AW");
    }

    public void SceneChange2()
    {
        SceneManager.LoadScene("Stage1WS");
    }

    public void SceneChange3()
    {
        SceneManager.LoadScene("Stage1AS");
    }
}
