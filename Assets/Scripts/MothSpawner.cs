using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSpawner : TemporalSingleton<MothSpawner>
{
	[SerializeField] private GameObject m_prefabMoth = null;
	private Vector3 m_min = Vector3.zero;
	private Vector3 m_max = Vector3.zero;
	private Bounds m_bounds;
	
    // Start is called before the first frame update
    void Start()
    {
		m_bounds = this.GetComponent<BoxCollider2D>().bounds;
		m_min = m_bounds.min;
		m_max = m_bounds.max;		
    }
	
	public void SpawnMoth()
	{
		Vector3 randomPos = new Vector3(Random.Range(m_min.x, m_max.x), Random.Range(m_min.y, m_max.y), 0);
		GameObject newMoth = Instantiate(m_prefabMoth, randomPos, Quaternion.identity);
	}
}
