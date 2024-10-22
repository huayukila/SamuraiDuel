using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSEController : MonoBehaviour
{
    public static TitleSEController instance;
    private void Awake()
    {
        //インスタンスの設定
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
