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
    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cout > 0.2f)
        {
            agent.SetDestination(player.position);
            cout = 0f;
        }
        cout += Time.deltaTime;
    }
}
