using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpandDown : MonoBehaviour
{
    [SerializeField]
    public float targetHeight = 2.0f; // 目標の高さ
    [SerializeField]
    public float moveSpeed = 2.0f; // 移動速度
    [SerializeField]
    public float waitTime = 0.5f; // 待機時間

    private bool movingDown = true; // 現在下がっているかどうか
    private Vector3 startPosition; // 初期位置

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingDown)
        {
            // 下に移動
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetHeight, transform.position.z), moveSpeed * Time.deltaTime);

            // 目標の高さに到達したかどうかをチェック
            if (transform.position.y <= targetHeight)
            {
                movingDown = false;
                StartCoroutine(WaitAndMoveUp());
            }
        }
    }

    private IEnumerator WaitAndMoveUp()
    {
        // 指定された時間待機
        yield return new WaitForSeconds(waitTime);

        // 上に戻る
        while (transform.position.y < startPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    //
}
