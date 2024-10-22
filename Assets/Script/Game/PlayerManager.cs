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
    public GameObject GameOverUI;
    public CameraController CameraController;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Register<DefendSuccesed>(e =>
        {

            // 3�b��Ƀ��[�h�؂�ւ����s��
            CameraController.ShakeCheck();
            Invoke("SwitchActions", 3f);
            ShowSwitchUI();

        }).UnregisterWhenGameObjectDestroyed(gameObject);

        EventSystem.Register<AttackSuccesed>(e =>
        {
            // 3�b��ɃV�[���J�ڂ��s��
            CameraController.ShakeCheck();
            Invoke("LoadTitleScene", 3f);
            ShowGameOverUI();
        }).UnregisterWhenGameObjectDestroyed(gameObject);
    }

    // 3�b��ɌĂ΂�郁�\�b�h
    private void SwitchActions()
    {
        dog.SwitchAction();
        cat.SwitchAction();
    }

    private void ShowSwitchUI()
    {
        SwitchUI.SetActive(true);
        Invoke("HideSwitchUI", 3f);
    }

    private void HideSwitchUI()
    {
        SwitchUI.SetActive(false);
    }

    private void ShowGameOverUI()
    {
        GameOverUI.SetActive(true);
        Invoke("HideGameOverUI", 3f);
        Invoke("LoadTitleScene", 3f);
    }

    private void HideGameOverUI()
    {
        GameOverUI.SetActive(false);
    }

    // 3�b��ɌĂ΂�郁�\�b�h
    private void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
        AudioKit.PlayBGM("TitleBGM", 1f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
