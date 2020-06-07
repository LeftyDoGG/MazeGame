using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public PMove pmove;

    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (pmove.Cnum >= 3)
        {
            anim.Play("Win");
        }
    }
}
