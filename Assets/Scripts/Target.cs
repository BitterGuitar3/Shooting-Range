using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int direction;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (this.gameObject.transform.position.x > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * direction * Time.deltaTime, Space.World);
    }

    //When bullet hits, play animation and THEN delete the target and up the player's score
    //REPPLACE WITH RAYCASTING INSTEAD TO MAYBE GET A MORE ACCURATE HIT INSTEAD OF COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.CompareTo("Bullet") == 0)
        {
            if(this.transform.position.y > 1.5f)
            {
                animator.SetBool("TopShot", true);
            }
            else
            {
                animator.SetBool("BottomShot", true);
            }

            GameObject.FindGameObjectWithTag("Range").GetComponent<ShootingRange>().IncrementScore(2);

            Destroy(collision.gameObject);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
