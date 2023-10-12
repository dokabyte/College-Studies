using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterControls characterControls;

    private Rigidbody2D rigidbody;

    private Vector2 movementDirection;

    private int numberOfJumps = 0;

    public AcornManager am;

    

    [HideInInspector] public int acornsCollected = 0;

    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxNumberOfJumps = 2;
    [SerializeField] private int numberOfAcornToWin = 4;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        characterControls = new CharacterControls();

        characterControls.Movement.Move.started += ReceiveMovePlayerInput;
        characterControls.Movement.Move.performed += ReceiveMovePlayerInput;
        characterControls.Movement.Move.canceled += ReceiveMovePlayerInput;

        characterControls.Movement.Jump.started += JumpPlayer;
    }

    private void Update()
    {
        MovePlayer();
        CheckVictory();
        FlipCharacter(); // Chame a função para inverter a escala do personagem
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        bool isJumpPressed = context.ReadValueAsButton();

        if (isJumpPressed && numberOfJumps < maxNumberOfJumps)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
            numberOfJumps++;
        }
    }

    private void MovePlayer()
    {
        transform.Translate(movementDirection * velocity * Time.deltaTime);
    }

    private void ReceiveMovePlayerInput(InputAction.CallbackContext context)
    {
        movementDirection.x = context.ReadValue<float>();
    }

    private void FlipCharacter()
    {
        // Inverta a escala do objeto com base na direção do movimento
        if (movementDirection.x < 0) // Se estiver se movendo para a esquerda
        {
            transform.localScale = new Vector3(-1, 1, 1); // Inverta a escala no eixo X
        }
        else if (movementDirection.x > 0) // Se estiver se movendo para a direita
        {
            transform.localScale = new Vector3(1, 1, 1); // Mantenha a escala original
        }
    }

    private void CheckVictory()
    {
        if (acornsCollected >= numberOfAcornToWin)
        {
            print("PARABÉNS! VOCÊ VENCEU!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Acorn")
        {
            Destroy(collision.gameObject);
            am.acornCount++;
        }
        
        numberOfJumps = 0;
    }

    private void OnEnable()
    {
        characterControls.Enable();
    }

    private void OnDisable()
    {
        characterControls.Disable();
        characterControls.Movement.Move.started -= ReceiveMovePlayerInput;
        characterControls.Movement.Move.performed -= ReceiveMovePlayerInput;
        characterControls.Movement.Move.canceled -= ReceiveMovePlayerInput;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Acorn"))
        {
            Destroy(other.gameObject);
            am.acornCount++;
        }
    }


}
