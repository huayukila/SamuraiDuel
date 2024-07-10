using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
    }

    void Init()
    {
        AudioKit.Init();
    }
}