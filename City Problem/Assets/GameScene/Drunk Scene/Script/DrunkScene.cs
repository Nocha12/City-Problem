using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkScene : MonoBehaviour {
	public static DrunkScene self;

	public SpriteRenderer close;
	public Animator turnOn;

	public bool isPause = true;

	void Awake()
    {
        self = this;
    }

    public void StartGame()
    {
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
}
