using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : MonoBehaviour {
    [SerializeField]
    private ConsumeAction action;

    public void BeConsumedBy(PlayerController player) {
        action.Apply(player);
        Destroy(this.gameObject);
    }
}
