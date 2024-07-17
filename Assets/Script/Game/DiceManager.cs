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

    int countLeft;
    int countRight;

    public PlayerController LeftPlayer;
    public PlayerController RightPlayer;

    void Start()
    {
        isStop = false;
    }

    void Update()
    {
        rollTime += Time.deltaTime;
        if (isStop)
            return;
        if (rollTime >= 3.0f&& countLeft!=countRight)
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
        int leftDiceCount = Random.Range(1, 7);
        int rightDiceCount = Random.Range(1, 7);

        diceSpriteLeft.sprite = diceArray[countLeft - 1];
        diceSpriteRight.sprite = diceArray[countRight - 1];
        // アタックの判定
        //if (leftDiceCount > rightDiceCount)
        //{
        //    LeftPlayer.Attack();
        //    RightPlayer.Defen();
        //}
        //else if (rightDiceCount > leftDiceCount)
        //{
        //    RightPlayer.Attack();
        //    LeftPlayer.Defen();
        //}
    }
}
