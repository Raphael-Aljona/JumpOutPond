using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float maxJumpDistance = 5f;   
    public float chargeSpeed = 3f;        
    public float currentForce = 0f;      
    private bool charging = false;

    private bool isJumping = false;
    public float jumpDuration = 0.5f;

    public Vector3 startPosition;

    public Slider powerSlider;

    void Start()
    {
        //startPosition = transform.position;
    }

    void Update()
    {

        //Movimento para cima
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            charging = true;
            currentForce = 0f;
        }

        if (Input.GetKey(KeyCode.UpArrow) && charging) { 
            currentForce += chargeSpeed * Time.deltaTime;
            if (currentForce > maxJumpDistance) currentForce = maxJumpDistance;

            if (powerSlider != null)
            {
                powerSlider.value = currentForce / maxJumpDistance;
                //Debug.Log(powerSlider.value);
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && charging) {
            charging = false;
            if (!isJumping)
            {
                StartCoroutine(Move(Vector3.up, currentForce));
            }

            if (powerSlider )
            {
                powerSlider.value = 0;
            }
        }

        //Movimento para baixo
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            charging = true;
            currentForce = 0f;
        }

        if (Input.GetKey(KeyCode.DownArrow) && charging) { 
            currentForce += chargeSpeed * Time.deltaTime;
            if (currentForce > maxJumpDistance) currentForce = maxJumpDistance;


            if (powerSlider != null)
            {
                powerSlider.value = currentForce / maxJumpDistance;
                //Debug.Log(powerSlider.value);
            }

        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && charging) {
            charging = false;
            if (!isJumping)
            {
                StartCoroutine(Move(Vector3.down, currentForce));

            }
            if (powerSlider)
            {
                powerSlider.value = 0;
            }
        }

        //Movimento para direita
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            charging = true;
            currentForce = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && charging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            if (currentForce > maxJumpDistance) currentForce = maxJumpDistance;


            if (powerSlider != null)
            {
                powerSlider.value = currentForce / maxJumpDistance;
                //Debug.Log(powerSlider.value);
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && charging)
        {
            charging = false;
            if (!isJumping)
            {
                StartCoroutine(Move(Vector3.right, currentForce));

            }
            if (powerSlider)
            {
                powerSlider.value = 0;
            }
        }

        //Movimento para esquerda
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            charging = true;
            currentForce = 0f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && charging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            if (currentForce > maxJumpDistance) currentForce = maxJumpDistance;

            if (powerSlider != null)
            {
                powerSlider.value = currentForce / maxJumpDistance;
                //Debug.Log(powerSlider.value);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && charging)
        {
            charging = false;
            if (!isJumping) {
                StartCoroutine(Move(Vector3.left, currentForce));

            }
            if (powerSlider)
            {
                powerSlider.value = 0;
            }
        }
    }

    IEnumerator Move(Vector3 direction, float force)
    {
        isJumping = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + direction * force;
        float elapsed = 0f;

        while (elapsed < jumpDuration) { 
            transform.position = Vector3.Lerp(startPos, endPos, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isJumping = false;

    }
}
