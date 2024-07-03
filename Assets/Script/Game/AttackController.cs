using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    float power;
    float currentAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0,0,-power);
            power += Time.deltaTime;
            Debug.Log(power);
            currentAngle = transform.rotation.z;
        }
        else
        {
            power = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.9f*Time.deltaTime);
        }
    }
    public  float EaseInOutCirc(float x)
    {
        return x < 0.5f
        ? (1 - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * x, 2.0f))) / 2.0f
        : (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f)) + 1.0f) / 2.0f;
    }
}
