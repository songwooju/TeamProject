using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
