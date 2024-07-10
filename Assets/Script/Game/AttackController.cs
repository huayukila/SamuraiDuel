using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float AttackDuration = 0.4f;        // 攻撃にかかる時間

    [SerializeField]
    float BackDuration = 3f;             // 攻撃キャンセルにかかる時間

    float currentAngle;
    // Start is called before the first frame update
    PlayerControllerState state;

    Transform katanaTrans;

    public AttackController(PlayerController ctlr) : base(ctlr)
    {
    }

    public enum PlayerControllerState
    {
        Idle,
        Charge,
        Attack,
        Back,
        End,
    }

    void Start()
    {
        state = PlayerControllerState.Idle;
    }

    public float EaseInOutCirc(float x)
    {
        return x < 0.5f
        ? (1 - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * x, 2.0f))) / 2.0f
        : (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f)) + 1.0f) / 2.0f;
    }

    protected override void Init()
    {
       katanaTrans=playerCtrl.KatanaTrans;
    }

    public override void Update()
    {
        switch (state)
        {
            case PlayerControllerState.Idle:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    state = PlayerControllerState.Charge;
                }
                break;
            case PlayerControllerState.Charge:
                {
                    if (Input.GetKey(KeyCode.S))
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
                    else if (Input.GetKeyUp(KeyCode.S))
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
                        state = PlayerControllerState.Idle;
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        state = PlayerControllerState.Charge;
                    }

                    currentAngle = Mathf.Lerp(StartAngle, ChargeDestinationAngle, timer / ChargeDuration);
                    katanaTrans.eulerAngles = new Vector3(0, 0, currentAngle);
                }
                break;
        }
    }

    public override void FixedUpdate()
    {
       
    }
}
