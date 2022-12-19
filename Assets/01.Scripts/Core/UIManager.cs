using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _playerHPTxt;
    [SerializeField] private Slider _playerDodgeSlider;

    public float DodgeSliderValue => _playerDodgeSlider.value;

    public void SetPlayerHP(float hp){
        Sequence sq = DOTween.Sequence();

        sq.Append(_playerHPTxt.rectTransform.DORotate(new Vector3(0, 0, 180), 0.3f));
        sq.OnComplete(() => {
            sq.Append(_playerHPTxt.rectTransform.DORotate(new Vector3(0, 0, 0), 0.2f));
            _playerHPTxt.text = $"{hp}";
            sq.Kill();
        });
    }

    public void DodgeSliderValueSet(float value){
        value = Mathf.Clamp(value, _playerDodgeSlider.minValue, _playerDodgeSlider.maxValue);
        _playerDodgeSlider.value = value;
    }
}
