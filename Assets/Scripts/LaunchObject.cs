using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class LaunchObject : MonoBehaviour
{
    float speed = 3f;
    float spawnSpeed = 2f;
    private bool isRight = true;
    public Transform spawnPoint;
    public GameObject[] objectLaunch;

    public void Start()
    {
        StartCoroutine(LaunchObjects());
    }

    private IEnumerator LaunchObjects()
    {

        while (true) 
        {

            int randomIndex = Random.Range(0, objectLaunch.Length);
            GameObject carSelect = objectLaunch[randomIndex];

            GameObject obj = Instantiate(carSelect, spawnPoint.position, carSelect.transform.rotation);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

            if (rb != null) { 
                
                float direction = isRight ? 1 : -1;
                rb.linearVelocity = new Vector2 (direction * speed, 0f);
            }

            yield return new WaitForSeconds(spawnSpeed);
        }

    }

}
