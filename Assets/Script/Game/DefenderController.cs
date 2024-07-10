
using TMPro;
using UnityEngine;

// 動作の列挙型を定義
public enum DefenderMotion
{
    None = -1,          // 動作なし
    standBy,            // 準備
    waveHand,           // 手を振る
    shirahadori,        // 白刃取り
    waveHandBack,       // 手を振り返す
    coolDown,           // クールダウン
}

// プレイヤー設定の列挙型を定義
public enum DefenderPlayerLeftRightSetting
{
    LeftPlayer,         // 左プレイヤー
    RightPlayer,        // 右プレイヤー
}
public struct DefenderControllerSeting
{
    // 当たり判定用
    public Transform _checkHitPosition;//　当たり判定の位置
    public float _checkHitRadiue;// 当たり判定の半径
    public LayerMask _layerMask;// 当たり判定に有効なレイヤ

    // 右プレイか左プレイヤか
    public DefenderPlayerLeftRightSetting _dfLRSetting;// プレイヤー設定をシリアライズ

    // 判定用タイマー
    public int _waveHandTimermax;// 手を振る動作の最大時間
    public int _shiRaHaDoRiTimer;// 白刃取り動作の最大時間
    public int _waveHandBackTimer;// 手を振り返す動作の最大時間
    public int _coolDownTimerMax;// クールダウン動作の最大時間

    public Animator _defenceAnimation;
}
public class DefenderController : PlayerAction
{
    // 初期動作を準備状態に設定
    private DefenderMotion _motion;
    public DefenderMotion Motion { get => _motion; set => _motion = value; }

    // プレイヤー設定をシリアライズ
    private DefenderPlayerLeftRightSetting _dfLRSetting;

    // 当たり判定用
    private Transform _checkHitPosition;//　当たり判定の位置
    private LayerMask _layerMask;// 当たり判定に有効なレイヤ
    private float _checkHitRadiue; // 当たり判定の半径(1)

    // 判定用タイマー
    private int _waveHandTimermax;  // 手を振る動作の最大時間(5)
    private int _shiRaHaDoRiTimer; // 白刃取り動作の最大時間(50)
    private int _waveHandBackTimer;// 手を振り返す動作の最大時間(10)
    private int _coolDownTimerMax;// クールダウン動作の最大時間(180)

    // 観測用変数
    //[Header("観測用---------------------------")]
    private int _motionCnt; // 動作カウンター

    // プレイヤー名を保持する変数
    private string _playerName;

    private Animator _defenceAnimation;
    

    public DefenderController(DefenderControllerSeting setting)
    {
        // 当たり判定用
        _checkHitPosition = setting._checkHitPosition;//　当たり判定の位置
        _checkHitRadiue = setting._checkHitRadiue;// 当たり判定の半径
        _layerMask = setting._layerMask;// 当たり判定に有効なレイヤ

        // 初期動作を準備状態に設定
        Motion = DefenderMotion.standBy;// 初期動作を準備状態に設定

        // プレイヤー設定をシリアライズ
        _dfLRSetting = setting._dfLRSetting;// プレイヤー設定をシリアライズ

        // 判定用タイマー
        _waveHandTimermax = setting._waveHandTimermax;// 手を振る動作の最大時間
        _shiRaHaDoRiTimer = setting._shiRaHaDoRiTimer;// 白刃取り動作の最大時間
        _waveHandBackTimer = setting._waveHandBackTimer;// 手を振り返す動作の最大時間
        _coolDownTimerMax = setting._coolDownTimerMax;// クールダウン動作の最大時間

        _defenceAnimation = setting._defenceAnimation;

        _motionCnt = 0;// 動作カウンター

        if (_dfLRSetting == DefenderPlayerLeftRightSetting.LeftPlayer)// プレイヤー設定に応じてアタックボタンを設定
        {
            _playerName = "Player01Fire";
        }
        else
        {
            _playerName = "Player02Fire";
        }
    }
    public override void FixedUpdate()
    {
        _motionCnt++; // 動作カウンターを増加
    }

    public override void Update()
    {
        Think(); // 思考処理
        Move();  // 行動処理
        
    }

    // 思考処理
    private void Think()
    {
        DefenderMotion nm = Motion; // 一応現在のモーションを指定
        switch (Motion)
        {
            case DefenderMotion.None:
                break;
            case DefenderMotion.standBy:
                if (Input.GetButtonDown(_playerName)) { nm = DefenderMotion.waveHand; } // ボタン入力で手を振る動作に移行
                break;
            case DefenderMotion.waveHand:
                if (_motionCnt == _waveHandTimermax) { nm = DefenderMotion.shirahadori; } // 一定時間後に白刃取り動作に移行
                break;
            case DefenderMotion.shirahadori:
                if (_motionCnt == _shiRaHaDoRiTimer&& !CheckHit()) { nm = DefenderMotion.waveHandBack; } // 一定時間後に手を振り返す動作に移行
                break;
            case DefenderMotion.waveHandBack:
                if (_motionCnt == _waveHandBackTimer) { nm = DefenderMotion.coolDown; } // 一定時間後にクールダウン動作に移行
                break;
            case DefenderMotion.coolDown:
                if (_motionCnt == _coolDownTimerMax) { nm = DefenderMotion.standBy; } // 一定時間後に準備状態に戻る
                break;
            default:
                break;
        }
        UpdateMotion(nm); // 動作を更新
    }

    // 行動処理
    private void Move()
    {
        switch (Motion)
        {
            case DefenderMotion.None:
                break;
            case DefenderMotion.standBy:
                if (_motionCnt == 0) 
                { 
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence00");   
                } // 準備状態でカウンターが0の場合、ログを出力
                break;
            case DefenderMotion.waveHand:
                if (_motionCnt == 0)
                { 
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence01");
                } // 手を振る動作でカウンターが0の場合、ログを出力
                break;
            case DefenderMotion.shirahadori:
                if (_motionCnt == 0)
                { 
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence02");
                } // 白刃取り動作でカウンターが0の場合、ログを出力
                if (CheckHit()) 
                { 
                    Debug.Log("Hit");
                    _defenceAnimation.SetTrigger("Defence03");
                } // 当たり判定のチェック
                break;
            case DefenderMotion.waveHandBack:
                if (_motionCnt == 0) 
                {
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence01");
                } // 手を振り返す動作でカウンターが0の場合、ログを出力
                break;
            case DefenderMotion.coolDown:
                if (_motionCnt == 0) { Debug.Log(Motion.ToString() + _playerName); } // クールダウン動作でカウンターが0の場合、ログを出力
                break;
            default:
                break;
        }
    }

    // 動作の更新処理
    private void UpdateMotion(DefenderMotion nm)
    {
        if (Motion == nm) { return; } // 動作が変わらない場合は何もしない
        else
        {
            Motion = nm; // 動作を更新
            _motionCnt = 0; // 動作カウンターをリセット
        }
    }

    // 当たり判定のチェック
    private bool CheckHit()
    {
        if (_checkHitPosition != null)
        {
            Collider2D _c2D = Physics2D.OverlapCircle(
            _checkHitPosition.position,
            _checkHitRadiue,
            _layerMask);
            return _c2D; // 当たり判定の結果を返す
        }
        return false;
    }

    
}
