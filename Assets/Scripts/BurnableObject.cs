using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour {
    [SerializeField]
    private ConsumeAction action;

    public Sprite burnedSprite;

    private bool burned = false;

    public void TryBeBurnedBy(PlayerController player)
    {
        if (!burned && action.CanBeAppliedTo(player))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = burnedSprite;
            burned = true;

            action.ApplyTo(player);
            float effectSize = action.GetExpectedFuel();

            if (effectSize >= 0f) {
                AudioController.instance.PlayConsumeSound(effectSize);
                BurnEffects.instance.PlaceSmallFire(this.transform.position, effectSize);
            } else {
                // Cold sounds?
            }
        }
    }

    public bool IsBurned() {
        return burned;
    }
}
