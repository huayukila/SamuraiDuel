using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackPlayerLeftRightSetting
{
    LeftPlayer,         // ���v���C���[
    RightPlayer,        // �E�v���C���[
}
public class AttackController : PlayerAction
{
    float timer;

    [SerializeField]
    private float ChargeDuration = 2f;        // �`���[�W�ɂ����鎞��

    [SerializeField]
    float ChargeDestinationAngle = -80; // �`���[�W���̃S�[���ƂȂ�p�x

    [SerializeField]
    float StartAngle = -90;             // �ʏ펞�̊p�x

    [SerializeField]
    float DestinationAngle = -10;       // �U���I�����̊p�x

    [SerializeField]
    float AttackDuration = 0.4f;        // �U���ɂ����鎞��

    //[SerializeField]
    //float BackDuration = 3;             // �U���L�����Z���ɂ����鎞��

    float currentAngle;

    private string playerName;

    KeyCode attackKeyCode;

    // ���O�\���p�̃t���O
    private bool logDisplayed = false;

    // �E�v���C�����v���C����
    public DefenderPlayerLeftRightSetting _dfLRSetting;// �v���C���[�ݒ���V���A���C�Y
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
        katanaTrans =playerCtrl.KatanaTrans;
        // �v���C���[�ݒ���V���A���C�Y
        _dfLRSetting = playerCtrl._dfSetting._dfLRSetting;// �v���C���[�ݒ���V���A���C�Y
        if (_dfLRSetting == DefenderPlayerLeftRightSetting.LeftPlayer)// �v���C���[�ݒ�ɉ����ăA�^�b�N�{�^����ݒ�
        {
            StartAngle=-StartAngle;
            ChargeDestinationAngle = -ChargeDestinationAngle;
            playerName = "Player01Fire";
            attackKeyCode = KeyCode.X;
        }
        else
        {
            DestinationAngle=-DestinationAngle;
            playerName = "Player02Fire";
            attackKeyCode=KeyCode.M;
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
                    if (Input.GetButton(playerName)||Input.GetKey(attackKeyCode))
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
                    else if (Input.GetButtonUp(playerName))
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
                    // currentAngle��DestinationAngle�Ɠ����ɂȂ����Ƃ��Ƀ��O����x�����\��
                    if (!logDisplayed && Mathf.Approximately(currentAngle, DestinationAngle))
                    {
                        Debug.Log("End");
                        logDisplayed = true; // ���O��\�������̂Ńt���O���Z�b�g
                    }
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
                    if (Input.GetButtonDown(playerName) || Input.GetKeyDown(attackKeyCode))
                    {
                        state = PlayerControllerState.Charge;
                            currentAngle = Mathf.Lerp(StartAngle, ChargeDestinationAngle, timer / ChargeDuration);
                            katanaTrans.eulerAngles = new Vector3(0, 0, currentAngle);
                    }
                }
                break;
        }
    }

    public override void FixedUpdate()
    {
       
    }

 }
