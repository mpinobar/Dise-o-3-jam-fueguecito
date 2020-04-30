using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSpawner : TemporalSingleton<FingerSpawner>
{
	[SerializeField] private GameObject m_fingerPrefab = null;
	[SerializeField] private List<FingerSpawningAreas> m_fingerSpawningAreas = null;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			SpawnFinger(1);
		}
	}

	public void SpawnFinger(float lifeIncreaseForDifficulty)
	{
		int index = Random.Range(0, m_fingerSpawningAreas.Count);

		GameObject newFinger = Instantiate(m_fingerPrefab, m_fingerSpawningAreas[index].GenerateSpawnLocation(), Quaternion.identity);
		newFinger.GetComponent<Finger>().SetUpLife(1);
	}
}
