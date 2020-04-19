using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHopping : MonoBehaviour
{
    private readonly int HOP_SEARCH_ATTEMPTS = 10;

    public float hopDelayMin = 1f;
    public float hopDelayMax = 2f;
    public float hopDistanceMin = 1f;
    public float hopDistanceMax = 2f;

    private Vector3 hopTarget;
    private float hopDelay;

    // Start is called before the first frame update
    void Start()
    {
        CalculateNextHop();
    }

    // Update is called once per frame
    void Update()
    {
        hopDelay -= Time.deltaTime;
        if (hopDelay < 0f) {
            transform.position = hopTarget;
            CalculateNextHop();
        }
    }

    private void CalculateNextHop() {
        for (int i = 0; i < HOP_SEARCH_ATTEMPTS; i++) {
            Vector3 target = RandomHopTarget();
            Debug.Log(target);
            if (!GameController.instance.IsWaterHere(target)) {
                hopTarget = target;
            }
        }
        hopTarget = transform.position;

        hopDelay = Random.Range(hopDelayMin, hopDelayMax);
    }

    private Vector3 RandomHopTarget() { 
        float rotation = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(hopDistanceMin, hopDistanceMax);

        float xDelta = Mathf.Cos(rotation) * distance;
        float yDelta = Mathf.Sin(rotation) * distance;
        return transform.position + new Vector3(xDelta, yDelta, 0f);
    }
}
