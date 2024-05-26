using UnityEngine;

public class HealBlood : MonoBehaviour
{
    [SerializeField] private float healValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().Healing(healValue);
            gameObject.SetActive(false);
        }
    }
}
