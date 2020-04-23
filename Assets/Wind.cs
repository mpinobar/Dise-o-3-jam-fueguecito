using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : ApproachingDanger
{
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
            currentHP -= collision.GetComponent<Cursor>().velocity.magnitude * (1 - (armadura * 0.01f));
            if(currentHP > 0)
            {
                Color c = spr.color;
                c.a = currentHP / windHP;
                spr.color = c;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
