using ComeHomeGame;
using System.Collections;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenee : MonoBehaviour
{
    [SerializeField]
    public int indexScene;
    [SerializeField]
    public float x;  
    [SerializeField]
    public float y;

    private IEnumerator DelayAndLoadScene(float delay, int sceneIndex, Collider2D collision)
    {
        yield return new WaitForSeconds(delay);

        DataManager dataManager = DataManager.Instance;
        if (dataManager != null)
        {
            var health = collision.GetComponent<Health>();
            var currentHealth = health.CurrentHealth();
            var healthBlood = collision.GetComponent<HealBlood>();
            var maximumHealth = healthBlood.HealthValue();
            var playerController = collision.GetComponent<PlayerController>();
            var speed = playerController.GetSpeed();
            var jump = playerController.GetJump();
            var scale = playerController.GetScaleCharacter();

            dataManager.SetPlayerData(currentHealth, scale, jump, speed, 1, maximumHealth);
            SceneManager.LoadScene(sceneIndex);

            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetPosition(x, y, 0);
                var healthController = collision.gameObject.GetComponent<Health>();
                healthController.SetRespawn();
            }
             

        }
        else
        {
            Debug.LogError("Data Manager not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Bắt đầu coroutine để tạo độ trễ trước khi chuyển scene
            StartCoroutine(DelayAndLoadScene(3f, indexScene, collision));

        }
    }
}
