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
    public float hopDuration = 1f;
    public float playerVisionDistance = 5f;

    private Vector3 hopTarget;
    private float hopDelay;

    private Vector3 defaultScale;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        defaultScale = transform.localScale;
        DoNextHop();
    }

    // Update is called once per frame
    void Update()
    {
        hopDelay -= Time.deltaTime;
        if (hopDelay < 0f) {
            DoNextHop();
        }
    }

    private void DoNextHop() {
        // Default to not hopping anywhere, but try to find a better position.
        hopTarget = transform.position;
        float maxDistToPlayer = Vector3.Distance(transform.position, player.transform.position);
        bool shouldAvoidPlayer = maxDistToPlayer < playerVisionDistance;
        for (int i = 0; i < HOP_SEARCH_ATTEMPTS; i++) {
            Vector3 target = RandomHopTarget();
            float distToPlayer = Vector3.Distance(target, player.transform.position);

            if (!GameController.instance.IsWaterHere(target)) {
                if (!shouldAvoidPlayer) {
                    hopTarget = target;
                    break;
                } else if (distToPlayer > maxDistToPlayer) {
                    hopTarget = target;
                    maxDistToPlayer = distToPlayer;
                }
            }
        }

        hopDelay = Random.Range(hopDelayMin, hopDelayMax);

        float facing = -Mathf.Sign(hopTarget.x - transform.position.x);
        transform.localScale = new Vector3(defaultScale.x * facing, defaultScale.y, defaultScale.z);
        LeanTween.move(gameObject, hopTarget, hopDuration);
    }

    private Vector3 RandomHopTarget() { 
        float rotation = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(hopDistanceMin, hopDistanceMax);

        float xDelta = Mathf.Cos(rotation) * distance;
        float yDelta = Mathf.Sin(rotation) * distance;
        return transform.position + new Vector3(xDelta, yDelta, 0f);
    }
}
