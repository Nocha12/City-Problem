using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineDustScene : MonoBehaviour
{
	public static FineDustScene self;
	public GameObject cloudPrefab;

	public SpriteRenderer close;

	public float minDelay;
	public float maxDelay;
 
	public Transform rightPos;

	public Transform dust;

	public Animator turnOn;

	public bool isPause = true;
    
	private void Awake()
	{
		self = this;
	}

	public void StartGame()
	{
		StartCoroutine(MakeCloud());
		isPause = false;
	}

	public void GameOver()
	{
		GameManagerK.self.isGameOver = true;
  
		StartCoroutine(CloseEffect());
    }

    IEnumerator CloseEffect()
    {
        StartCoroutine(GameManagerK.self.CloseEffect(close, 0.2f));
        yield return new WaitForSeconds(0.2f);
		StartCoroutine(GameManagerK.self.Shake(close.gameObject, 1f, 0.4f));
		yield return new WaitForSeconds(0.5f);
        StartCoroutine(GameManagerK.self.Fade());
    }

	IEnumerator MakeCloud()
	{
		while (true)
		{
			Cloud cloud;
            
			cloud = Instantiate(cloudPrefab, rightPos.position, Quaternion.identity, transform).GetComponent<Cloud>();
            
			Vector3 vec = cloud.transform.localPosition;
			cloud.transform.localPosition = new Vector3(vec.x, vec.y + Random.Range(0, 1), 1);
            
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
		}
	}
}
