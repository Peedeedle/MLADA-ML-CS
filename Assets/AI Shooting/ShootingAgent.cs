using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ShootingAgent : Agent
{
    public int score = 0;
    public float speed = 3f;
    public float rotationSpeed = 3f;

    public Transform shootingPoint;
    public int minStepsBetweenShots = 50;
    public int damage = 100;

    public Projectile projectile;
    public EnemyManager enemyManager;
    //public ShootingWall shootingwall;

    private bool ShotAvaliable = true;
    private int StepsUntilShotIsAvaliable = 0;

    private Vector3 StartingPosition;
    private Rigidbody Rb;
    private EnvironmentParameters EnvironmentParameters;

    public event Action OnEnvironmentReset;

    private void Shoot() {
        if (!ShotAvaliable)
            return;

        var layerMask = 1 << LayerMask.NameToLayer("Enemy");
        var direction = transform.forward;

        var spawnedProjectile = Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0f, -90f, 0f));
        spawnedProjectile.SetDirection(direction);

        Debug.DrawRay(transform.position, direction, Color.blue, 1f);

        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 200f, layerMask)) {
            hit.transform.GetComponent<Enemy>().GetShot(damage, this);
            //AddReward(1f);
            //Debug.Log("+1 REWARD1 HIT ENEMY");
        } else {

            AddReward(-0.033f);
            Debug.Log("-1 REWARD HIT WALL");

        }


        ShotAvaliable = false;
        StepsUntilShotIsAvaliable = minStepsBetweenShots;
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(ShotAvaliable);
        //Add Angle Y
    }

    private void FixedUpdate() {
        if (!ShotAvaliable) {
            StepsUntilShotIsAvaliable--;

            if (StepsUntilShotIsAvaliable <= 0)
                ShotAvaliable = true;
        }
        Debug.Log("-1 REWARD1");
        AddReward(-1f / MaxStep);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        if (Mathf.RoundToInt(actions.ContinuousActions[0]) >= 1) {
            Shoot();
        }

        Rb.velocity = new Vector3(actions.ContinuousActions[1] * speed, 0f, actions.ContinuousActions[2] * speed);
        transform.Rotate(Vector3.up, actions.ContinuousActions[3] * rotationSpeed);
    }

    public override void Initialize() {
        StartingPosition = transform.position;
        Rb = GetComponent<Rigidbody>();

        //TODO: Delete
        Rb.freezeRotation = true;
        EnvironmentParameters = Academy.Instance.EnvironmentParameters;
    }
    public override void Heuristic(in ActionBuffers actionsOut) {

        //
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;

        continousActions[0] = Input.GetKey(KeyCode.P) ? 1f : 0f;
        continousActions[2] = Input.GetAxis("Horizontal");
        //actionsOut[1] = -Input.GetAxis("Vertical");
        continousActions[3] = Input.GetAxis("Vertical");
    }

    public override void OnEpisodeBegin() {
        OnEnvironmentReset?.Invoke();

        //Load Parameter from Curciulum
        minStepsBetweenShots = Mathf.FloorToInt(EnvironmentParameters.GetWithDefault("shootingFrequenzy", 30f));

        transform.position = StartingPosition;
        Rb.velocity = Vector3.zero;
        ShotAvaliable = true;
    }

    public void RegisterKill() {
        score++;
        AddReward(1.0f / EnvironmentParameters.GetWithDefault("amountZombies", 4f));
        Debug.Log("+1 REWARD2");
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("enemy")) {
            enemyManager.SetEnemiesActive();
            AddReward(-1f);
            Debug.Log("+1 REWARD3");
            EndEpisode();
        }
    }
}