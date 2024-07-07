using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Tạo một GameObject mới nếu instance chưa tồn tại
                GameObject go = new GameObject("DataManager");
                _instance = go.AddComponent<DataManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    public string Type { get; set; } = "Normal";
    public User user { get; set; }
    public float currentHealth;
    public Vector3 scalePlayer;
    public float Jump;
    public float Speed;
    public float Dame;
    public float maximumHealth;
    public float Score;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Phương thức để thiết lập dữ liệu
    public void SetPlayerData(float health, Vector3 scale, float jump, float speed, float dame, float maxHealth)
    {
        this.currentHealth = health;
        this.scalePlayer = scale;
        this.Jump = jump;
        this.Speed = speed;
        this.Dame = dame;
        this.maximumHealth = maxHealth;
    }

    // Phương thức để lấy dữ liệu
    public (float, Vector3, float, float, float, float) GetPlayerData()
    {
        return (currentHealth, scalePlayer, Jump, Speed, Dame, maximumHealth);
    }
    public void SetUser(User user)
    {
        this.user= user;
    }
    public void SetScore(float score)
    {
        this.Score = score;
    }
    public float GetScore()
    {
        return this.Score;
    }
}
