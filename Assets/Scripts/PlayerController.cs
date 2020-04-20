using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject miniCollider;
    public GameObject smallCollider;
    public GameObject mediumCollider;
    public GameObject ohLawdCollider;

    private readonly float MOVEMENT_SPEED = 2f;
    private readonly float DEFAULT_FUEL_LOSS_RATE = 0.5f;
    private readonly float MAX_FUEL = 100f;

    private readonly float MIN_SMALL_SIZE = 5f;
    private readonly float MIN_MEDIUM_SIZE = 15f;
    private readonly float MIN_OHLAWD_SIZE = 25f;

    // This is UR LIFE FORCE.
    private float fuel = 4.5f;
    // fuel lost per second
    private float fuelLossRate = 0.5f;

    private float xInput;
    private float yInput;

    public enum size
    {
        mini,
        small,
        medium,
        ohLawd
    }
    private size currentSpriteSize = size.mini;

    public RuntimeAnimatorController miniAnimation;
    public RuntimeAnimatorController smallAnimation;
    public RuntimeAnimatorController mediumAnimation;
    public RuntimeAnimatorController ohLawdAnimation;

    // Start is called before the first frame update
    void Start() {
        fuelLossRate = DEFAULT_FUEL_LOSS_RATE;
    }

    // Update is called once per frame
    void Update() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        CheckSize();
    }

    // Physics timestep. Put motion code here for smoother motions and collisions.
    private void FixedUpdate() {
        if (GameController.instance.IsPaused()) {
            return;
        }

        UpdateFuel();

        transform.position += new Vector3(xInput, yInput) * MOVEMENT_SPEED * Time.fixedDeltaTime;

        float playerScale = CalculateScale();
        transform.localScale = new Vector3(playerScale, playerScale);
    }

    private void SetColliderActive(size size, bool active) {
        switch (size) {
            case size.mini:
                miniCollider.SetActive(active);
                break;
            case size.small:
                smallCollider.SetActive(active);
                break;
            case size.medium:
                mediumCollider.SetActive(active);
                break;
            case size.ohLawd:
                ohLawdCollider.SetActive(active);

                break;
        }
    }

    private void SetNewSizeObjects(size newSize) {
        SetColliderActive(currentSpriteSize, false);
        currentSpriteSize = newSize;
        SetColliderActive(currentSpriteSize, true);
    }

    private void CheckSize()
    {
        if (fuel < MIN_SMALL_SIZE && currentSpriteSize != size.mini)
        {
            SetNewSizeObjects(size.mini);
            SetAnimation(miniAnimation);
        }
        else if (fuel >= MIN_SMALL_SIZE && fuel < MIN_MEDIUM_SIZE && currentSpriteSize != size.small)
        {
            SetNewSizeObjects(size.small);
            SetAnimation(smallAnimation);

        }
        else if (fuel >= MIN_MEDIUM_SIZE && fuel < MIN_OHLAWD_SIZE && currentSpriteSize != size.medium)
        {
            SetNewSizeObjects(size.medium);
            SetAnimation(mediumAnimation);
        }
        else if (fuel >= MIN_OHLAWD_SIZE && currentSpriteSize != size.ohLawd)
        {
            SetNewSizeObjects(size.ohLawd);
            SetAnimation(ohLawdAnimation);
        }
    }

    private void SetAnimation(RuntimeAnimatorController animation)
    {
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animation;
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
            AudioController.instance.PlayPortalSound();
            GameController.instance.PortalToScene(portal.sceneToLoad);
        }

        BurnableObject burnable = collider.GetComponent<BurnableObject>();
        if (burnable)
        {
            burnable.TryBeBurnedBy(this);
        }
    }

    public size GetSize()
    {
        return currentSpriteSize;
    }
}
