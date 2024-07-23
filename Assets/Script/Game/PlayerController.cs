using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform KatanaTrans;
    private PlayerAction _currentAction;
    private PlayerAction[] _actions;

    private int actionIndex = 0;

    [HideInInspector]
    public DefenderControllerSeting _dfSetting;

    [SerializeField] private Transform _checkHitPosition;
    [SerializeField] private float _checkHitRadiue = 1f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private DefenderPlayerLeftRightSetting _dfLRSetting;
    [SerializeField] private Animator _defenceAnimation;

    //観察用---------
    [SerializeField] int _moveCnt;

    // デバッグ用テキスト
    [SerializeField] TextMeshProUGUI _motiontTMP;

    //キャラのスイッチ用
    [Header("キャラのスイッチ用")]
    [SerializeField] private GameObject _hand_Attack;
    [SerializeField] private GameObject _hand_Defence;
    private KeyCode _LeftSwitchKey;
    private KeyCode _RightSwitchKey;


    bool canInput = false;
    private void Start()
    {
        _actions = new PlayerAction[2];
        // 当たり判定用
        _dfSetting._checkHitPosition = _checkHitPosition;//　当たり判定の位置
        _dfSetting._checkHitRadiue = _checkHitRadiue;// 当たり判定の半径
        _dfSetting._layerMask = _layerMask;// 当たり判定に有効なレイヤ

        // 右プレイか左プレイヤか
        _dfSetting._dfLRSetting = _dfLRSetting;// プレイヤー設定をシリアライズ

        // 判定用タイマー
        _dfSetting._waveHandTimermax = 5;// 手を振る動作の最大時間
        _dfSetting._shiRaHaDoRiTimer = 50;// 白刃取り動作の最大時間
        _dfSetting._waveHandBackTimer = 10;// 手を振り返す動作の最大時間
        _dfSetting._coolDownTimerMax = 180;// クールダウン動作の最大時間

        _dfSetting._defenceAnimation = _defenceAnimation;

        _actions[0] = new DefenderController(this);
        _actions[1] = new AttackController(this);


        SwitchKeyIni();
        SetPlayerAttackMode();

       
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInput)
            return;
        _moveCnt = _currentAction.moveCnt;//観測用

        _currentAction.Update();
        if (_motiontTMP != null)
        {
            var _cu = _currentAction as DefenderController;
            //_motiontTMP.text = _cu.Motion + "\n" + _dfLRSetting;
        }

        if (Input.GetKeyDown(_LeftSwitchKey) || Input.GetKeyDown(_RightSwitchKey))
        {
            SwitchAction();
        }

    }

    private void FixedUpdate()
    {
        if (!canInput) return;
        _currentAction.FixedUpdate();
    }

    public void SwitchAction()
    {
        canInput=true;
        actionIndex++;
        actionIndex %= 2;
        _currentAction = _actions[actionIndex];
        SwitchHandler();
    }
    private void SwitchKeyIni()
    {
        if (_dfLRSetting == DefenderPlayerLeftRightSetting.LeftPlayer)
        {
            _LeftSwitchKey = KeyCode.A;
            _RightSwitchKey = KeyCode.D;
        }
        else
        {
            _LeftSwitchKey = KeyCode.LeftArrow;
            _RightSwitchKey = KeyCode.RightArrow;
        }
    }
    private void SwitchHandler()
    {
        if (_currentAction == _actions[0])
        {
            _hand_Attack.SetActive(false);
            _hand_Defence.SetActive(true);
        }
        else
        {
            _hand_Attack.SetActive(true);
            _hand_Defence.SetActive(false);
        }
    }
    public void SetPlayerAttackMode()
    {
        canInput = true;
        actionIndex = 1;
        _currentAction = _actions[actionIndex];
        SwitchHandler();
    }
    public void SetPlayerDefenceMode()
    {
        canInput=true;
        actionIndex = 0;
        _currentAction = _actions[actionIndex];
        SwitchHandler();
    }

    public void HitHands()
    {
        canInput=false;
        KatanaTrans.rotation =Quaternion.AngleAxis(-2,Vector3.forward);
    }
    // ギズモを描画
    private void OnDrawGizmos()
    {
        if (_checkHitPosition != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_checkHitPosition.position, _checkHitRadiue); // ギズモとして当たり判定の範囲を描画
        }
    }

   
}