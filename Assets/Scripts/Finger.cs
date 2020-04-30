using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
	[SerializeField] private float m_speed = 0f;
	[SerializeField] private float m_burntSpeedModifier = 0f;
	[SerializeField] private float m_yOffset = 0f;
	[SerializeField] private float m_stoppingDistance = 0f;

	[SerializeField] private Sprite m_burntSprite = null;
	private SpriteRenderer m_cmpSpriteRenderer = null;
	
	private Transform m_candle = null;
	private Vector3 m_startingPos = Vector3.zero;
	private Vector3 m_currentDestination = Vector3.zero;
	private bool m_isBurnt = false;

	[SerializeField] private int m_maxMinLife = 0;
	private int m_currentLife = 0;
	[SerializeField] private float m_damage = 0;
	[SerializeField] private Color m_color = Color.white;
	[SerializeField] private Color m_red = Color.red;

	[SerializeField] private LayerMask m_layers;

	private void Awake()
	{
		m_startingPos = this.transform.position;
		m_cmpSpriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
		m_candle = CandleBehaviour.Instance.transform;
		m_currentDestination = m_candle.position;
		this.transform.up = (m_currentDestination + new Vector3(0, m_yOffset, 0)) - this.transform.position;
	}
	
    // Update is called once per frame
    void Update()
    {
		float speed = m_isBurnt ? m_speed * Time.deltaTime *  m_burntSpeedModifier : m_speed * Time.deltaTime;
		
		this.transform.position = Vector3.MoveTowards(this.transform.position, m_currentDestination + new Vector3(0, m_yOffset, 0), speed);

		if (Vector3.Distance(this.transform.position, m_currentDestination + new Vector3(0, m_yOffset, 0)) <= m_stoppingDistance)
		{
			if (m_isBurnt)
			{
				Destroy(this.gameObject);
			}
			else
			{
				m_currentDestination = m_startingPos;
				m_cmpSpriteRenderer.color = m_color;
				CandleBehaviour.Instance.DealDamageToCandle(m_damage);
				m_isBurnt = true;
			}			
		}

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f, m_layers);
			if(hit.collider != null)
			{
				if (hit.collider.gameObject == this.gameObject)
				{
					if (!m_isBurnt)
					{
						m_currentLife = m_currentLife - 1;
						if (m_currentLife <= 0)
						{
							m_isBurnt = true;
							m_currentDestination = m_startingPos;
							m_cmpSpriteRenderer.color = m_red;
						}
					}
				}
			}
		}
	}

	public void SetUpLife(int LifeIncrease)
	{
		m_currentLife = m_maxMinLife + LifeIncrease;
	}

	

	public void ScareFinger()
	{
		if (!m_isBurnt)
		{
			m_currentDestination = m_startingPos;
			m_isBurnt = true;
			m_cmpSpriteRenderer.color = m_red;
		}
	}
	public void SetUpSpeed(float duration)
	{
		m_speed = Vector3.Distance(this.transform.position, m_currentDestination + new Vector3(0, m_yOffset, 0)) / duration;
	}
}
