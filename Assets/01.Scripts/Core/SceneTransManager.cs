using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransManager : MonoSingleton<SceneTransManager>
{
    private Image panel;

    private void Start() {
        panel = GameObject.Find("Canvas/Fade").GetComponent<Image>();
        panel.DOFade(0, 0.5f).SetUpdate(true);
        DontDestroyOnLoad(gameObject);
    }

    public void SceneChange(string name){
        StartCoroutine(ChangeScene(name));
    }

    private IEnumerator ChangeScene(string name){
        panel.DOFade(1, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        
        while(!asyncOperation.isDone) yield return null;

        panel = GameObject.Find("Canvas/Fade").GetComponent<Image>();
        panel.DOFade(0, 1f).SetUpdate(true);
    }
}
