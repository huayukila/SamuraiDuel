using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct DefendSuccesed { }
public struct AttackSuccesed { }
public class PlayerManager : MonoBehaviour
{
    public PlayerController dog;
    public PlayerController cat;

    public GameObject SwitchUI;
    public GameObject GgameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Register<DefendSuccesed>(e => {

            // 3秒後にモード切り替えを行う
            Invoke("SwitchActions", 3f);

        }).UnregisterWhenGameObjectDestroyed(gameObject);

        EventSystem.Register<AttackSuccesed>(e =>
        {
            // 3秒後にシーン遷移を行う
            Invoke("LoadTitleScene", 3f);
        }).UnregisterWhenGameObjectDestroyed(gameObject);
    }

    // 3秒後に呼ばれるメソッド
    private void SwitchActions()
    {
        dog.SwitchAction();
        cat.SwitchAction();
    }

    // 3秒後に呼ばれるメソッド
    private void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
