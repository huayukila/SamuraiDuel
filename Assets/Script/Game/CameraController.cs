using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _shakeTime;
    [SerializeField] float _magnitude;

    float _shakecount = 0.0f;
    float mg;
    Vector3 pos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShakeCheck();
        }
    }
    private void ShakeCheck()
    {
            pos= transform.position;
            _shakecount = 0f;
            mg = _magnitude;
            StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        while (_shakecount < _shakeTime)
        {
            float x = Random.Range(-_magnitude, _magnitude);
            float y = Random.Range(-_magnitude, _magnitude);
            transform.position = new Vector3(x, y, pos.z);

            _shakecount += Time.deltaTime;
            _magnitude -= Time.deltaTime;
            yield return null;
        }
        transform.position = pos;
        _magnitude = mg;
    }
}
