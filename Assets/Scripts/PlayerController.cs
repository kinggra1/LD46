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


    private float xInput;
    private float yInput;

    // Start is called before the first frame update
    void Start() {
        fuelLossRate = DEFAULT_FUEL_LOSS_RATE;
    }

    // Update is called once per frame
    void Update() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        fuel -= fuelLossRate * Time.deltaTime;
    }

    // Physics timestep. Put motion code here for smoother motions and collisions.
    private void FixedUpdate() {
        transform.position += new Vector3(xInput, yInput) * MOVEMENT_SPEED * Time.fixedDeltaTime;
        transform.localScale = new Vector3(fuel, fuel);
    }

    public void AddFuel(float deltaFuel) {
        this.fuel += deltaFuel;
        // TODO: Check for constraints (e.g. 0 since this could be negative fuel).
    }

    public float GetFuel() {
        return fuel;
    }

    // Callback whenever we enter a "trigger", which is a collder on an object with "isTrigger" checked.
    private void OnCollisionEnter2D(Collision2D collision) {
        ConsumableObject consumable = collision.otherCollider.GetComponent<ConsumableObject>();
            if (consumable) {
                consumable.BeConsumedBy(this);
            }
        
    }
}
