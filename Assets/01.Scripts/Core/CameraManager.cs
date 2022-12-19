using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera _mainVCam;
    private CinemachineBasicMultiChannelPerlin _mainVCamPerlin;

    private WaitForSeconds _shakeDuration;

    private void Awake() {
        _mainVCam = GameObject.Find("Main VCam").GetComponent<CinemachineVirtualCamera>();
        _mainVCamPerlin = _mainVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start() {
        _mainVCamPerlin.m_AmplitudeGain = 0; //Camera Reset
    }

    ///<summary>
    /// duration 동안 intensity의 세기로 카메라를 흔듬
    ///</summary>
    public void CameraShake(float intensity, float duration){
        _shakeDuration = new WaitForSeconds(duration);
        StartCoroutine(ShakeCoroutine(intensity));
    }

    private IEnumerator ShakeCoroutine(float intensity){
        _mainVCamPerlin.m_AmplitudeGain = intensity;
        yield return _shakeDuration;
        _mainVCamPerlin.m_AmplitudeGain = 0;
    }
}
