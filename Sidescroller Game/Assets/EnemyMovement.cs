using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector2 forceFactors;

    public float speedMultiplier = 1;
    public float attackDelay = 1;
    public float attackRange = 1;

    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>().transform;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(forceFactors * direction * speedMultiplier);
    }

    private void Update()
    {
        float finalAngle = Mathf.Sin(Time.time * 20) * 5;
        transform.eulerAngles = new Vector3(0, 0, finalAngle); //Apply new angle to object
        
        sr.flipX = (player.transform.position.x < transform.position.x);
    }
}
