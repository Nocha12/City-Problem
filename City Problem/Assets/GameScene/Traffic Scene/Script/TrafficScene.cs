using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficScene : MonoBehaviour {
	public static TrafficScene self;

	public GameObject redCarPrefab;
	public GameObject blueCarPrefab;

	public SpriteRenderer close;

	public Transform spawnPos;
	public float delay;

	public List<Car> cars;

	public Animator turnOn;

	private void Awake()
	{
		self = this;
	}

	public void StartGame()
	{
		StartCoroutine(MakeCar());
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

	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			TrafficOrganization(CarType.RedCar);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			TrafficOrganization(CarType.BlueCar);
	}
    
	void TrafficOrganization(CarType type)
	{
		if (cars.Count > 0)
		{
			if (cars[0].type == type)
			{
				if (type == CarType.RedCar)
					cars[0].StartCoroutine(cars[0].Moving(cars[0].transform, -2, 0.1f));
				else
					cars[0].StartCoroutine(cars[0].Moving(cars[0].transform, 2, 0.1f));
                
				cars.Remove(cars[0]);
			}
		}
	}

	IEnumerator MakeCar()
    {
        while (true)
        {
            bool isRed = Random.Range(0, 2) == 1;
            Car car;

			if (isRed)
            {
				car = Instantiate(redCarPrefab, spawnPos.position, Quaternion.identity, transform).GetComponent<Car>();
				car.type = CarType.RedCar;
            }
            else
            {
				car = Instantiate(blueCarPrefab, spawnPos.position, Quaternion.identity, transform).GetComponent<Car>();
				car.type = CarType.BlueCar;
            }

			cars.Add(car);
            
            yield return new WaitForSeconds(delay);
        }
    }
}
