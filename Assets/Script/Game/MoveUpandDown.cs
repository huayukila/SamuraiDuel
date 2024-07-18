using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpandDown : MonoBehaviour
{
    [SerializeField]
    public float targetHeight = 2.0f; // �ڕW�̍���
    [SerializeField]
    public float moveSpeed = 2.0f; // �ړ����x
    [SerializeField]
    public float waitTime = 0.5f; // �ҋ@����

    private bool movingDown = true; // ���݉������Ă��邩�ǂ���
    private Vector3 startPosition; // �����ʒu

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingDown)
        {
            // ���Ɉړ�
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetHeight, transform.position.z), moveSpeed * Time.deltaTime);

            // �ڕW�̍����ɓ��B�������ǂ������`�F�b�N
            if (transform.position.y <= targetHeight)
            {
                movingDown = false;
                StartCoroutine(WaitAndMoveUp());
            }
        }
    }

    private IEnumerator WaitAndMoveUp()
    {
        // �w�肳�ꂽ���ԑҋ@
        yield return new WaitForSeconds(waitTime);

        // ��ɖ߂�
        while (transform.position.y < startPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    //
}
