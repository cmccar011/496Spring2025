using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    private Rigidbody rb;
    private Animator anim;


    public bool gameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }


    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
    {
    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    isOnGround = false;
    anim.SetTrigger("Jump_trig");
    }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
        isOnGround = true;
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            
        }
    }
}
