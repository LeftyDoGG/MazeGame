using UnityEngine;
using System.Collections;

public class EMove : MonoBehaviour
{
    PHealth playerHealth;
    EHealth enemyHealth;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update()
    {
        nav.SetDestination(player.position);
    }
}

