using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObj : MonoBehaviour
{   
    private Vector3 originPosition;

	private float shakeintensity;
    public float coefShakeIntensity = 0.5f;

	public float shakeRange = 0.2f;

    void Awake()
    {
		originPosition = transform.position;
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
			Shake();
		
        if (shakeintensity > 0)
        {
			transform.position = originPosition + new Vector3(Random.Range(-shakeRange, shakeRange), Random.Range(-shakeRange, shakeRange), 0) * shakeintensity;
            shakeintensity -= Time.deltaTime;
        }
    }

    public void Shake()
    {
		shakeintensity = coefShakeIntensity;
    }
}
