using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float maxJumpDistance = 5f;
    public float chargeSpeed = 3f;
    public float currentForce = 0f;
    private bool charging = false;

    private bool isJumping = false;
    public float jumpDuration = 0.5f;

    public Slider powerSlider;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isJumping", false);
        animator.SetInteger("direction", 0); 
    }

    void Update()
    {
        // Testa os 4 inputs
        MovimentInput(KeyCode.UpArrow, Vector3.up, 0);
        MovimentInput(KeyCode.DownArrow, Vector3.down, 1);
        MovimentInput(KeyCode.RightArrow, Vector3.right, 2);
        MovimentInput(KeyCode.LeftArrow, Vector3.left, 3);
    }

    void MovimentInput(KeyCode key, Vector3 dir, int directionIndex)
    {
        if (Input.GetKeyDown(key))
        {
            charging = true;
            currentForce = 0f;

            // seta direção no Animator (Idle muda)
            animator.SetInteger("direction", directionIndex);
        }

        if (Input.GetKey(key) && charging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            if (currentForce > maxJumpDistance) currentForce = maxJumpDistance;

            if (powerSlider != null)
                powerSlider.value = currentForce / maxJumpDistance;
        }

        if (Input.GetKeyUp(key) && charging)
        {
            charging = false;
            if (!isJumping)
            {
                StartCoroutine(Move(dir, currentForce, directionIndex));
            }

            if (powerSlider) powerSlider.value = 0;
        }
    }

    IEnumerator Move(Vector3 direction, float force, int dirIndex)
    {
        isJumping = true;
        animator.SetBool("isJumping", true);
        animator.SetInteger("direction", dirIndex);

        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + direction * force;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / jumpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        isJumping = false;
        animator.SetBool("isJumping", false);
        animator.SetInteger("direction", dirIndex);
    }

}
