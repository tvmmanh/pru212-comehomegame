using ComeHomeGame;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth
    {
        get; 
        private set;
    }
    private Animator anm;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anm = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anm.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anm.SetTrigger("dead");
                GetComponent<PlayerController>().enabled = false;
                dead = true;
            }
        }
    }



    public void Healing(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
