
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    // ����̗񋓌^���`
    enum Motion
    {
        None = -1,          // ����Ȃ�
        standBy,            // ����
        waveHand,           // ���U��
        shirahadori,        // ���n���
        waveHandBack,       // ���U��Ԃ�
        coolDown,           // �N�[���_�E��
    }
    // ���������������Ԃɐݒ�
    Motion _motion = Motion.standBy;

    // �v���C���[�ݒ�̗񋓌^���`
    enum PlayerSetting
    {
        LeftPlayer,         // ���v���C���[
        RightPlayer,        // �E�v���C���[
    }
    // �v���C���[�ݒ���V���A���C�Y
    [SerializeField] PlayerSetting _playerSetting;

    // �����蔻��p
    [SerializeField] Transform _checkHitPosition;//�@�����蔻��̈ʒu
    [SerializeField] LayerMask _layerMask;// �����蔻��ɗL���ȃ��C��
    [SerializeField] private float _checkHitRadiue = 1f; // �����蔻��̔��a

    // ����p�^�C�}�[
    [SerializeField] private int _waveHandTimermax = 5;  // ���U�铮��̍ő厞��
    [SerializeField] private int _shiRaHaDoRiTimer = 50; // ���n��蓮��̍ő厞��
    [SerializeField] private int _waveHandBackTimer = 10;// ���U��Ԃ�����̍ő厞��
    [SerializeField] private int _coolDownTimerMax = 180;// �N�[���_�E������̍ő厞��

    // �ϑ��p�ϐ�
    [Header("�ϑ��p---------------------------")]
    [SerializeField] private int _motionCnt = 0; // ����J�E���^�[

    // �f�o�b�O�p�e�L�X�g
    [SerializeField] TextMeshProUGUI _motiontTMP;

    // �v���C���[����ێ�����ϐ�
    private string _playerName;

    private void Start()
    {
        // �v���C���[�ݒ�ɉ����ăA�^�b�N�{�^����ݒ�
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
        _motionCnt++; // ����J�E���^�[�𑝉�
    }

    private void Update()
    {
        Think(); // �v�l����
        Move();  // �s������
        if (_motiontTMP != null)
        {
            _motiontTMP.text = _motion.ToString()+"\n"+_playerSetting;

        }
    }

    // �v�l����
    private void Think()
    {
        Motion nm = _motion; // �ꉞ���݂̃��[�V�������w��
        switch (_motion)
        {
            case Motion.None:
                break;
            case Motion.standBy:
                if (Input.GetButtonDown(_playerName)) { nm = Motion.waveHand; } // �{�^�����͂Ŏ��U�铮��Ɉڍs
                break;
            case Motion.waveHand:
                if (_motionCnt == _waveHandTimermax) { nm = Motion.shirahadori; } // ��莞�Ԍ�ɔ��n��蓮��Ɉڍs
                break;
            case Motion.shirahadori:
                if (_motionCnt == _shiRaHaDoRiTimer) { nm = Motion.waveHandBack; } // ��莞�Ԍ�Ɏ��U��Ԃ�����Ɉڍs
                break;
            case Motion.waveHandBack:
                if (_motionCnt == _waveHandBackTimer) { nm = Motion.coolDown; } // ��莞�Ԍ�ɃN�[���_�E������Ɉڍs
                break;
            case Motion.coolDown:
                if (_motionCnt == _coolDownTimerMax) { nm = Motion.standBy; } // ��莞�Ԍ�ɏ�����Ԃɖ߂�
                break;
            default:
                break;
        }
        UpdateMotion(nm); // ������X�V
    }

    // �s������
    private void Move()
    {
        switch (_motion)
        {
            case Motion.None:
                break;
            case Motion.standBy:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // ������ԂŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            case Motion.waveHand:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // ���U�铮��ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            case Motion.shirahadori:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // ���n��蓮��ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                if (CheckHit()) { Debug.Log("Hit"); } // �����蔻��̃`�F�b�N
                break;
            case Motion.waveHandBack:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // ���U��Ԃ�����ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            case Motion.coolDown:
                if (_motionCnt == 0) { Debug.Log(_motion.ToString() + _playerName); } // �N�[���_�E������ŃJ�E���^�[��0�̏ꍇ�A���O���o��
                break;
            default:
                break;
        }
    }

    // ����̍X�V����
    private void UpdateMotion(Motion nm)
    {
        if (_motion == nm) { return; } // ���삪�ς��Ȃ��ꍇ�͉������Ȃ�
        else
        {
            _motion = nm; // ������X�V
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

    // �M�Y����`��
    private void OnDrawGizmos()
    {
        if (_checkHitPosition != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_checkHitPosition.position, _checkHitRadiue); // �M�Y���Ƃ��ē����蔻��͈̔͂�`��
        }
    }
}
