using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : MonoBehaviour {
    [SerializeField]
    private ConsumeAction action;

    public void TryBeConsumedBy(PlayerController player) {
        if (action.CanBeAppliedTo(player)) {
            action.ApplyTo(player);
            Destroy(this.gameObject);
            AudioController.instance.PlayConsumeSound();
        }
    }
}
