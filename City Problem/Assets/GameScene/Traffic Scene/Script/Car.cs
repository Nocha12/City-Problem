using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarType {
	RedCar,
    BlueCar
}

public class Car : MonoBehaviour {
	public CarType type;
	public float speed;

	void Update()
	{
		if (GameManagerK.self.isGameOver)
            return;
		
		transform.Translate(Vector3.down * speed * Time.deltaTime);

		if (transform.localPosition.y < -7)
			Destroy(gameObject);
	}
    
    public IEnumerator Moving(Transform target, float posX, float time)
    {
        float t = 0f;

		Vector3 startPos = target.localPosition;
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        while (t < 1f)
		{         
            t += Time.deltaTime / time;
            
			float newPosX = Mathf.Lerp(startPos.x, posX, curve.Evaluate(t));

			target.localPosition = new Vector3(newPosX, transform.localPosition.y, transform.localPosition.z);

            yield return null;
        }
    }  
}
