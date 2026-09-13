using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    
    public NavMeshAgent agent;
    
    public float cout = 0;
    
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
            health -= other.gameObject.GetComponent<BulletScript>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (cout > 0.2f)
        {
            if ((player.position - transform.position).magnitude > 5f)
            {
                agent.SetDestination(player.position);
                agent.speed = 15;
            }
            else
            {
                agent.speed = 0;
            }
            
            cout = 0f;
        }
        cout += Time.deltaTime;
    }
}
