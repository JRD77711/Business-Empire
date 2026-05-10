using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioClip bgmClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSource == null || bgmClip == null) return;

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.ignoreListenerPause = true;
        bgmSource.Play();
    }
}
