using UnityEngine;

public class OpositeEnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anm;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anm.SetBool("walking", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x <= leftEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x >= rightEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        anm.SetBool("walking", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        anm.SetBool("walking", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction * (-1), initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
