using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingToGameScene : MonoBehaviour
{
    public void LoadingToGameScene01()
    {
        AudioKit.PlayFX("SelectSE", 1f);
        SceneManager.LoadScene("Game");
        AudioKit.StopBGM();
    }
}
