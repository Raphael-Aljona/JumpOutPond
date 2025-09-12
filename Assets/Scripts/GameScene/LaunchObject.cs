using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LaunchObject : MonoBehaviour
{
    float speed = 3f;
    float spawnSpeed = 2f;
    public bool isRight = true;
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

            if (!isRight)
                obj.transform.rotation = Quaternion.Euler(180, 180, 270);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

            if (rb != null)
            {

                float direction = isRight ? 1 : -1;
                rb.linearVelocity = new Vector2(direction * speed, 0f);
            }

            StartCoroutine(DestroyCars(obj));

            yield return new WaitForSeconds(spawnSpeed);
        }

    }

    public IEnumerator DestroyCars(GameObject obj)
    {
        while (obj != null)
        {
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(obj.transform.position);

            if (viewportPosition.x < -1f || viewportPosition.x > 1.4 || viewportPosition.y < -1f || viewportPosition.y > 1)
            {
                Destroy(obj);
                break;
            }


            yield return null;
        }
    }

}
