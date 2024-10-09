using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackPlayerLeftRightSetting
{
    LeftPlayer,         // 左プレイヤー
    RightPlayer,        // 右プレイヤー
}
public class AttackController : PlayerAction
{
    float timer;

    [SerializeField]
    private float ChargeDuration = 2f;        // チャージにかかる時間

    [SerializeField]
    float ChargeDestinationAngle = -80; // チャージ時のゴールとなる角度

    [SerializeField]
    float StartAngle = -90;             // 通常時の角度

    [SerializeField]
    float DestinationAngle = -10;       // 攻撃終了時の角度

    [SerializeField]
    float AttackDuration = 0.28f;        // 攻撃にかかる時間

    //[SerializeField]
    //float BackDuration = 3;             // 攻撃キャンセルにかかる時間

    float currentAngle;

    private string playerName;

    KeyCode attackKeyCode;

    Vector3 effectPoint=new Vector3(-2.86f,-0.7f,0);


    // 右プレイか左プレイヤか
    public DefenderPlayerLeftRightSetting _dfLRSetting;// プレイヤー設定をシリアライズ
    // Start is called before the first frame update
    PlayerControllerState state;

    Transform katanaTrans;

    public AttackController(PlayerController ctlr) : base(ctlr)
    {
    }
    //

    public enum PlayerControllerState
    {
        Idle,
        Charge,
        Attack,
        Back,
        End,
    }


    protected override void Init()
    {
        state = PlayerControllerState.Idle;
        katanaTrans = playerCtrl.KatanaTrans;
        // プレイヤー設定をシリアライズ
        _dfLRSetting = playerCtrl._dfSetting._dfLRSetting;// プレイヤー設定をシリアライズ
        if (_dfLRSetting == DefenderPlayerLeftRightSetting.LeftPlayer)// プレイヤー設定に応じてアタックボタンを設定
        {
            StartAngle = -StartAngle;
            ChargeDestinationAngle = -ChargeDestinationAngle;
            playerName = "Player01Fire";
            attackKeyCode = KeyCode.X;
            effectPoint =  new Vector3(-effectPoint.x,effectPoint.y,0);
        }
        else
        {
            DestinationAngle = -DestinationAngle;
            playerName = "Player02Fire";
            attackKeyCode = KeyCode.M;
        }
        katanaTrans.rotation = Quaternion.AngleAxis(StartAngle, Vector3.forward);
    }

    public override void Update()
    {
        switch (state)
        {
            case PlayerControllerState.Idle:
                if (Input.GetButtonDown(playerName) || Input.GetKeyDown(attackKeyCode))
                {
                    state = PlayerControllerState.Charge;
                }
                break;
            case PlayerControllerState.Charge:
                {
                    if (Input.GetButton(playerName) || Input.GetKey(attackKeyCode))
                    {
                        timer += Time.deltaTime;
                        if (timer >= ChargeDuration)
                        {
                            timer = ChargeDuration;
                            state = PlayerControllerState.Attack;
                        }
                        currentAngle = Mathf.Lerp(StartAngle, ChargeDestinationAngle, timer / ChargeDuration);
                        katanaTrans.eulerAngles = new Vector3(0, 0, currentAngle);
                    }
                    if (Input.GetButtonUp(playerName) || Input.GetKeyUp(attackKeyCode))
                    {
                        state = PlayerControllerState.Back;
                    }
                }
                break;
            case PlayerControllerState.Attack:
                {

                    timer += Time.deltaTime;
                    if (timer - ChargeDuration >= AttackDuration)
                    {
                        timer = ChargeDuration + AttackDuration;
                        state = PlayerControllerState.End;
                        EventSystem.Send<AttackSuccesed>();
                        GameObject.Instantiate(playerCtrl.hitEffectPrefab,effectPoint,Quaternion.identity);

                    }
                    currentAngle = Mathf.Lerp(ChargeDestinationAngle, DestinationAngle, (timer - ChargeDuration) / AttackDuration);
                    katanaTrans.eulerAngles = new Vector3(0, 0, currentAngle);
                }
                break;
            case PlayerControllerState.Back:
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        timer = 0;
                    }
                    if (Input.GetButtonDown(playerName) || Input.GetKeyDown(attackKeyCode))
                    {
                        state = PlayerControllerState.Charge;

                    }
                    currentAngle = Mathf.Lerp(StartAngle, ChargeDestinationAngle, timer / ChargeDuration);
                    katanaTrans.eulerAngles = new Vector3(0, 0, currentAngle);
                }
                break;
            case PlayerControllerState.End:
                {

                }

                break;
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Reset()
    {
        state = PlayerControllerState.Idle;
        katanaTrans.rotation = Quaternion.AngleAxis(StartAngle, Vector3.forward);
        currentAngle = 0;
        timer = 0;
    }
}
