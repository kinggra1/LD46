using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioClip consumeSound;

    private AudioSource consumeAudioSource;

    private void Awake() {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        consumeAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayConsumeSound() {
        // consumeAudioSource.clip = consumeSound;
        consumeAudioSource.pitch = (Random.Range(0.6f, 1.1f));
        consumeAudioSource.PlayOneShot(consumeSound);
    }
}
