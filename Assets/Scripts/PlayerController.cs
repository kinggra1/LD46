using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float MOVEMENT_SPEED = 2f;
    private readonly float DEFAULT_FUEL_LOSS_RATE = 0.5f;

    // This is UR LIFE FORCE.
    private float fuel = 5f;
    // fuel lost per second
    private float fuelLossRate = 0.5f;

    // Start is called before the first frame update
    void Start() {
        fuelLossRate = DEFAULT_FUEL_LOSS_RATE;
    }

    // Update is called once per frame
    void Update() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        fuel -= fuelLossRate * Time.deltaTime;

        transform.position += new Vector3(xInput, yInput) * MOVEMENT_SPEED * Time.deltaTime;
        transform.localScale = new Vector3(fuel, fuel);
    }

    public void AddFuel(float deltaFuel) {
        this.fuel += deltaFuel;
        // TODO: Check for constraints (e.g. 0 since this could be negative fuel).
    }

    // Callback whenever we enter a "trigger", which is a collder on an object with "isTrigger" checked.
    private void OnTriggerEnter2D(Collider2D other) {
        ConsumableObject consumable = other.GetComponent<ConsumableObject>();
        if (consumable) {
            consumable.BeConsumedBy(this);
        }
        
    }
}
