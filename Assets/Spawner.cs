using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : TemporalSingleton<Spawner>
{
	[SerializeField] private float m_cloudSpeed = 0f;

	[SerializeField] private Transform m_nubeSprite = null;
	[SerializeField] private Transform m_endPosition = null;
	private Vector3 m_endPositionV3 = Vector3.zero;
	private Vector3 m_startPositionV3 = Vector3.zero;
	private enum CloudState { None, MovingDown, MovingUp, Raining }
	private CloudState m_currentSate = CloudState.None;

	float m_rainDuration = 0f;


	public List<GameObject> prefabsToSpawn;
    [SerializeField] float timeBetweenSpawns;
    float tmp;
    Transform candle;
	

	// Start is called before the first frame update
	void Start()
    {
        candle = GameObject.FindGameObjectWithTag("Candle").transform;
		m_startPositionV3 = m_nubeSprite.position;
		m_endPositionV3 = m_endPosition.position;
	}

	private void Update()
	{
		switch (m_currentSate)
		{
			case CloudState.None:
				break;
			case CloudState.MovingDown:
				{
					m_nubeSprite.position = Vector3.MoveTowards(m_nubeSprite.position, m_endPositionV3, m_cloudSpeed * Time.deltaTime);
					if (Vector3.Distance(m_nubeSprite.position, m_endPositionV3) <= 0.2f)
					{
						m_currentSate = CloudState.Raining;
						Spawn();
					}
				}
				break;
			case CloudState.MovingUp:
				{
					m_nubeSprite.position = Vector3.MoveTowards(m_nubeSprite.position, m_startPositionV3, m_cloudSpeed * Time.deltaTime);
					if (Vector3.Distance(m_nubeSprite.position, m_startPositionV3) <= 0.2f)
					{

						m_currentSate = CloudState.None;
					}
				}
				break;
			case CloudState.Raining:
				{
					m_rainDuration = m_rainDuration - Time.deltaTime;
					if (m_rainDuration <= 0)
					{
						m_currentSate = CloudState.MovingUp;
					}
				}
				break;
			default:
				break;
		}
	}

	public void SpawnWind(float delay, float duration)
	{
		m_cloudSpeed = Vector3.Distance(m_nubeSprite.position, m_endPositionV3) / delay;
		m_currentSate = CloudState.MovingDown;
		m_rainDuration = duration;
	}

	void Spawn()
	{
		int rand = Random.Range(0, prefabsToSpawn.Count);
		GameObject i = Instantiate(prefabsToSpawn[rand], transform.position, Quaternion.identity);
		i.GetComponent<ApproachingDanger>().Candle = candle;
		i.GetComponent<ApproachingDanger>().ChangeSpeed(m_rainDuration);
	}
}
