using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : TemporalSingleton<RainScript>
{
    [SerializeField] float damagePerHit = 10;
    [SerializeField] float puntos = 10;
    ParticleSystem ps;

	[SerializeField] private float m_cloudSpeed = 0f;

	[SerializeField] private Transform m_nubeSprite = null;
	[SerializeField] private Transform m_endPosition = null;
	private Vector3 m_endPositionV3 = Vector3.zero;
	private Vector3 m_startPositionV3 = Vector3.zero;
	private enum CloudState { None, MovingDown, MovingUp, Raining}
	private CloudState m_currentSate = CloudState.None;


	private float m_rainDuration = 0f;


    List<ParticleCollisionEvent> pc;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    private void Start()
    {
		m_startPositionV3 = m_nubeSprite.position;
		m_endPositionV3 = m_endPosition.position;
        ps = GetComponent<ParticleSystem>();
        pc = new List<ParticleCollisionEvent>();
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
						ps.Play();
					}
				}
				break;
			case CloudState.MovingUp:
				{
					m_nubeSprite.position = Vector3.MoveTowards(m_nubeSprite.position, m_startPositionV3, m_cloudSpeed * Time.deltaTime);
					if(Vector3.Distance(m_nubeSprite.position, m_startPositionV3) <= 0.2f)
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
						ps.Stop();
					}
				}
				break;
			default:
				break;
		}
	}

	public void SpawnRain(float delay, float duration)
	{
		m_cloudSpeed = Vector3.Distance(m_nubeSprite.position, m_endPositionV3) / delay;
		m_currentSate = CloudState.MovingDown;
		m_rainDuration = duration;
	}

	public void StopRain()
	{
		m_currentSate = CloudState.MovingUp;
		ps.Stop();
	}

    public void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<ProtectionFromRain>() != null)
        {
            other.gameObject.GetComponent<ProtectionFromRain>().DealDamageToProtection(damagePerHit);
        }
        else if (other.gameObject.GetComponent<CandleBehaviour>() != null)
        {
            other.gameObject.GetComponent<CandleBehaviour>().DealDamageToCandle(damagePerHit);
        }

        Score.Instance.AñadirPuntos(puntos);
    }
    
}
