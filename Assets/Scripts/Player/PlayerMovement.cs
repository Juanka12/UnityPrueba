using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Camera cam;
    private MouseLook mouseLook;

    public Animator playerAnimatorController;

    public SelectObject objectSelector;
    public SelectSpell spellSelector;

    public Inventory inventory;
    public Inventory equipment;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        mouseLook = cam.GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inventory.Load();
            equipment.Load();
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * xMove + transform.forward * zMove;

        playerAnimatorController.SetFloat("walkSpeed", movement.magnitude * speed);

        controller.Move(movement * speed * Time.deltaTime);

        playerAnimatorController.SetBool("isGrounded", isGrounded);
        playerAnimatorController.SetFloat("verticalSpeed", velocity.y);

        jump();
        attack();
        useItem();
        interact();
        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.money.AddMoney(100);
        }
    }
 
    private void useItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            objectSelector.GetItem().Use();
        }
    }
    private void interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable!=null)
                {
                    interactable.OnFocused(transform, inventory);
                }
            }
        }
    }

    private void attack()
    {
        if (Input.GetButtonDown("Attack") && isGrounded && mouseLook.isCursorLocked())
        {
            playerAnimatorController.SetBool("isAttack", true);

            GameObject gameObject = Instantiate(spellSelector.GetSpell().spellPrefab, transform.position, cam.GetComponent<Transform>().rotation);
        }
        else if (Input.GetButtonUp("Attack"))
        {
            playerAnimatorController.SetBool("isAttack", false);
        }
    }

    private void jump()
    {
        if (Input.GetButtonDown("Jump")&& isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerAnimatorController.SetTrigger("jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnApplicationQuit(){
        inventory.Clear();
        equipment.Clear();
    }
}

