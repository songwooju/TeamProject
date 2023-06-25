using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndStory : MonoBehaviour
{
    public TMP_Text storyText;
    string sentence;

    // Start is called before the first frame update
    void Start()
    {
        sentence = "������� ����ϰ� ���縦 ���� �ŵ��� ȭ��  Ǯ�ȴ�!  �찡�찡 ������ ��ȭ�� ��ã�Ҵ�!";
        StartCoroutine(Typing(sentence));
    }


    IEnumerator Typing(string text)
    {
        storyText.text = null;

        if (text.Contains("  ")) text = text.Replace("  ", "\n");

        for (int i = 0; i < text.Length; i++)
        {
            storyText.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
