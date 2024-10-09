using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryController : MonoBehaviour
{
    float timer = 0;
    public float DestroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > DestroyTimer)
        {
            Destroy(gameObject);
        }
    }
}
