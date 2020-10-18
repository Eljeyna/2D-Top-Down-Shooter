using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerController : MonoBehaviour
{
    public float speed = 4f;

    [Space(10)]
    public Dash dash;
    public Animator animations;
    public Rigidbody2D rb2d;
    public Camera cam;
    public BasePlayer thisPlayer;
    public Gun weapon;

    [Space(10)]
    public GameManager.State state;

    public float shakeForce = 2f;

    [HideInInspector] public Vector3 moving;
    [HideInInspector] public Vector2 moveVelocity;
    [HideInInspector] public Vector3 dashDirection;

    private NewInputSystem controls;

    private Vector2 mousePos;

    private bool attack;

    private void Awake()
    {
        controls = new NewInputSystem();

        controls.Player.Movement.performed += movementEvent => moving = movementEvent.ReadValue<Vector2>();
        controls.Player.Movement.canceled += movementEvent => moving = Vector3.zero;

        controls.Player.Attack.performed += attackEvent => attack = true;
        controls.Player.Attack.canceled += attackEvent => attack = false;

        GameManager.GetPlayer();
    }

    private void Update()
    {
        switch(state)
        {
            case GameManager.State.Normal:
                StateNormal();
                break;
            case GameManager.State.Dash:
                StateDash();
                break;
            case GameManager.State.Stun:
                StateStun();
                break;
        }
    }

    private void StateNormal()
    {
        mousePos = cam.ScreenToWorldPoint(Pointer.current.position.ReadValue());

        Vector2 lookDir = mousePos - rb2d.position;
        float angle = GameManager.GetAngleBetweenPoints(lookDir) - 90f;
        rb2d.rotation = angle;

        moveVelocity = moving.normalized * speed;

        if (moveVelocity != Vector2.zero)
        {
            rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
        }

        if (attack && weapon.nextAttack <= Time.time)
        {
            if (weapon.clip == 0)
            {
                weapon.fireWhenEmpty = true;
            }

            weapon.PrimaryAttack();
            CinemachineShaker.Instance.enabled = true;
            CinemachineShaker.Instance.ShakeSmooth(shakeForce, weapon.gunData.fireRatePrimary);
        }
    }

    private void StateDash()
    {
        if (dash.nextDashTime <= Time.time)
        {
            rb2d.velocity = Vector2.zero;
            state = GameManager.State.Normal;
        }
        else
        {
            float dashSpeed = dash.dashSpeed.Evaluate(dash.dashEvaluateTime);
            rb2d.velocity = dashDirection * dashSpeed;
            dash.dashEvaluateTime += Time.deltaTime;
        }
    }

    private void StateStun()
    {
        return;
    }

    private void OnReload()
    {
        if (!weapon.reloading)
        {
            weapon.Reload();
        }
    }

    private void OnDash()
    {
        if (dash.dashes > 0 && dash.dashTime <= Time.time)
        {
            dash.dashEvaluateTime = 0f;
            dashDirection = moving == Vector3.zero ? transform.up : moving.normalized;
            dash.enabled = true;
            dash.Use();
            state = GameManager.State.Dash;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
