using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    public PlayerInput playerInput;
    public Transform transform;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2d;
    public CapsuleCollider2D collider2d;
    public AListener listener;
    public PlayerStats playerStats;

    public AudioSource audioSource;
    public AudioClip audioClip_run;
    public AudioClip audioClip_jump;
    public AudioClip audioClip_climb;
    public AudioClip audioClip_death;

    public int horizontalVelocityFactor = 100;
    public int maxHorizontalVelocity = 8;
    public float airborneHorizontalFactor = 0.5f;
    public int verticalVelocityFactor = 2000;

    private Vector2 movementValue;
    private Vector2 horizontalDirection;
    private Vector2 verticalDirection;
    private bool canJump = false;
    private bool climbing = false;
    private bool running = false;

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>();
        horizontalDirection = new Vector2(movementValue.x, 0f);
        verticalDirection = new Vector2(0f, Mathf.Ceil(movementValue.y));
        canJump = verticalDirection != Vector2.zero && collider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // facing direction
        switch (movementValue.x)
        {
            case -1f:
                spriteRenderer.flipX = false;
                break;
            case 1f:
                spriteRenderer.flipX = true;
                break;
        }

        animator.SetBool("jumping", canJump);
    }

    public void OnInteract()
    {
        List<GameObject> _objs = listener.GetInteractiveObjects();

        foreach(GameObject obj in _objs)
        {
            if (obj == null) continue;

            switch(obj.tag)
            {
                case "ladder":
                    ToggleClimb();
                    return;
                case "oxygen":
                    playerStats.AddOxygen(obj.GetComponent<IO_OxygenTank>().oxygenAmount);
                    _objs.Remove(obj);
                    Destroy(obj, 0.2f);
                    return;
                case "energyCell":
                    playerStats.AddInventory(obj);
                    Destroy(obj, 0.2f);
                    return;
                case "cog":
                    playerStats.AddInventory(obj);
                    Destroy(obj, 0.2f);
                    return;
                case "console":
                    obj.GetComponent<Console>().AddItems(playerStats.inventory);
                    return;
                case "letter":
                    playerStats.AddInventory(obj);
                    Destroy(obj, 0.2f);
                    return;
                default:
                    Debug.Log("No handling for tag: " + obj.tag);
                    return;
            }
        }
    }

    public void ToggleClimb()
    {
        if (!climbing && collider2d.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            climbing = true;
            rb2d.velocity *= 0f;
            rb2d.isKinematic = climbing;
            float _ladderX = listener.GetInteractiveObjects().Find(obj => obj.tag == "ladder").transform.position.x;
            transform.position = new Vector3(_ladderX, transform.position.y, transform.position.z);
        }
        else if (climbing)
        {
            climbing = false;
            rb2d.isKinematic = climbing;
            animator.speed = 1;
        }

        animator.SetBool("climbing", climbing);
    }

    public void ToggleRun(bool enabled)
    {
        // run animation and sound
        if (!running && enabled)
        {
            animator.SetFloat("speed", 1);
            audioSource.clip = audioClip_run;
            audioSource.loop = true;
            audioSource.Play();
            running = true;
        }
        else if (running && !enabled)
        {
            animator.SetFloat("speed", 0);
            audioSource.Stop();
            running = false;
        }
    }

    public void PlaySoundDeath()
    {
        audioSource.clip = audioClip_death;
        audioSource.loop = false;
        audioSource.Play();
    }

    void FixedUpdate()
    {
        float x = 0;
        float y = 0;
        // Add Horizontal Momentum - player is touching ground
        if (horizontalDirection != Vector2.zero && Mathf.Abs(rb2d.velocity.x) < maxHorizontalVelocity && collider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            x = horizontalDirection.x * horizontalVelocityFactor * Time.fixedDeltaTime;
            ToggleRun(true);
        }
        // Control in air - player is not touching ground
        else if (horizontalDirection != Vector2.zero && Mathf.Abs(rb2d.velocity.x) < maxHorizontalVelocity && !collider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            x = horizontalDirection.x * horizontalVelocityFactor * airborneHorizontalFactor * Time.fixedDeltaTime;
            ToggleRun(false);
        } 
        else if (horizontalDirection == Vector2.zero)
        {
            ToggleRun(false);
        }

        // Add Vertical Momentum
        if (canJump && !climbing)
        {
            y = (verticalDirection.y * verticalVelocityFactor * Time.fixedDeltaTime);
            ToggleRun(false);
            canJump = false;
        }

        // Climb
        if (horizontalDirection != Vector2.zero && climbing)
        {
            ToggleRun(false);
            ToggleClimb();
        }

        // AddForce using result momentum
        if (!rb2d.isKinematic)
        {
            rb2d.AddForce(new Vector2(x, y));
        }
    }

    void Update()
    {
        if (climbing)
        {
            transform.position += new Vector3(0, verticalDirection.y * Time.deltaTime * 3, 0);
            animator.speed = Mathf.Abs(verticalDirection.y);
        }
    }
}
