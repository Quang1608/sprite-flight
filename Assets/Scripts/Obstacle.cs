using Unity.Mathematics;
using UnityEngine;

using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float minSpeed = 125f;
    public float maxSpeed = 300f;
    public float maxSpinSpeed = 10f;

    public GameObject bounceEffectPrefab;
    Rigidbody2D rb;
    Collider2D collide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomSize = Random.Range(minSize, maxSize);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        Vector2 randomDirection = Random.insideUnitCircle;

        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        float otherSpeed = collision.rigidbody.linearVelocity.magnitude;
        float curSpeed = rb.linearVelocity.magnitude;
        // Debug.Log("functioned");
        // Debug.Log(otherSpeed + " " + curSpeed);
        if (otherSpeed < curSpeed)
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity);

            Destroy(bounceEffect, 1f);
        }
    }
}
