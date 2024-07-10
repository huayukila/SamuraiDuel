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

    // �f�o�b�O�p�e�L�X�g
    [SerializeField] TextMeshProUGUI _motiontTMP;

    private void Start()
    {
        _actions = new PlayerAction[2];
        // �����蔻��p
        _dfSetting._checkHitPosition = _checkHitPosition;//�@�����蔻��̈ʒu
        _dfSetting._checkHitRadiue = _checkHitRadiue;// �����蔻��̔��a
        _dfSetting._layerMask = _layerMask;// �����蔻��ɗL���ȃ��C��

        // �E�v���C�����v���C����
        _dfSetting._dfLRSetting = _dfLRSetting;// �v���C���[�ݒ���V���A���C�Y

        // ����p�^�C�}�[
        _dfSetting._waveHandTimermax = 5;// ���U�铮��̍ő厞��
        _dfSetting._shiRaHaDoRiTimer = 50;// ���n��蓮��̍ő厞��
        _dfSetting._waveHandBackTimer = 10;// ���U��Ԃ�����̍ő厞��
        _dfSetting._coolDownTimerMax = 180;// �N�[���_�E������̍ő厞��

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