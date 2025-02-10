using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickMenu : MonoBehaviour
{
    public AudioSource soundSource;
    public Sprite[] soundSprite;
    bool isSoundOn;

    private void Start()
    {
        GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Sound").GetComponent<Image>().sprite = soundSprite[1];
        isSoundOn = true;
    }
    public void OnClickToSelectSceneBtn()
    {
        SceneManager.LoadScene("PlayerSelectScene");
    }
    public void OnClickReStartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickOptionBtn() // 옵션버튼 눌렀을 때 작동 정지
    {
        Time.timeScale = 0;
    }
    public void MenuBtnOutn() // 옵션버튼 나가면 작동
    {
        Time.timeScale = 1;
    }
    public void SetSoundVolume()
    {
        if (isSoundOn) // 소리가 켜져있다면 끄도록
        {
            soundSource.volume = 0.0f;
            GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Sound").GetComponent<Image>().sprite = soundSprite[0];
            isSoundOn = false;
        }
        else if (!isSoundOn) // 소리가 꺼져있다면 켜지도록
        {
            soundSource.volume = 1.0f;
            GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Sound").GetComponent<Image>().sprite = soundSprite[1];
            isSoundOn = true;
        }
    }
}
