using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerK : MonoBehaviour {
 
	public static GameManagerK self;
	public bool isGameOver;
	public float speed;
	public AudioClip gameBGM;
	public float score;

	public Text scoreText;
	Coroutine gameC;

	int count;

	public CanvasGroup gameOverImg;

	private void Awake()
	{
		self = this;

		SoundManager.self.PlayBGM(gameBGM);
	}

    void Start()
	{
		score = 0;
		Time.timeScale = 1;
		DrunkScene.self.turnOn.SetTrigger("TurnOn");
		gameC = StartCoroutine(StartDrunkScene());
	}

	IEnumerator StartDrunkScene()
	{
		yield return new WaitForSeconds(1.2f);
		DrunkScene.self.turnOn.gameObject.SetActive(false);
		TurnOn();

		yield return new WaitForSeconds(4f);
		TrashScene.self.turnOn.SetTrigger("TurnOn");

		yield return new WaitForSeconds(1.2f);
		TrashScene.self.turnOn.gameObject.SetActive(false);
        TurnOn();
        
		yield return new WaitForSeconds(4f);
		FineDustScene.self.turnOn.SetTrigger("TurnOn");

        yield return new WaitForSeconds(1.2f);
		FineDustScene.self.turnOn.gameObject.SetActive(false);
        TurnOn();
        
		yield return new WaitForSeconds(4f);
		TrafficScene.self.turnOn.SetTrigger("TurnOn");
        
        yield return new WaitForSeconds(1.2f);
		TrafficScene.self.turnOn.gameObject.SetActive(false);
        TurnOn();
	}

	public IEnumerator Fade()
    {
        for (float f = 0; f <= 1; f += 0.01f)
        {
			gameOverImg.alpha = f;
            
			yield return null;
        }
    }
       
	void Update() {
		if (gameOverImg.alpha > 0.5f && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)))
		{
			SceneManager.LoadScene("MainScene");
			return;
		}

		if (isGameOver)
			StopCoroutine(gameC);

		Time.timeScale += speed;

		if (!isGameOver)
		{
			score += Time.deltaTime * 5;
			scoreText.text = ((int)score).ToString();
		}      
	}
    
	public IEnumerator CloseEffect(SpriteRenderer target, float time)
    {
        float t = 0f;

		Vector3 startScale = target.transform.localScale * 3;
		Vector3 scaleEnd = target.transform.localScale;
        
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        while (t < 1f)
        {
            t += Time.deltaTime / time;

			target.transform.localScale = Vector3.Lerp(startScale, scaleEnd, curve.Evaluate(t));
            
			target.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, curve.Evaluate(t)));

            yield return null;
        }
    }

	public IEnumerator Shake(GameObject obj, float range, float shakeintensity = 0.5f)   
	{
		Vector3 originPosition;

		originPosition = obj.transform.position;
        
		while(shakeintensity > 0)
		{         
			obj.transform.position = originPosition + new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0) * shakeintensity;
            shakeintensity -= Time.deltaTime;

			yield return null;
		}
	}
    
    public void TurnOn()
    {
		if(count == 0)        
			DrunkScene.self.StartGame();
		else if(count == 1)
			TrashScene.self.StartGame();
		else if(count == 2)
			FineDustScene.self.StartGame();
		else if(count == 3)
			TrafficScene.self.StartGame();

		count++;
	}
}
