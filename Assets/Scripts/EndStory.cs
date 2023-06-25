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
        sentence = "공룡들을 토벌하고 제사를 지내 신들의 화가  풀렸다!  우가우가 부족은 평화를 되찾았다!";
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
