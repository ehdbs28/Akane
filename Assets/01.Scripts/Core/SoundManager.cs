using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer _masterMixer;

    private AudioSource _bgmSource;

    private void Start() {
        _bgmSource = Camera.main.GetComponent<AudioSource>();
        if(GameManager.Instance.CurrentScene == 1) PlayBGM(GameManager.Instance.BossPhase1BGM);
    }

    public void StopBGM(){
        _bgmSource.Stop();
    }

    public void BGMPause(bool pause){
        if(pause) _bgmSource.Pause();
        else _bgmSource.UnPause();
    }

    public void PlayBGM(AudioClip clip){
        _bgmSource.clip = clip;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    public void PlayOneShot(AudioSource source, AudioClip clip) => source.PlayOneShot(clip);

    public void PlayOneShot(AudioSource source, string clipName){
        AudioClip clip = Resources.Load<AudioClip>($"AudioClip/SFX/{clipName}");

        if(clip != null){
            source.PlayOneShot(clip);
        }
        else{
            Debug.LogError($"Has not exist {clipName} audioClip in Resource folder");
        }
    }
}
