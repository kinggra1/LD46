﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public struct PlayerStats {
    // Travel speed of player at this size.
    public float speed;
    // Fuel loss at this size.
    public float fuelLoss;
    // How long you will be immune from fuel loss when growing (not shrinking) to this size).
    public float immunityTime;

    public PlayerStats(float speed, float fuelLoss, float immunityTime) {
        this.speed = speed;
        this.fuelLoss = fuelLoss;
        this.immunityTime = immunityTime;
    }
}

public class PlayerController : MonoBehaviour
{
    public GameObject miniCollider;
    public GameObject smallCollider;
    public GameObject mediumCollider;
    public GameObject ohLawdCollider;

    private readonly float MAX_FUEL = 50f;

    private readonly float MIN_SMALL_SIZE = 15f;
    private readonly float MIN_MEDIUM_SIZE = 25f;
    private readonly float MIN_OHLAWD_SIZE = 50f;

    // This is UR LIFE FORCE.
    private float fuel = 4.5f;

    private float immunityTimer = 0f;

    private float xInput;
    private float yInput;

    private Tilemap tilemap;

    public enum size
    {
        mini,
        small,
        medium,
        ohLawd
    }
    Dictionary<size, PlayerStats> statsMap = new Dictionary<size, PlayerStats>();

    private size currentSpriteSize = size.mini;

    public RuntimeAnimatorController miniAnimation;
    public RuntimeAnimatorController smallAnimation;
    public RuntimeAnimatorController mediumAnimation;
    public RuntimeAnimatorController ohLawdAnimation;

    public Animator faceAnimator;

    public enum expression
    {
        idle,
        scared
    }

    private expression currentFaceExpression = expression.idle;

    public RuntimeAnimatorController idleExpressionAnimation;
    public RuntimeAnimatorController scaredExpressionAnimation;

    private void Awake() {
        /*
         * Stats are:
         *     1. Speed
         *     2. Fuel Loss Rate
         *     3. Invincible time after growing (not shrinking) to size.
         */
        statsMap.Add(size.mini, new PlayerStats(2f, 1f, 0f));
        statsMap.Add(size.small, new PlayerStats(2.5f, 2f, 1f));
        statsMap.Add(size.medium, new PlayerStats(3.5f, 5f, 1f));
        statsMap.Add(size.ohLawd, new PlayerStats(4f, 20f, 3f));
    }

    // Start is called before the first frame update
    void Start() {
        tilemap = GetComponent<Tilemap>();

        float playerScale = CalculateScale();
        transform.localScale = new Vector3(playerScale, playerScale);
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

        transform.position += new Vector3(xInput, yInput) * CurrentSpeed() * Time.fixedDeltaTime;

        float playerScale = CalculateScale();
        transform.localScale = new Vector3(playerScale, playerScale);
    }

    private float CurrentSpeed() {
        return statsMap[currentSpriteSize].speed;
    }

    private float CurrentFuelLossRate() {
        return statsMap[currentSpriteSize].fuelLoss;
    }

    private bool Immune() {
        return immunityTimer > 0f;
    }

    private void SetImmunityTimer(float immunity) {
        immunityTimer = immunity;
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
        // Add immunity timer if this change was a growth.
        if (currentSpriteSize < newSize) {
            SetImmunityTimer(statsMap[newSize].immunityTime);
        }

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
        return Mathf.Sqrt(fuel/2);
    }

    private void ConstrainFuelCheck() {
        // Arbitrarily cap at 0.5f for now.
        if (fuel < 0.5f) {
            GameController.instance.Die();
        }
    }

    private void UpdateFuel() {
        immunityTimer -= Time.deltaTime;
        if (Immune()) {
            return;
        }

        fuel -= CurrentFuelLossRate() * Time.deltaTime;
        ConstrainFuelCheck();
    }

    public void AddFuel(float deltaFuel) {

        if (deltaFuel < 0f && Immune()) {
            return;
        }

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
        ConsumableObject consumable = collider.GetComponent<ConsumableObject>();
        if (consumable) {
            consumable.TryBeConsumedBy(this);
        }
    }

    public size GetSize()
    {
        return currentSpriteSize;
    }

    public void SetFaceAnimation(expression faceExpression)
    {
        if (currentFaceExpression != faceExpression)
        {
            currentFaceExpression = faceExpression;
            if (currentFaceExpression == expression.idle)
            {
                faceAnimator.runtimeAnimatorController = idleExpressionAnimation;
            }
            else if (currentFaceExpression == expression.scared)
            {
                faceAnimator.runtimeAnimatorController = scaredExpressionAnimation;
            }
        }
    }
}
