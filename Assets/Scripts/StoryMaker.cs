using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryMaker : MonoBehaviour
{
    public TMP_Text storyText;

    public string[] sentence;

    public string[] storySentences;

    public int textNum;

    // Start is called before the first frame update
    void Start()
    {
        StartStory(sentence);
    }

    public void StartStory(string[] texts)
    {
        storySentences = texts;
        StartCoroutine(Typing(storySentences[textNum]));
    }

    public void NextText()
    {
        storyText.text = null;
        textNum++;

        if (textNum == storySentences.Length)
        {
            SceneManager.LoadScene("PlayerSelectScene");
            textNum = 0;
            return;
        }

        StartCoroutine(Typing(storySentences[textNum]));
    }

    IEnumerator Typing(string text)
    {
        storyText.text = null;

        if (text.Contains("  ")) text = text.Replace("  ", "\n");

        for (int i = 0; i < text.Length; i++ )
        {
            storyText.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);
        NextText();
    }
}
