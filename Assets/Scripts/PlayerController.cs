using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private readonly float MOVEMENT_SPEED = 2f;
    private readonly float DEFAULT_FUEL_LOSS_RATE = 0.5f;
    private readonly float MAX_FUEL = 100f;

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

    }

    // Physics timestep. Put motion code here for smoother motions and collisions.
    private void FixedUpdate() {
        UpdateFuel();

        transform.position += new Vector3(xInput, yInput) * MOVEMENT_SPEED * Time.fixedDeltaTime;

        float playerScale = CalculateScale();
        transform.localScale = new Vector3(playerScale, playerScale);
    }

    private float CalculateScale() {
        return Mathf.Sqrt(fuel);
    }

    private void ConstrainFuelCheck() {
        // Arbitrarily cap at 0.5f for now.
        if (fuel < 0.5f) {
            GameController.instance.Die();
        }
    }

    private void UpdateFuel() {
        fuel -= fuelLossRate * Time.deltaTime;

        ConstrainFuelCheck();
    }

    public void AddFuel(float deltaFuel) {
        this.fuel += deltaFuel;
        ConstrainFuelCheck();
    }

    public float GetFuel() {
        return fuel;
    }

    public float GetFuelRatio() {
        return fuel / MAX_FUEL;
    }

    // Callback whenever we enter a "trigger", which is a collder on an object with "isTrigger" checked.
    private void OnCollisionEnter2D(Collision2D collision) {
        ConsumableObject consumable = collision.collider.GetComponent<ConsumableObject>();
        if (consumable) {
            consumable.TryBeConsumedBy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        ExitPortal portal = collider.GetComponent<ExitPortal>();
        if (portal) {
            GameController.instance.PortalToScene(portal.sceneToLoad);
        }

        BurnableObject burnable = collider.GetComponent<BurnableObject>();
        if (burnable)
        {
            burnable.TryBeBurnedBy(this);
        }
    }
}
