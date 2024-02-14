using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.CompareTo("Bullet") == 0)
        {
            GameObject.FindGameObjectWithTag("Range").GetComponent<ShootingRange>().IncrementScore(10);

            Destroy(collision.gameObject);
        }
    }
}
