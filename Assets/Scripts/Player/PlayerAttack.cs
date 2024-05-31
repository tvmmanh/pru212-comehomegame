using ComeHomeGame;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //code sai logic - nen coi lai

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] firePunchs;
    private Animator anm;
    private PlayerController playerMovement;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anm = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anm.SetTrigger("attack");
        coolDownTimer = 0;

        firePunchs[FindFirePunch()].transform.position = firePoint.position;
        firePunchs[FindFirePunch()].GetComponent<ProjectTile>().SetDirection(Mathf.Sign(-transform.localScale.x));
    }

    private int FindFirePunch()
    {
        for (int i = 0; i < firePunchs.Length; i++)
        {
            if (!firePunchs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}