using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] Sprite[] diceArray;
    [SerializeField] private SpriteRenderer diceSpriteLeft;
    [SerializeField] SpriteRenderer diceSpriteRight;
    private bool isStop;
    private float rollTime;
    private float disappearTime;
    private bool isDiceVisible;

    int countLeft;
    int countRight;

    public PlayerController LeftPlayer;
    public PlayerController RightPlayer;

    void Start()
    {
        isStop = false;
        isDiceVisible = true;
        disappearTime = 3.0f; // サイコロが消えるまでの時間（秒）
    }

    void Update()
    {
        rollTime += Time.deltaTime;

        if (isStop && isDiceVisible)
        {
            disappearTime -= Time.deltaTime;
            if (disappearTime <= 0)
            {
                HideDice();
                isDiceVisible = false;
            }
            return;
        }

        if (isStop)
            return;
        if (rollTime >= 1.5f && countLeft!=countRight)
        {
            isStop = true;
            DetermineAttack();
        }
        else
        {
            isStop = false;
            RollDice();
        }
    }

    void RollDice()
    {
         countLeft = Random.Range(1, 7);
        diceSpriteLeft.sprite = diceArray[countLeft - 1];
        countRight = Random.Range(1, 7);
        diceSpriteRight.sprite = diceArray[countRight - 1];
    }

    void DetermineAttack()
    {
        // それぞれのプレイヤーのサイコロを振る

        diceSpriteLeft.sprite = diceArray[countLeft - 1];
        diceSpriteRight.sprite = diceArray[countRight - 1];

        Debug.Log(countLeft + " "+ countRight);
        // アタックの判定
        Invoke("ChangeMode",1.7f);
    }

    void ChangeMode()
    {
        if (countLeft > countRight)
        {
            LeftPlayer.SetPlayerAttackMode();
            RightPlayer.SetPlayerDefenceMode();
        }
        else
        {
            RightPlayer.SetPlayerAttackMode();
            LeftPlayer.SetPlayerDefenceMode();
        }
    }

    void HideDice()
    {
        diceSpriteLeft.sprite = null;
        diceSpriteRight.sprite = null;
    }

    //
    //
}
