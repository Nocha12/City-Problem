using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {
	public AudioClip mainBGM;
	public AudioClip clickSound;

	private void Start()
	{
		SoundManager.self.PlaySingle(mainBGM);	
	}

	public void StartGame()
	{
		SoundManager.self.PlaySingle(clickSound);
		SceneManager.LoadScene("GameScene");
	}

	public void ExitGame()
    {
		SoundManager.self.PlaySingle(clickSound);
		Application.Quit();
    }
}
