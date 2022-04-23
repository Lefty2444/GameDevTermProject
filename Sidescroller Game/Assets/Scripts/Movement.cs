using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public int hearts = 3;
    public Vector2 forceFactors;

    public float damageDelay = .5f;

    public float speedMultiplier = 1;

    public Image heartImage;

    private Animator animator;
    private bool invincible = false;
    private int maxHearts = 5;
    private Vector2 startPos;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Quaternion normal = Quaternion.identity;
    private Quaternion flipped = Quaternion.Euler(0, 180, 0);

    public static Movement player;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        maxHearts = hearts;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Spawn();
    }
    private void Awake()
    {
        player = this;
    }

    void Spawn()
    {
        hearts = maxHearts;
        transform.position = startPos;
        UpdateHearts();
    }

    void FixedUpdate()
    {
        Vector2 force = new Vector2(Input.GetAxis("Horizontal") * forceFactors.x, Input.GetAxis("Vertical") * forceFactors.y);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("punch"))
            rb.AddForce(force * speedMultiplier / 4);
        else
            rb.AddForce(force * speedMultiplier);
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = (Input.GetAxis("Horizontal") < 0 ? flipped : normal);
            animator.SetFloat("speed", rb.velocity.magnitude);
        } else
        {
            animator.SetFloat("speed", 0);
        }
        if (Input.GetKey("space"))
        {
            animator.SetTrigger("attack");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("punch")) {
            animator.ResetTrigger("attack");
        }
    }

    public void UpdateHearts()
    {
        RectTransform rt = heartImage.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(200 * hearts, 200);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincible)
        {
            hearts--;
            UpdateHearts();
            invincible = true;
            StopAllCoroutines();
            StartCoroutine(Invulnerable(damageDelay));
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincible)
        {
            hearts--;
            UpdateHearts();
            invincible = true;
            StopAllCoroutines();
            StartCoroutine(Invulnerable(damageDelay));
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
        invincible = false;
    }

}
