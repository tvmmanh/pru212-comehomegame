using ComeHomeGame;
using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
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

    public Vector3 respawnPosition { get; set; }

    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {

        DataManager dataManager = DataManager.Instance;
        if (dataManager != null)
        {
            if (dataManager.Type == "Normal")
            {
                var (currentHealth, scalePlayer, jump, spd, dame, maxHealth) = dataManager.GetPlayerData();

                if (currentHealth != 0) this.currentHealth = currentHealth;
                else this.currentHealth = startingHealth;
            } else if (dataManager.Type == "Continue")
            {
                User user = dataManager.user;
                this.currentHealth = (float)user.currentHealth;
            }
            if(gameObject.tag == "Enemy") this.currentHealth=startingHealth;
        }



        anm = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        respawnPosition = transform.position; // Initial spawn position
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anm.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            if(hurtSound!=null) SoundManage.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anm.SetTrigger("die");

                foreach (Behaviour behave in components)
                {
                    behave.enabled = false;
                }

                dead = true;
               if(deathSound!=null) SoundManage.instance.PlaySound(deathSound);
                StartCoroutine(DieCoroutine());
            }
        }
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        Debug.Log("Die coroutine started");

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        anm.SetTrigger("die");
        dead = true;

        if(deathSound!=null) SoundManage.instance.PlaySound(deathSound);

        yield return new WaitForSeconds(deathSound.length);

        // Respawn
        Respawn();
    }

    public void Healing(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        Debug.Log("Respawning at position: " + respawnPosition);

        dead = false;
        currentHealth = startingHealth;
        transform.position = respawnPosition;

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        if (col != null)
        {
            col.enabled = true;
        }

        anm.ResetTrigger("die");
        anm.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour behave in components)
        {
            behave.enabled = true;
        }
    }

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

    public void SetRespawn()
    {
        respawnPosition = transform.position;
    }
    public float CurrentHealth()
    {
        return currentHealth;
    }
}
