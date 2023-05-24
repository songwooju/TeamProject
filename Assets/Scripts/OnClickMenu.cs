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
    public void OnClickOptionBtn() // 옵션버튼 눌렀을 때 작동 정지
    {
        Time.timeScale = 0;
    }
    public void MenuBtnOutn() // 옵션버튼 나가면 작동
    {
        Time.timeScale = 1;
    }
}
