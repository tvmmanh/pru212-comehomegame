using ComeHomeGame;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth
    {
        get; 
        private set;
    }
    private Animator anm;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRender;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    
    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anm = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anm.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManage.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anm.SetTrigger("die");

                /*  
                //Player
                if(GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().enabled = false;
                }

                //Enemy
                if(GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponent<DemonEnemy>() != null)
                {
                    GetComponent<DemonEnemy>().enabled = false;
                } 
                */

                foreach (Behaviour behave in components)
                {
                    behave.enabled = false;
                }

                dead = true;

                SoundManage.instance.PlaySound(deathSound);
            }
        }
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        Debug.Log("die nef");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        anm.SetTrigger("die");
        dead = true;

        SoundManage.instance.PlaySound(deathSound);

        yield return new WaitForSeconds(deathSound.length);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void Healing(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //public void Respawn()
    //{
    //    dead = false;
    //    Healing(startingHealth);
    //    anm.ResetTrigger("die");
    //    anm.Play("Idle");
    //    StartCoroutine(Invunerability());

    //    foreach (Behaviour behave in components)
    //    {
    //        behave.enabled = true;
    //    }
    //}

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
