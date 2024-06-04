using UnityEngine;

public class SoundManage : MonoBehaviour
{
    public static SoundManage instance
    {
        get;
        private set;
    }
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //keep this object even go to new scene
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
