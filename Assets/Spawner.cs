using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : TemporalSingleton<Spawner>
{
    public List<GameObject> prefabsToSpawn;
    [SerializeField] float timeBetweenSpawns;
    float tmp;
    Transform candle;

    // Start is called before the first frame update
    void Start()
    {
        candle = GameObject.FindGameObjectWithTag("Candle").transform;
    }
	
	public void SpawnWind(float delay, float duration)
	{
		int rand = Random.Range(0, prefabsToSpawn.Count);
		GameObject i = Instantiate(prefabsToSpawn[rand], transform.position, Quaternion.identity);
		i.GetComponent<ApproachingDanger>().Candle = candle;
	}
}
