
using TMPro;
using UnityEngine;

// ����̗񋓌^���`
public enum DefenderMotion
{
    None = -1,          // ����Ȃ�
    standBy,            // ����
    waveHand,           // ���U��
    shirahadori,        // ���n���
    waveHandBack,       // ���U��Ԃ�
    coolDown,           // �N�[���_�E��
}

// �v���C���[�ݒ�̗񋓌^���`
public enum DefenderPlayerLeftRightSetting
{
    LeftPlayer,         // ���v���C���[
    RightPlayer,        // �E�v���C���[
}
public struct DefenderControllerSeting
{
    // �����蔻��p
    public Transform _checkHitPosition;//�@�����蔻��̈ʒu
    public float _checkHitRadiue;// �����蔻��̔��a
    public LayerMask _layerMask;// �����蔻��ɗL���ȃ��C��

    // �E�v���C�����v���C����
    public DefenderPlayerLeftRightSetting _dfLRSetting;// �v���C���[�ݒ���V���A���C�Y

    // ����p�^�C�}�[
    public int _waveHandTimermax;// ���U�铮��̍ő厞��
    public int _shiRaHaDoRiTimer;// ���n��蓮��̍ő厞��
    public int _waveHandBackTimer;// ���U��Ԃ�����̍ő厞��
    public int _coolDownTimerMax;// �N�[���_�E������̍ő厞��

    public Animator _defenceAnimation;
}
public class DefenderController : PlayerAction
{
    // ���������������Ԃɐݒ�
    private DefenderMotion _motion;
    public DefenderMotion Motion { get => _motion; set => _motion = value; }

    // �v���C���[�ݒ���V���A���C�Y
    private DefenderPlayerLeftRightSetting _dfLRSetting;

    // �����蔻��p
    private Transform _checkHitPosition;//�@�����蔻��̈ʒu
    private LayerMask _layerMask;// �����蔻��ɗL���ȃ��C��
    private float _checkHitRadiue; // �����蔻��̔��a(1)

    // ����p�^�C�}�[
    private int _waveHandTimermax;  // ���U�铮��̍ő厞��(5)
    private int _shiRaHaDoRiTimer; // ���n��蓮��̍ő厞��(50)
    private int _waveHandBackTimer;// ���U��Ԃ�����̍ő厞��(10)
    private int _coolDownTimerMax;// �N�[���_�E������̍ő厞��(180)

    // �ϑ��p�ϐ�
    //[Header("�ϑ��p---------------------------")]
    private int _motionCnt; // ����J�E���^�[

    // �v���C���[����ێ�����ϐ�
    private string _playerName;

    private Animator _defenceAnimation;
    

    public DefenderController(DefenderControllerSeting setting)
    {
        // �����蔻��p
        _checkHitPosition = setting._checkHitPosition;//�@�����蔻��̈ʒu
        _checkHitRadiue = setting._checkHitRadiue;// �����蔻��̔��a
        _layerMask = setting._layerMask;// �����蔻��ɗL���ȃ��C��

        // ���������������Ԃɐݒ�
        Motion = DefenderMotion.standBy;// ���������������Ԃɐݒ�

        // �v���C���[�ݒ���V���A���C�Y
        _dfLRSetting = setting._dfLRSetting;// �v���C���[�ݒ���V���A���C�Y

        // ����p�^�C�}�[
        _waveHandTimermax = setting._waveHandTimermax;// ���U�铮��̍ő厞��
        _shiRaHaDoRiTimer = setting._shiRaHaDoRiTimer;// ���n��蓮��̍ő厞��
        _waveHandBackTimer = setting._waveHandBackTimer;// ���U��Ԃ�����̍ő厞��
        _coolDownTimerMax = setting._coolDownTimerMax;// �N�[���_�E������̍ő厞��

        _defenceAnimation = setting._defenceAnimation;

        _motionCnt = 0;// ����J�E���^�[

        if (_dfLRSetting == DefenderPlayerLeftRightSetting.LeftPlayer)// �v���C���[�ݒ�ɉ����ăA�^�b�N�{�^����ݒ�
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
        _motionCnt++; // ����J�E���^�[�𑝉�
    }

    public override void Update()
    {
        Think(); // �v�l����
        Move();  // �s������
        
    }

    // �v�l����
    private void Think()
    {
        DefenderMotion nm = Motion; // �ꉞ���݂̃��[�V�������w��
        switch (Motion)
        {
            case DefenderMotion.None:
                break;
            case DefenderMotion.standBy:
                if (Input.GetButtonDown(_playerName)) { nm = DefenderMotion.waveHand; } // �{�^�����͂Ŏ��U�铮��Ɉڍs
                break;
            case DefenderMotion.waveHand:
                if (_motionCnt == _waveHandTimermax) { nm = DefenderMotion.shirahadori; } // ��莞�Ԍ�ɔ��n��蓮��Ɉڍs
                break;
            case DefenderMotion.shirahadori:
                if (_motionCnt == _shiRaHaDoRiTimer&& !CheckHit()) { nm = DefenderMotion.waveHandBack; } // ��莞�Ԍ�Ɏ��U��Ԃ�����Ɉڍs
                break;
            case DefenderMotion.waveHandBack:
                if (_motionCnt == _waveHandBackTimer) { nm = DefenderMotion.coolDown; } // ��莞�Ԍ�ɃN�[���_�E������Ɉڍs
                break;
            case DefenderMotion.coolDown:
                if (_motionCnt == _coolDownTimerMax) { nm = DefenderMotion.standBy; } // ��莞�Ԍ�ɏ�����Ԃɖ߂�
                break;
            default:
                break;
        }
        UpdateMotion(nm); // ������X�V
    }

    // �s������
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
                } // ������ԂŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            case DefenderMotion.waveHand:
                if (_motionCnt == 0)
                { 
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence01");
                } // ���U�铮��ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            case DefenderMotion.shirahadori:
                if (_motionCnt == 0)
                { 
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence02");
                } // ���n��蓮��ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                if (CheckHit()) 
                { 
                    Debug.Log("Hit");
                    _defenceAnimation.SetTrigger("Defence03");
                } // �����蔻��̃`�F�b�N
                break;
            case DefenderMotion.waveHandBack:
                if (_motionCnt == 0) 
                {
                    Debug.Log(Motion.ToString() + _playerName);
                    _defenceAnimation.SetTrigger("Defence01");
                } // ���U��Ԃ�����ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            case DefenderMotion.coolDown:
                if (_motionCnt == 0) { Debug.Log(Motion.ToString() + _playerName); } // �N�[���_�E������ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            default:
                break;
        }
    }

    // ����̍X�V����
    private void UpdateMotion(DefenderMotion nm)
    {
        if (Motion == nm) { return; } // ���삪�ς��Ȃ��ꍇ�͉������Ȃ�
        else
        {
            Motion = nm; // ������X�V
            _motionCnt = 0; // ����J�E���^�[�����Z�b�g
        }
    }

    // �����蔻��̃`�F�b�N
    private bool CheckHit()
    {
        if (_checkHitPosition != null)
        {
            Collider2D _c2D = Physics2D.OverlapCircle(
            _checkHitPosition.position,
            _checkHitRadiue,
            _layerMask);
            return _c2D; // �����蔻��̌��ʂ�Ԃ�
        }
        return false;
    }

    
}
