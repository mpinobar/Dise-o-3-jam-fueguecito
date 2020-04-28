using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : MonoBehaviour
{
    [SerializeField] float damagePerHit = 10;
    ParticleSystem ps;

    List<ParticleCollisionEvent> pc;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        pc = new List<ParticleCollisionEvent>();
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
        //Destroy(gameObject);
    }
    
}
