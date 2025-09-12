using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Camera camera;
    public int score;

    public Text scoreText;
    public float highCamera;

    public bool isAdding = false;

    void Update()
    {
        if (camera.transform.position.y > highCamera)
        {
            highCamera = camera.transform.position.y;
            if (isAdding == false)
            {
                StartCoroutine(AddPoints());
            }
            //Debug.Log(score);
        }
    }

    public IEnumerator AddPoints()
    {
        isAdding = true;

        while (true) {
            score += 100;
            UpdateTextUI();
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    void UpdateTextUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
