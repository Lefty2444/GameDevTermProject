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
    public float attackDelay = 1;
    public float attackRange = 1;

    public Image heartImage;

    private bool invincible = false;
    private int maxHearts = 5;
    private Vector2 startPos;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        maxHearts = hearts;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Spawn();
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
        rb.AddForce(force * speedMultiplier);
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            sr.flipX = Input.GetAxis("Horizontal") < 0;

            float finalAngle =  Mathf.Sin(Time.time * 20) * 5;
            transform.eulerAngles = new Vector3(0, 0, finalAngle); //Apply new angle to object
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void UpdateHearts()
    {
        RectTransform rt = heartImage.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(200 * hearts, 200);
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
            sr.color = (Mathf.Sin(t * 25) > 0 ? Color.clear : Color.black);

            yield return null;
        }
        sr.color = Color.black;
        invincible = false;
    }

}
