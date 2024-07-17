using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [System.Serializable]
    public class Cloud
    {
        public GameObject _cloudObject;//クラウドのゲームオブジェクト
        public float _speed = 1.0f;//移動スピード
        public enum MoveDirection { right, left }
        public MoveDirection _direction = MoveDirection.left;//移動の方向

    }

    [SerializeField] private Cloud[] _clouds;

    void Update()
    {
        foreach (var cloud in _clouds)
        {
            if (cloud._cloudObject != null)//クラウドのゲームオブジェクトの存在を検査
            {
                //移動方向を設定
                Vector2 _dirVec;
                if (cloud._direction == Cloud.MoveDirection.right)
                {
                    _dirVec = Vector2.right;
                }
                else
                {
                    _dirVec = Vector2.left;
                }

                //クラウドを移動させる
                cloud._cloudObject.transform.Translate(_dirVec * cloud._speed * Time.deltaTime);
            }
        }
    }
}
