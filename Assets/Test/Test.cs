using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TestEvent
{
}

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Register<TestEvent>(e => { Debug.Log(1); }).UnregisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventSystem.Send<TestEvent>();
        }
    }
}