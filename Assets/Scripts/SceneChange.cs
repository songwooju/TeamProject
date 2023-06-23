using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float delay = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Stage2AW");
    }
}