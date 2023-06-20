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
        SceneManager.LoadScene("Stage1");
    }
    public void OnClickOptionBtn() // �ɼǹ�ư ������ �� �۵� ����
    {
        Time.timeScale = 0;
    }
    public void MenuBtnOutn() // �ɼǹ�ư ������ �۵�
    {
        Time.timeScale = 1;
    }
    public void SetSoundVolume()
    {
        if (isSoundOn) // �Ҹ��� �����ִٸ� ������
        {
            soundSource.volume = 0.0f;
            GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Sound").GetComponent<Image>().sprite = soundSprite[0];
            isSoundOn = false;
        }
        else if (!isSoundOn) // �Ҹ��� �����ִٸ� ��������
        {
            soundSource.volume = 1.0f;
            GameObject.Find("Canvas").transform.Find("Menu").transform.Find("Sound").GetComponent<Image>().sprite = soundSprite[1];
            isSoundOn = true;
        }
    }
}
