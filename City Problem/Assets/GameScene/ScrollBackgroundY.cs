using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackgroundY : MonoBehaviour {
	public Material material;
	public float speed;

	void Update () {
		if (GameManagerK.self.isGameOver)
            return;

		material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
	}
}
