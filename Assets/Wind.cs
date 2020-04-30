using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : ApproachingDanger
{
    [SerializeField] GameObject abanicoPrefab;
    GameObject abanico;
    [SerializeField] float puntos;
    [SerializeField] float damageToCandle = 10;
    [SerializeField] float windHP = 100;
    [SerializeField] float armadura = 25;
    public float currentHP;

    SpriteRenderer spr;
    private void Start()
    {
        currentHP = windHP;
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Cursor")
        {
            abanico.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y,0);
            currentHP -= Mathf.Abs(collision.GetComponent<Cursor>().velocity.y) * (1 - (armadura * 0.01f));
            if(currentHP > 0)
            {
                Color c = spr.color;
                c.a = currentHP / windHP;
                spr.color = c;
            }
            else
            {
                Score.Instance.AñadirPuntos(puntos);
                Destroy(abanico);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Candle")
        {
            collision.transform.root.GetComponent<CandleBehaviour>().DealDamageToCandle(damageToCandle);
            Destroy(gameObject);
        }
        if (collision.tag == "Cursor")
        {
            if(abanico != null)
            {
                abanico.SetActive(true);
            }else
            abanico = Instantiate(abanicoPrefab, collision.transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cursor")
        {
            abanico.SetActive(false);
        }
    }

	public void StopHammerTime()
	{
		Destroy(this.gameObject);
	}
}
