using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Canvas _canvas;

    [SerializeField] private TextMeshProUGUI _playerHPTxt;
    [SerializeField] private Slider _playerDodgeSlider;

    [SerializeField] private Slider _bossHPSlider_BackGround;
    [SerializeField] private Slider _bossHPSlider;

    [SerializeField] private Image _bossCutScenePanel;
    private RectTransform _bossImg;
    private RectTransform _bossNameBG;
    private RectTransform _bossNameTxt;

    private RectTransform _playerImg;
    private RectTransform _playerNameBG;
    private RectTransform _playerNameTxt;

    private GameObject _gameOverPanel;
    private RectTransform _gameOverPlayer;

    private GameObject _gameClearPanel;
    private RectTransform _gameClearBoss;

    public float DodgeSliderValue => _playerDodgeSlider.value;

    private Sequence _sequence;

    private void Awake() {
        _bossImg = _bossCutScenePanel.transform.Find("Boss").Find("BossImg").GetComponent<RectTransform>();
        _bossNameBG = _bossImg.parent.Find("BossNameBackGround").GetComponent<RectTransform>();
        _bossNameTxt = _bossImg.parent.Find("BossName").GetComponent<RectTransform>();

        _playerImg = _bossCutScenePanel.transform.Find("Player").Find("PlayerImg").GetComponent<RectTransform>();
        _playerNameBG = _playerImg.parent.Find("PlayerNameBackGround").GetComponent<RectTransform>();

        _gameOverPanel = _canvas.transform.Find("GameOverPanel").gameObject;
        _gameOverPlayer =_gameOverPanel.transform.Find("PlayerDieSprite").GetComponent<RectTransform>();

        _gameClearPanel = _canvas.transform.Find("GameClearPanel").gameObject;
        _gameClearBoss = _gameClearPanel.transform.Find("BossDieSprite").GetComponent<RectTransform>(); 

        BossCutSceneUP();
    }

    public void GameOver(Vector3 playerPos){
        _gameOverPlayer.anchoredPosition = CalcAnchoredPosition(playerPos);
        _gameOverPanel.SetActive(true);
    }

    public void GameClear(Vector3 bossPos){
        _gameClearBoss.anchoredPosition = CalcAnchoredPosition(bossPos);
        _gameClearPanel.SetActive(true);
    }

    private Vector3 CalcAnchoredPosition(Vector3 playerPos){
        Vector2 viewPortPosition = Camera.main.WorldToViewportPoint(playerPos);
        RectTransform canvasRect = _canvas.GetComponent<RectTransform>();
        Vector2 worldObj_ScreenPos = new Vector2((viewPortPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
        (viewPortPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        return worldObj_ScreenPos;
    }

    public void GameClear(){

    }

    public void SetPlayerHP(float hp){
        _sequence = DOTween.Sequence();

        _sequence.Append(_playerHPTxt.rectTransform.DORotate(new Vector3(0, 0, 180), 0.3f));
        _sequence.OnComplete(() => {
            _sequence.Append(_playerHPTxt.rectTransform.DORotate(new Vector3(0, 0, 0), 0.2f));
            _playerHPTxt.text = $"{hp}";
            _sequence.Kill();
        });
    }

    public void SetBossHP(float value){
        StartCoroutine(BossHPDownEffect(value));
    }

    private IEnumerator BossHPDownEffect(float value){
        float currentTime = 0;
        float currentValue = _bossHPSlider.value;

        while(currentTime <= 0.5f){
            currentTime += Time.deltaTime * 5;
            _bossHPSlider.value = Mathf.Lerp(currentValue, value, currentTime / 0.5f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        currentTime = 0;
        while(currentTime <= 1f){
            currentTime += Time.deltaTime * 10;
            _bossHPSlider_BackGround.value = Mathf.Lerp(currentValue, value, currentTime / 0.5f);
            yield return null;
        }
    }

    public void DodgeSliderValueSet(float value){
        value = Mathf.Clamp(value, _playerDodgeSlider.minValue, _playerDodgeSlider.maxValue);
        _playerDodgeSlider.value = value;
    }

    private IEnumerator DodgeSliderDownEffect(float value){
        float currentTime = 0;
        float currentValue = _bossHPSlider.value;

        while(currentTime <= 0.5f){
            currentTime += Time.deltaTime;
            _bossHPSlider.value = Mathf.Lerp(currentValue, value, currentTime / 0.5f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        currentTime = 0;
        while(currentTime <= 0.5f){
            currentTime += Time.deltaTime;
            _bossHPSlider_BackGround.value = Mathf.Lerp(currentValue, value, currentTime / 0.5f);
            yield return null;
        }
    }

    ///<summary>
    /// BossCutScene 실행하는 메서드
    ///</summary>
    public void BossCutSceneUP(){
        GameManager.Instance.IsGameStop = true;

        _sequence = DOTween.Sequence().SetUpdate(true);

        _sequence.Append(_bossCutScenePanel.DOFade(0.7f, 0.5f));

        _sequence.Append(_bossNameBG.DOAnchorPos(new Vector2(-368, -63), 0.5f));
        _sequence.Append(_playerNameBG.DOAnchorPos(new Vector2(291, 227), 0.5f));
        
        _sequence.Append(_bossNameTxt.DOAnchorPos(new Vector2(-180, 20), 0.5f));
        
        _sequence.Append(_bossImg.DOAnchorPos(new Vector2(-555, 424), 0.2f));
        _sequence.Append(_playerImg.DOAnchorPos(new Vector2(350, -258), 0.2f));

        _sequence.AppendInterval(2f);

        _sequence.OnComplete(() => BossCutSceneDown());
    }

    private void BossCutSceneDown(){
        _sequence = DOTween.Sequence().SetUpdate(true);

        _sequence.Append(_bossImg.DOAnchorPos(new Vector2(-468, -409), 0.2f));
        _sequence.Append(_playerImg.DOAnchorPos(new Vector2(380, 241), 0.2f));

        _sequence.Append(_bossNameTxt.DOAnchorPos(new Vector2(280, 20), 0.5f));
        
        _sequence.Append(_bossNameBG.DOAnchorPos(new Vector2(-1393, -543), 0.5f));
        _sequence.Append(_playerNameBG.DOAnchorPos(new Vector2(976, 548), 0.5f));
        
        _sequence.Append(_bossCutScenePanel.DOFade(0f, 0.5f));

        _sequence.OnComplete(() => {
            GameManager.Instance.IsGameStop = false;
        });
    }

    private void OnDestroy() {
        _sequence.Kill();
    }
}
