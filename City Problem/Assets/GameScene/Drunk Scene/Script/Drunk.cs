using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunk : MonoBehaviour {
	public float acceleration = 0;
	public float currentRot;
	public float keyAccel = 0;

	public Transform body;
	public Transform head;
	public Transform outHand;
	public Transform inHand;

	public Animator anim1;
	public Animator anim2;
	public Animator anim3;

	void Update()
	{
		if (GameManagerK.self.isGameOver)
		{
			anim1.SetTrigger("isGameOver");
			anim2.SetTrigger("isGameOver");
			anim3.SetTrigger("isGameOver");
			return;
		}

		if (DrunkScene.self.isPause)
			return;

		body.eulerAngles = new Vector3(0, 0, currentRot);

		outHand.localEulerAngles = new Vector3(0, 0, -currentRot);
		inHand.localEulerAngles = new Vector3(0, 0, -currentRot);

		bool isKeyDown = false;

		if (Input.GetKeyDown(KeyCode.Q))
			acceleration = 0;
		else if (Input.GetKey(KeyCode.Q))
		{
			isKeyDown = true;

			keyAccel += Time.deltaTime;   
		}
		else if (Input.GetKeyUp(KeyCode.Q))
			keyAccel = 0;

		if (Input.GetKeyDown(KeyCode.E))
			acceleration = 0;
		else if (Input.GetKey(KeyCode.E))
		{
			isKeyDown = true;
   
			keyAccel -= Time.deltaTime;        
		}
		else if (Input.GetKeyDown(KeyCode.E))
			keyAccel = 0;

		if (currentRot > 110 || currentRot < -110)
		{
			DrunkScene.self.GameOver();
		}

		if (isKeyDown)
			acceleration += keyAccel;
		else {
			if(currentRot < 0)
				acceleration -= Time.deltaTime;
			else 
				acceleration += Time.deltaTime;
		}

		currentRot += acceleration;
	}
}
