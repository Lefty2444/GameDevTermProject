using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public float score = 0;
    public TMP_Text text;
    public static Score instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        text = GetComponent<TMP_Text>();
        UpdateScore();
    }

    
    void UpdateScore()
    {
        text.text = score.ToString();
    }

    public void AddPoints(float points)
    {
        score += points;
        UpdateScore();
    }
}
