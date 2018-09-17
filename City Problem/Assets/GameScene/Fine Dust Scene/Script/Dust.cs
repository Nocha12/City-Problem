using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour {
	public Sprite happy;
	public Sprite sad;
    
	public List<GameObject> dust;
	public List<float> originY;
	public List<float> offsets;

	public GameObject particle;
	public Transform particlePos;
    
	float offset;
    
	public float speed;
	public float accel;

	bool isHappy = true;

	private void Awake()
	{
	}

	private void Start()
	{
		foreach (var d in dust)
		{
			originY.Add(d.transform.localPosition.y);
			offsets.Add(Random.Range(0, 5.0f));
		}   
	}

	private void Update()
	{
		if (GameManagerK.self.isGameOver) return;
		if (FineDustScene.self.isPause) return;

		if(isHappy && accel < 0)
		{
			isHappy = false;

			for (int i = 0; i < dust.Count; i++)
				dust[i].GetComponent<SpriteRenderer>().sprite = sad;
		}

		if (!isHappy && accel >= 0)
        {
            isHappy = true;

			for (int i = 0; i < dust.Count; i++)
				dust[i].GetComponent<SpriteRenderer>().sprite = happy;
        }

		if (Input.GetKeyDown(KeyCode.Space))
		{
			speed += accel -= 20f * Time.deltaTime;
			Instantiate(particle, particlePos.position, Quaternion.identity);
		}

		if (transform.localPosition.x > 7)
		{
			transform.localPosition = new Vector3(7, transform.localPosition.y, transform.localPosition.z);
			accel = 0;
		}
		offset += Time.deltaTime;
		for (int i = 0; i < dust.Count; i++)
		{
			Vector3 vec = dust[i].transform.localPosition;

			dust[i].transform.localPosition = new Vector3(vec.x, originY[i] + Mathf.Sin(offset + offsets[i]) * 0.5f, vec.z);
		}

		transform.Translate(Vector3.left * speed * Time.deltaTime);

		speed += accel += Time.deltaTime;
		if (speed > 1.4f)
		{
			accel = 0;
			speed = 1.4f;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Tree")
		{
			FineDustScene.self.GameOver();
		}
	}
}
