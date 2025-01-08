using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    public float health = 10f;
    public float startingHealth = 10f;
    public Image healthImage;
    public Animator anim;
    public bool isDead;

    public Vector2 movementInput;

    public float walkSpeed;
    public float runSpeed;

    public bool isSprinting;

    public Transform cameraTransform;

    public Rigidbody playerRigidbody;
    public CapsuleCollider playerCollider;
    public LayerMask groundLayer;

    public float jumpForce;


    public float buildRadius;

    public bool buildMode;
    public bool canBuild;

    public Vector3 buildPos;

    public GameObject templateOutlineBlock;
    public GameObject blockTemplatePrefab;

    public CharacterInventory cI;

    public bool fps;

    // Current Block
    public GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        cI = GetComponent<CharacterInventory>();
        if (!fps)
        {
            anim = GetComponent<Animator>();
        }

        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = health / startingHealth;

        if(isDead == false)
        {
            Movements();
            BuildBlock();
        }

        anim.SetBool("isDead", isDead);

        if(health <= 0)
        {
            health = 0.0f;
            isDead = true;
        }

        if(health >= 0.01f)
        {
            isDead = false;
        }

        //1/8/25: Added Code to Go Back To Menu.
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(0);
        
    }

    public void Movements()
    {
        if (!fps)
        {

            // Walking and Rotating
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float movement = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementInput = Vector2.ClampMagnitude(movementInput, 1f);

            Vector3 camF = cameraTransform.forward;
            Vector3 camR = cameraTransform.right;

            camF.y = 0;
            camR.y = 0;
            camF = camF.normalized;
            camR = camR.normalized;

            transform.position += (camF * movementInput.y + camR * movementInput.x) * Time.deltaTime * walkSpeed;
            anim.SetFloat("Speed", movement);


            if (movement != 0)
            {
                Vector3 targetDirection = new Vector3(vertical, 0f, -horizontal);
                targetDirection = cameraTransform.TransformDirection(targetDirection);
                targetDirection.y = 0.0f;

                Quaternion newRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, newRotation, Time.deltaTime * 20f);
            }
        }

        if (fps)
        {
            // Walking and Rotating
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            movementInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementInput = Vector3.ClampMagnitude(movementInput, 1f);

            Vector3 camF = cameraTransform.forward;
            Vector3 camR = cameraTransform.right;

            camF.y = 0;
            camR.y = 0;
            camF = camF.normalized;
            camR = camR.normalized;

            transform.position += (camF * movementInput.y + camR * movementInput.x) * Time.deltaTime * walkSpeed;
           // anim.SetFloat("MovementX", horizontal);
           // anim.SetFloat("MovementY", vertical);

            if(Mathf.Abs(horizontal) + Mathf.Abs(vertical) != 0)
            {
                anim.SetBool("Moving", true);
            }

            else
            {
                anim.SetBool("Moving", false);
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Jumping

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        bool isGrounded()
        {
            return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y, playerCollider.bounds.center.z), playerCollider.radius * 0.5f, groundLayer);
        }
    }
    public void BuildBlock()
    {
        if(block != null)
        {
            buildMode = true;
            cI.currentHandBlock.SetActive(true);
            cI.pickAxeWooden.SetActive(false);
        }

        if(block == null)
        {
            buildMode = false;
            cI.currentHandBlock.SetActive(false);
        }

        if (cI.pickWoodActive)
        {
            cI.pickAxeWooden.SetActive(true);
        }

        else
        {
            cI.pickAxeWooden.SetActive(false);
        }

        if (buildMode)
        {
            RaycastHit buildPosHit;

            if(Physics.Raycast(cameraTransform.gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out buildPosHit, buildRadius, groundLayer))
            {
                Vector3 point = buildPosHit.point;
                buildPos = new Vector3(Mathf.Round(point.x), Mathf.Round(point.y), Mathf.Round(point.z));
                canBuild = true;
            }
            else
            {
                Destroy(templateOutlineBlock);
                canBuild = false;
            }
        }

        if(!buildMode && templateOutlineBlock != null)
        {
            Destroy(templateOutlineBlock);
            canBuild = false;
        }

        if(canBuild && templateOutlineBlock == null)
        {
            templateOutlineBlock = Instantiate(blockTemplatePrefab, buildPos, Quaternion.identity);

        }

        if(canBuild && templateOutlineBlock != null)
        {
            templateOutlineBlock.transform.position = buildPos;

            if (Input.GetButtonDown("Build"))
            {
                BuildingBlock();
            }
        }
    }

    public void BuildingBlock()
    {
        GameObject newBlock = Instantiate(block, buildPos, Quaternion.identity);
        cI.takeAwayPlacedBlocks();
    }
}



