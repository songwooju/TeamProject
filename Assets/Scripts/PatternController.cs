using UnityEngine;
using System.Collections;

public class PatternController : MonoBehaviour
{
    public Pattern pattern1;
    public Pattern2 pattern2;
    public float patternDuration = 5f;
    public float minDelay = 0.5f;
    public float maxDelay = 2f;

    private bool isRunning = false;

    private void Start()
    {
        StartPatternController();
    }

    public void StartPatternController()
    {
        if (!isRunning)
        {
            StartCoroutine(RunPatterns());
        }
    }

    public void StopPatternController()
    {
        StopAllCoroutines();
        isRunning = false;
        pattern1.StopPattern();
        pattern2.StopPattern();
    }

    private IEnumerator RunPatterns()
    {
        isRunning = true;

        while (isRunning)
        {
            pattern1.StartPattern();
            pattern2.StartPattern();

            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            pattern1.StopPattern();
            pattern2.StopPattern();

            yield return new WaitForSeconds(patternDuration - delay);
        }
    }
}
