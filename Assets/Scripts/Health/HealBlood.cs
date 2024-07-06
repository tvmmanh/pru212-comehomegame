using UnityEngine;

public class HealBlood : MonoBehaviour
{
    [SerializeField] private float healValue;
    [SerializeField] private AudioClip healingSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManage.instance.PlaySound(healingSound);
            collision.GetComponent<Health>().Healing(healValue);
            gameObject.SetActive(false);
        }
    }
    public float HealthValue()
    {
        return healValue;
    }
}
