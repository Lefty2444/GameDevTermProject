using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector2 forceFactors;

    public float points = 10;
    public float speedMultiplier = 1;
    public int health = 2;
    
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer sr;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>().transform;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(forceFactors * direction * speedMultiplier);
    }

    private void Update()
    {
        animator.SetFloat("speed", rb.velocity.magnitude);
        sr.flipX = (player.transform.position.x < transform.position.x);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                Score.instance.AddPoints(points);
            } else
            {
                StartCoroutine(Invulnerable(.3f));
            }
        }
    }
    IEnumerator Invulnerable(float time)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            sr.color = (Mathf.Sin(t * 25) > 0 ? Color.clear : Color.white);

            yield return null;
        }
        sr.color = Color.white;
    }
}
