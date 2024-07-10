using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAction _currentAction;
    private PlayerAction[] _actions;

    private int actionIndex = 0;
    DefenderControllerSeting _dfSetting;

    [SerializeField] private Transform _checkHitPosition;
    [SerializeField] private float _checkHitRadiue = 1f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private DefenderPlayerLeftRightSetting _dfLRSetting;
    [SerializeField] private Animator _defenceAnimation;

    // デバッグ用テキスト
    [SerializeField] TextMeshProUGUI _motiontTMP;

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
        _currentAction = _actions[0];
    }

    // Update is called once per frame
    void Update()
    {
        _currentAction.Update();
        if (_motiontTMP != null)
        {
            var _cu = _currentAction as DefenderController;
            _motiontTMP.text = _cu.Motion + "\n" + _dfLRSetting;
        }
    }

    private void FixedUpdate()
    {
        _currentAction.FixedUpdate();
    }

    public void SwitchAction()
    {
        actionIndex++;
        actionIndex %= 2;
        _currentAction = _actions[actionIndex];
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