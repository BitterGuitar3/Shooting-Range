using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject bowl;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float minX = -3.75f;
    private float maxX = 3.75f;
    private float bowlMinY = 0.1f;
    private float bowlMaxY = 1.75f;
    private float zPos;
    private float firstTargetY = 3.35f;
    private float secondTargetY = 0.5f;
    private float targetX = 3.75f;
    private int score = 0;
    private IEnumerator bowlSpawning;
    private IEnumerator targetSpawning;

    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z + 0.3f;
        bowlSpawning = SpawnBowls(1.0f);
        targetSpawning = SpawnTargets(1.0f);
        StartCoroutine(bowlSpawning);
        //StartCoroutine(targetSpawning);
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

    //A co-routine to spawn targets for the simulation while it is running
    private IEnumerator SpawnTargets(float spawnRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnTarget();
        }
    }

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

    //Spawn a Shooting Target
    private void SpawnTarget()
    {
        // Positive X position (right side of the shooting range)
        if (coinFlip() > 0)
        {
            // Positive Y position (top of the shooting range)
            if (coinFlip() > 0)
            {
                Instantiate(target, new Vector3(targetX, firstTargetY, zPos - .25f), Quaternion.Euler(-90, 0, -180));
            }
            // Negative Y position (bottom of the shooting range)
            else
            {
                Instantiate(target, new Vector3(targetX, secondTargetY, zPos - .25f), Quaternion.Euler(90, 0, 0));
            }
        }
        // Negative X position (left side of the shooting range)
        else
        {
            // Positive Y position (top of the shooting range)
            if (coinFlip() > 0)
            {
                Instantiate(target, new Vector3(-targetX, firstTargetY, zPos - .25f), Quaternion.Euler(-90, 0, -180));
            }
            // Negative Y position (bottom of the shooting range)
            else
            {
                Instantiate(target, new Vector3(-targetX, secondTargetY, zPos - .25f), Quaternion.Euler(90, 0, 0));
            }
        }
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

    //Just a 50/50 coin flip method to chose between 1 option or the other
    private int coinFlip()
    {
        int[] choices = {-1, 1};
        int howManyChoices = choices.Length;
        int myRandomIndex = Random.Range(0, howManyChoices);
        return choices[myRandomIndex];
    }

    public void IncrementScore(int hitScore)
    {
        score += hitScore;
        scoreText.text = "Score: " + score;
    }

    //Destory objects that fall out of world
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.CompareTo("Target") == 0)
        {
            Debug.Log("Target Detected");
        }
        Destroy(other.gameObject);
    }
}
