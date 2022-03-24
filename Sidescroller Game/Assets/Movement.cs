using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 forceFactors;
    public float sizeVariance;

    public float topY;
    public float bottomY;

    private Rigidbody2D rb;
    private Vector2 scale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }

    
    void FixedUpdate()
    {
        Vector2 force = new Vector2(Input.GetAxis("Horizontal") * forceFactors.x, Input.GetAxis("Vertical") * forceFactors.y);
        rb.AddForce(force);
    }

    private void Update()
    {
        transform.localScale = scale * Mathf.Lerp(1 + sizeVariance, 1 - sizeVariance, (transform.position.y - bottomY) / (topY - bottomY));
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float finalAngle =  Mathf.Sin(Time.time * 20) * 5;
            transform.eulerAngles = new Vector3(0, 0, finalAngle); //Apply new angle to object
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
