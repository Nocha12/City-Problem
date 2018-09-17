using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType {
	Can,
    Peper,
	Pet,
	Vinyl
}

public class Trash : MonoBehaviour {
	public TrashType type;

	public List<Sprite> cans;
	public List<Sprite> pepers;
	public List<Sprite> pets;
	public List<Sprite> vinyls;

	SpriteRenderer rend;

	public float speed;

	void Awake () {
		type = (TrashType)Random.Range(0, 4);

		rend = GetComponent<SpriteRenderer>();

		if(type == TrashType.Can)
			rend.sprite = cans[Random.Range(0, cans.Count)];
		if (type == TrashType.Pet)
			rend.sprite = pets[Random.Range(0, pets.Count)];
		if (type == TrashType.Peper)
			rend.sprite = pepers[Random.Range(0, pepers.Count)];
		if (type == TrashType.Vinyl)
			rend.sprite = vinyls[Random.Range(0, vinyls.Count)];
	}

	void Update()
	{
		if (GameManagerK.self.isGameOver) return;
			
		transform.Translate(Vector3.right * speed * Time.deltaTime);

		if (transform.localPosition.x > 3)
		{
			TrashScene.self.GameOver();
		}
	}
}
