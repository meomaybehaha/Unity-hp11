using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class BulletScript : MonoBehaviour
{
    private float count = 3;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            Destroy(gameObject);
        }
            
    }
}
