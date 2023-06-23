using UnityEngine;
using UnityEngine.SceneManagement;

public class GoStage1 : MonoBehaviour
{
    public void ChangeScene1()
    {
        SceneManager.LoadScene("Stage1AS");
    }

    public void ChangeScene2()
    {
        SceneManager.LoadScene("Stage1AW");
    }

    public void ChangeScene3()
    {
        SceneManager.LoadScene("Stage1WS");
    }
}
