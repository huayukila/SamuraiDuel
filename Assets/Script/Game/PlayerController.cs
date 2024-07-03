using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAction _currentAction;
    private PlayerAction[] _actions;

    private int actionIndex = 0;

    private void Start()
    {
        //‚±‚±‚Åactions‰Šú‰»,actions[0]‚Íattack,actions[1]‚Ídefences
    }
  
    }

    // Update is called once per frame
    void Update()
    {
        _currentAction.Update();
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
}