using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public EnemyData soldierData;

    [Space(10)]
    public Rigidbody2D rb2d;
    public BaseEnemy thisEnemy;
    public Gun weapon;

    [Space(10)]
    public GameManager.State state;

    [HideInInspector] public Vector2 moveVelocity;

    private void Start()
    {
        soldierData.ignoreLength = Mathf.Min(soldierData.ignoreLength, weapon.gunData.range);
    }

    private void Update()
    {
        switch (state)
        {
            case GameManager.State.Normal:
                StateNormal();
                break;
            case GameManager.State.Stun:
                StateStun();
                break;
            default:
                break;
        }
    }

    private void StateNormal()
    {
        Vector2 lookDir = GameManager.player.position - rb2d.position;
        float angle = GameManager.GetAngleBetweenPoints(lookDir) - 90f;
        rb2d.rotation = angle;

        float distance = Vector2.Distance(GameManager.player.position, rb2d.position);

        if (distance > soldierData.ignoreLength)
        {
            moveVelocity = transform.up * soldierData.speed;
            rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
        }
        
        if (distance <= weapon.gunData.range && weapon.nextAttack <= Time.time)
        {
            if (weapon.clip == 0)
            {
                weapon.fireWhenEmpty = true;
            }

            weapon.PrimaryAttack();
        }
    }

    private void StateStun()
    {
        return;
    }
}
