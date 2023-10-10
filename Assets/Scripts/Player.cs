using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{
    public PhotonView MyPhotonView;
    public Rigidbody rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public Renderer myRenderer; // Changed to Renderer for 3D
    public Text PlayerNameText;
    // public TMPro.TextMeshProUGUI PlayerNameText;

    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce; // Added for jumping

    private CapsuleCollider playerHeadCollider; // Changed to CapsuleCollider for 3D

    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerNameText.text = photonView.owner.NickName;
            PlayerNameText.color = Color.cyan;
        }

        playerHeadCollider = GetComponent<CapsuleCollider>(); // Changed to CapsuleCollider for 3D
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        // Get input for WASD keys
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys
        float verticalInput = Input.GetAxis("Vertical");     // W and S keys

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput); // Use 3D vector

        // Normalize the move vector to prevent faster diagonal movement
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        // Move the player using Rigidbody (apply force)
        rb.velocity = move * MoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            // Jump using Rigidbody (apply upward force)
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsGrounded = false;
        }

        // Handle animation (you may need to set animator parameters)
        if (move.magnitude > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Handle flipping the player's sprite (or model) based on input
        if (move.x < 0)
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }
        else if (move.x > 0)
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }

        // Handle other actions as needed (e.g., shooting)
    }

    [PunRPC]
    private void FlipTrue()
    {
        // Adjust the player's rotation or sprite flipping as needed
        transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate 180 degrees around the Y-axis
    }

    [PunRPC]
    private void FlipFalse()
    {
        // Reset the player's rotation or sprite flipping as needed
        transform.rotation = Quaternion.identity; // Reset rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
}
