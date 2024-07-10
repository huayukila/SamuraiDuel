using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public int count;
    [SerializeField] Sprite[] diceArray;
    private SpriteRenderer diceSprite;
    private bool isStop;
    private float rollTime;

    void Start()
    {
        count = Random.Range(1, 7);
        isStop = false;
        diceSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rollTime += Time.deltaTime;
        if (rollTime >= 3.0f)
        {
            isStop = true;
        }
        else 
        {
            isStop = false;
        }

        if (!isStop)
        {
            count = Random.Range(1, 7);
            if (count == 1)
            {
                diceSprite.sprite = diceArray[0];
            }
            if (count == 2)
            {
                diceSprite.sprite = diceArray[1];
            }
            if (count == 3)
            {
                diceSprite.sprite = diceArray[2];
            }
            if (count == 4)
            {
                diceSprite.sprite = diceArray[3];
            }
            if (count == 5)
            {
                diceSprite.sprite = diceArray[4];
            }
            if (count == 6)
            {
                diceSprite.sprite = diceArray[5];
            }
        }
    }
}
