
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    // 動作の列挙型を定義
    enum Motion
    {
        None = -1,          // 動作なし
        standBy,            // 準備
        waveHand,           // 手を振る
        shirahadori,        // 白刃取り
        waveHandBack,       // 手を振り返す
        coolDown,           // クールダウン
    }
    // 初期動作を準備状態に設定
    Motion _motion = Motion.standBy;

    // プレイヤー設定の列挙型を定義
    enum PlayerSetting
    {
        LeftPlayer,         // 左プレイヤー
        RightPlayer,        // 右プレイヤー
    }
    // プレイヤー設定をシリアライズ
    [SerializeField] PlayerSetting _playerSetting;

    // 当たり判定用
    [SerializeField] Transform _checkHitPosition;//　当たり判定の位置
    [SerializeField] LayerMask _layerMask;// 当たり判定に有効なレイヤ
    [SerializeField] private float _checkHitRadiue = 1f; // 当たり判定の半径

    // 判定用タイマー
    [SerializeField] private int _waveHandTimermax = 5;  // 手を振る動作の最大時間
    [SerializeField] private int _shiRaHaDoRiTimer = 50; // 白刃取り動作の最大時間
    [SerializeField] private int _waveHandBackTimer = 10;// 手を振り返す動作の最大時間
    [SerializeField] private int _coolDownTimerMax = 180;// クールダウン動作の最大時間

    // 観測用変数
    [Header("観測用---------------------------")]
    [SerializeField] private int _motionCnt = 0; // 動作カウンター

    // デバッグ用テキスト
    [SerializeField] TextMeshProUGUI _motiontTMP;

    // プレイヤー名を保持する変数
    private string _playerName;

    private void Start()
    {
        // プレイヤー設定に応じてアタックボタンを設定
        if (_playerSetting == PlayerSetting.LeftPlayer)
        {
            _playerName = "Player01Fire";
        }
        else
        {
            _playerName = "Player02Fire";
        }
        
    }

    private void FixedUpdate()
    {
        _motionCnt++; // 動作カウンターを増加
    }

    private void Update()
    {
        Think(); // 思考処理
        Move();  // 行動処理
        if (_motiontTMP != null)
        {
            _motiontTMP.text = _motion.ToString()+"\n"+_playerSetting;

        }
    }

    // 思考処理
    private void Think()
    {
        Motion nm = _motion; // 一応現在のモーションを指定
        switch (_motion)
        {
            case Motion.None:
                break;
            case Motion.standBy:
                if (Input.GetButtonDown(_playerName)) { nm = Motion.waveHand; } // ボタン入力で手を振る動作に移行
                break;
            case Motion.waveHand:
                if (_motionCnt == _waveHandTimermax) { nm = Motion.shirahadori; } // 一定時間後に白刃取り動作に移行
                break;
            case Motion.shirahadori:
                if (_motionCnt == _shiRaHaDoRiTimer) { nm = Motion.waveHandBack; } // 一定時間後に手を振り返す動作に移行
                break;
            case Motion.waveHandBack:
                if (_motionCnt == _waveHandBackTimer) { nm = Motion.coolDown; } // 一定時間後にクールダウン動作に移行
                break;
            case Motion.coolDown:
                if (_motionCnt == _coolDownTimerMax) { nm = Motion.standBy; } // 一定時間後に準備状態に戻る
                break;
            default:
                break;
        }
        UpdateMotion(nm); // 動作を更新
    }

    // 行動処理
    private void Move()
    {
        switch (_motion)
        {
            case Motion.None:
                break;
            case Motion.standBy:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // 準備状態でカウンターが0の場合、ログを出力
                break;
            case Motion.waveHand:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // 手を振る動作でカウンターが0の場合、ログを出力
                break;
            case Motion.shirahadori:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // 白刃取り動作でカウンターが0の場合、ログを出力
                if (CheckHit()) { Debug.Log("Hit"); } // 当たり判定のチェック
                break;
            case Motion.waveHandBack:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // 手を振り返す動作でカウンターが0の場合、ログを出力
                break;
            case Motion.coolDown:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // クールダウン動作でカウンターが0の場合、ログを出力
                break;
            default:
                break;
        }
    }

    // 動作の更新処理
    private void UpdateMotion(Motion nm)
    {
        if (_motion == nm) { return; } // 動作が変わらない場合は何もしない
        else
        {
            _motion = nm; // 動作を更新
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
