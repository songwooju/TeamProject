using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMenu : MonoBehaviour
{   
    public void OnClickFirstSceneBtn()
    {
        SceneManager.LoadScene("FirstScene");
    }
    public void OnClickReGameBtn()
    {
        SceneManager.LoadScene("PlayerSelectScene");
    }
    public void OnClickOptionBtn() // �ɼǹ�ư ������ �� �۵� ����
    {
        Time.timeScale = 0;
    }
    public void MenuBtnOutn() // �ɼǹ�ư ������ �۵�
    {
        Time.timeScale = 1;
    }
}
