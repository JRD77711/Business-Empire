using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip clickSFX;
    public AudioClip cashSFX;
    public AudioClip upgradeSFX;
    public AudioClip errorSFX;
    public AudioClip eventSFX;

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
        if (bgmSource == null || bgmClip == null)
            return;

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.ignoreListenerPause = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        PlaySFX(clickSFX);
    }

    public void PlayCash()
    {
        PlaySFX(cashSFX);
    }

    public void PlayUpgrade()
    {
        PlaySFX(upgradeSFX);
    }

    public void PlayError()
    {
        PlaySFX(errorSFX);
    }

    public void PlayEvent()
    {
        PlaySFX(eventSFX);
    }
}