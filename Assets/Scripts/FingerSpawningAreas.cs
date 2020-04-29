using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSpawningAreas : MonoBehaviour
{
	[SerializeField] private Transform m_limitA = null;
	[SerializeField] private Transform m_limitB = null;
	private Vector3 m_position = Vector3.zero;
	private float m_marginX = 0f;
	private float m_marginY = 0f;

	private void Start()
	{
		m_position = (m_limitA.position + m_limitB.position)/2;
		m_marginX = Mathf.Abs(m_limitA.position.x - m_position.x);
		m_marginY = Mathf.Abs(m_limitA.position.y - m_position.y);
	}


	public Vector2 GenerateSpawnLocation()
	{
		Vector2 spawnPoint = new Vector2(Random.Range(-m_marginX, m_marginX) + m_position.x, Random.Range(-m_marginY, m_marginY) + m_position.y);

		return spawnPoint;
	}

}
