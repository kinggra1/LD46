using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioClip consumeSound;

    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    public AudioClip backgroundFire;

    private AudioSource consumeAudioSource;
    private AudioSource musicAudioSource;
    private AudioSource backgroundAudioSource;

    private void Awake() {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        consumeAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();

        musicAudioSource.clip = gameplayMusic;
        backgroundAudioSource.loop = true;
        musicAudioSource.volume = 0.1f;
        musicAudioSource.Play();

        backgroundAudioSource.clip = backgroundFire;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayConsumeSound(float expectedFuel) {

        consumeAudioSource.volume = Mathf.Min(expectedFuel / 5f, 1f);
        // consumeAudioSource.clip = consumeSound;
        consumeAudioSource.pitch = (Random.Range(0.6f, 1.1f));
        consumeAudioSource.PlayOneShot(consumeSound);
    }
}
