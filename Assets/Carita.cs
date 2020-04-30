using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carita : MonoBehaviour
{

    CandleBehaviour cb;
    Animator anim;
    private void Awake()
    {
        cb = transform.root.GetComponent<CandleBehaviour>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cb.LifePercentage <= 0.5f)
        {
            anim.SetInteger("State", 4);
        }
        else if (cb.LifePercentage <= 0.75f )
        {
            anim.SetInteger("State", 3);
        }
        else if( cb.LifePercentage <= 0.9f)
        {
            anim.SetInteger("State", 0);
        }
        else
        {
            anim.SetInteger("State", 1);
        }




    }
}
