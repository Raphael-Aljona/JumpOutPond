using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f;

    public GameObject painelGameOver;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y - 5.5f > player.transform.position.y)
        {
            Debug.Log("ta morto");
            Destroy(player);

            painelGameOver.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
