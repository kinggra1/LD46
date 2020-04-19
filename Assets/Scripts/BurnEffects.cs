using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BurnEffects : MonoBehaviour
{
    public static BurnEffects instance;

    public GameObject smallFire;

    private readonly float SMALL_FIRE_LIFE = 4f;

    private GameObject effectsParent;

    private void Awake() {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        effectsParent = new GameObject();
        effectsParent.name = "BurnEffects";
    }

    public void PlaceSmallFireNear(Vector3 location, float scaler) {
        location += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        PlaceSmallFire(location, scaler);
    }

    public void PlaceSmallFire(Vector3 location, float scaler) {
        GameObject effect = Instantiate(smallFire, location, Quaternion.identity);
        effect.gameObject.transform.SetParent(effectsParent.transform);

        effect.gameObject.transform.localScale = Vector3.zero;
        Vector3 targetScale = new Vector3(scaler, scaler, scaler);

        // Overshoot then drop to target scale.
        LeanTween.scale(effect, targetScale, 0.4f).setEaseOutBack().setOvershoot(2.3f);

        LeanTween.scale(effect, Vector3.zero, SMALL_FIRE_LIFE / 2).setDelay(SMALL_FIRE_LIFE / 2);
        Destroy(effect, SMALL_FIRE_LIFE);
    }
}
