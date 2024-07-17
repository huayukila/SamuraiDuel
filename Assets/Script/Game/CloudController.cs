using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [System.Serializable]
    public class Cloud
    {
        public GameObject _cloudObject;//�N���E�h�̃Q�[���I�u�W�F�N�g
        public float _speed = 1.0f;//�ړ��X�s�[�h
        public enum MoveDirection { right, left }
        public MoveDirection _direction = MoveDirection.left;//�ړ��̕���

    }

    [SerializeField] private Cloud[] _clouds;

    void Update()
    {
        foreach (var cloud in _clouds)
        {
            if (cloud._cloudObject != null)//�N���E�h�̃Q�[���I�u�W�F�N�g�̑��݂�����
            {
                //�ړ�������ݒ�
                Vector2 _dirVec;
                if (cloud._direction == Cloud.MoveDirection.right)
                {
                    _dirVec = Vector2.right;
                }
                else
                {
                    _dirVec = Vector2.left;
                }

                //�N���E�h���ړ�������
                cloud._cloudObject.transform.Translate(_dirVec * cloud._speed * Time.deltaTime);
            }
        }
    }
}
