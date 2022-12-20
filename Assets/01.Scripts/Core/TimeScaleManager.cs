using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager Instance;

    private WaitForSecondsRealtime _duration;

    public void SetTimeScale(float endValue, float duration, float timeLerpSpeed = 0.5f){
        StartCoroutine(ScaleLerp(endValue, duration, timeLerpSpeed));
    }

    private IEnumerator ScaleLerp(float endValue, float duration, float timeLerpSpeed = 0.5f){
        _duration = new WaitForSecondsRealtime(duration);
        float currentTime = 0;

        while(currentTime <= timeLerpSpeed){
            currentTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1f, endValue, currentTime / timeLerpSpeed);
            yield return null;
        }

        yield return _duration;

        currentTime = 0;
        while(currentTime <= timeLerpSpeed){
            currentTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(endValue, 1f, currentTime / timeLerpSpeed);
            yield return null;
        }
    }
}
