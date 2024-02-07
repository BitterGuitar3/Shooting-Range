using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject bowl;

    private float minX = -3.75f;
    private float maxX = 3.75f;
    private float bowlMinY = 0.1f;
    private float bowlMaxY = 1.75f;
    private float zPos;
    private IEnumerator bowlSpawning;
    //private IEnumerator targetSpawning;

    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z + 0.3f;
        bowlSpawning = SpawnBowls(1.0f);
        //targetSpawning = SpawnTargets(1.0f);
        StartCoroutine(bowlSpawning);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //A co-routine to spawn the bowls for the simulation while it is running
    private IEnumerator SpawnBowls(float spawnRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnBowl();
        }
    }

    /*private IEnumerator SpawnTargets(float spawnRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnTarget();
            break;
        }
    }*/

    //Spawn a bowl with appropriate force and torque
    private void SpawnBowl()
    {
        Vector3 rotationForce = GenerateRandomTorque();
        float spawnX = Random.Range(minX, maxX);
        float spawnY = bowlMinY;
        float xMultiplier, yMultiplier;
        Vector3 force;

        if (Mathf.Abs(spawnX) > 3.2f)
        {
            spawnY = Random.Range(bowlMinY, bowlMaxY);
            xMultiplier = 1.0f;
            yMultiplier = spawnY < 1 ? 0.9f : 0.5f;
        }
        else if (Mathf.Abs(spawnX) > 1.5f)
        {
            xMultiplier = 0.5f;
            yMultiplier = 1.1f;
        }
        else
        {
            xMultiplier = 0.0f;
            yMultiplier = 1.1f;
            force = GenerateRandomForce(xMultiplier, yMultiplier);
        }

        if (spawnX > 0)
        {
            force = GenerateRandomForce(-xMultiplier, yMultiplier);
        }
        else
        {
            force = GenerateRandomForce(xMultiplier, yMultiplier);
        }


        var instance = Instantiate(bowl, new Vector3(spawnX, spawnY, zPos), Quaternion.identity).GetComponent<Rigidbody>();
        instance.AddForce(force, ForceMode.Impulse);
        instance.AddTorque(rotationForce, ForceMode.Impulse);

    }

    //Generate a random torque value
    private Vector3 GenerateRandomTorque()
    {
        float x = Random.Range(5, 30) * Time.deltaTime;
        float y = Random.Range(5, 30) * Time.deltaTime;
        float z = Random.Range(5, 30) * Time.deltaTime;
        return new Vector3(x, y, z);
    }

    //Generate a random force value
    private Vector3 GenerateRandomForce(float xMultiplier, float yMultiplier)
    {
        float x = Random.Range(3, 7);
        float y = Random.Range(5, 7);
        return new Vector3 (x * xMultiplier, y * yMultiplier, 0);
    }

    //Destory objects that fall out of world
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
