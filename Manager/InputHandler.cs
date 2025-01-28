using UnityEngine;
using UnityEngine.InputSystem; // Fix typo in the namespace

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputAction walkAction;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private InputAction slideAction;
    [SerializeField] private InputAction punchAction;
    [SerializeField] private InputAction bubbleAction;
    [SerializeField] private InputAction harvestFatAction;
    private EnemyHealth enemy;
    public EnemyHealth GetEnemyHealth() => enemy;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool PunchPressed { get; private set; }
    public bool SlidePressed { get; private set; }
    public bool BubblePressed { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool HarvestFatPressed { get; private set; }

    void Awake()
    {
        walkAction.performed += ctx => MoveInput = new Vector2(ctx.ReadValue<float>(), 0f);
        walkAction.canceled += ctx => MoveInput = Vector2.zero;

        jumpAction.performed += ctx => JumpPressed = true;
        jumpAction.canceled += ctx => JumpPressed = false;

        slideAction.performed += ctx => SlidePressed = true;
        slideAction.canceled += ctx => SlidePressed = false;

        punchAction.performed += ctx => PunchPressed = true;
        punchAction.canceled += ctx => PunchPressed = false;

        bubbleAction.performed += ctx => BubblePressed = true;
        bubbleAction.canceled += ctx => BubblePressed = false;

        harvestFatAction.performed += ctx => HarvestFatPressed = true;
        harvestFatAction.canceled += ctx => HarvestFatPressed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            IsGrounded = true;
        }
        if (other.tag == "Enemy")
        {
            enemy = other.GetComponent<EnemyHealth>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            IsGrounded = false;
        }
        if (other.tag == "Enemy")
        {
            enemy = null;
        }
    }

    void OnEnable()
    {
        walkAction.Enable();
        jumpAction.Enable();
        slideAction.Enable();
        punchAction.Enable();
        bubbleAction.Enable();
        harvestFatAction.Enable();
    }

    void OnDisable()
    {
        walkAction.Disable();
        jumpAction.Disable();
        slideAction.Disable();
        punchAction.Disable();
        bubbleAction.Disable();
        harvestFatAction.Disable();
    }
}
