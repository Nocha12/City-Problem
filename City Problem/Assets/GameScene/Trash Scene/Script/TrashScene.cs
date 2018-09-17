using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScene : MonoBehaviour
{
	public static TrashScene self;

	public GameObject trashPrefab;
	public float delay;

	public Transform spawnPos;
	public List<Trash> trashes;
	public List<TrashCan> trashCans;

	public AudioClip clip;

	Coroutine scaleC;

	public SpriteRenderer close;

	public Animator turnOn;

	void Awake()
	{
		self = this;
	}

	public void StartGame()
	{
		StartCoroutine(MakeTrash());
	}

	public void GameOver()
	{
		GameManagerK.self.isGameOver = true;

		StartCoroutine(CloseEffect());
	}

	IEnumerator CloseEffect() {
		StartCoroutine(GameManagerK.self.CloseEffect(close, 0.2f));
		yield return new WaitForSeconds(0.2f);
        StartCoroutine(GameManagerK.self.Shake(close.gameObject, 1f, 0.4f));
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(GameManagerK.self.Fade());

	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.W))
			CatchTrash(TrashType.Peper);
		if (Input.GetKeyDown(KeyCode.S))
			CatchTrash(TrashType.Pet);
		if (Input.GetKeyDown(KeyCode.A))
			CatchTrash(TrashType.Can);
		if (Input.GetKeyDown(KeyCode.D))
			CatchTrash(TrashType.Vinyl);
	}

	IEnumerator MakeTrash()
    {
        while (true)
        {
			Trash trash;
			
			trash = Instantiate(trashPrefab, spawnPos.position, Quaternion.identity, transform).GetComponent<Trash>();
			trashes.Add(trash);           

            yield return new WaitForSeconds(delay);
        }
    }

	void CatchTrash(TrashType type)
	{
		if (trashes.Count == 0)
			return;

		Trash t = trashes[0];

        if(t.type == type)
		{
			SoundManager.self.PlaySingle(clip);

			TrashCan trashCan = trashCans[(int)type];

			trashCan.StartCoroutine(trashCan.ScaleEffect(trashCan.transform));

			trashes.Remove(t);
            Destroy(t.gameObject);
        }
	}   
}
