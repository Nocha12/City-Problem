using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
	SpriteRenderer rend;

	public List<Sprite> sprites;

	public float maxSpeed;
	public float minSpeed;

	float speed;

	private void Awake()
	{
		rend = GetComponent<SpriteRenderer>();
  
		rend.sprite = sprites[Random.Range(0, sprites.Count)];

		speed = Random.Range(minSpeed, maxSpeed);
	}
	
	void Update () {
		if (GameManagerK.self.isGameOver)
			return;

		transform.Translate(Vector3.left * speed * Time.deltaTime);

		if (transform.localPosition.x < -7.5f)
			Destroy(gameObject);
	}
}
