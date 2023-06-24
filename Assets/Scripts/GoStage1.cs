using UnityEngine;
using UnityEngine.SceneManagement;

public class GoStage1 : MonoBehaviour
{
    public void ChangeScene1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage1AS");
    }

    public void ChangeScene2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage1AW");
    }

    public void ChangeScene3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage1WS");
    }

    public void ChangeScene4()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage2AS");
    }

    public void ChangeScene5()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage2AW");
    }

    public void ChangeScene6()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage2WS");
    }

    public void GotoFirstScene()
    {
        SceneManager.LoadScene("FirstScene");
    }
}
