////////////////////////////////////////////////////////////
// File: OGSCAgent.cs
// Author: Jack Peedle
// Date Created: 27/02/22
// Last Edited By: Jack Peedle
// Date Last Edited: 27/02/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class OGSCAgent : Agent{

    //
    [SerializeField] private Transform targetTransform;

    //
    public Transform shootingPoint;

    //
    public int minStepsBetweenShots = 50;

    //
    public int damage = 100;

    //
    private bool bShotAvailable = true;

    //
    private int stepsUntilShotAvailable = 0;

    //
    private Vector3 startingPosition;

    //
    private Rigidbody rb;

    //
    public override void OnEpisodeBegin() {


        //
        transform.localPosition = new Vector3(Random.Range(3f, -3f), 0, Random.Range(-2f, -3.5f));

        //
        targetTransform.localPosition = new Vector3(Random.Range(3.5f, -3.5f), 0f, Random.Range(3.5f, 2f));


    }

    //
    public override void OnActionReceived(ActionBuffers actions) {

        //
        float moveX = actions.ContinuousActions[0];

        //
        float moveZ = actions.ContinuousActions[1];

        //
        float moveSpeed = 3f;

        //
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        /*
        //
        if (Mathf.RoundToInt(actions.ContinuousActions[2]) >= 1) {

            //
            Shoot();

        }
        */

    }

    //
    public override void Heuristic(in ActionBuffers actionsOut) {

        //
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;

        //
        continousActions[0] = Input.GetAxisRaw("Horizontal");

        //
        continousActions[1] = Input.GetAxisRaw("Vertical");

        //
        //continousActions[2] = Input.GetKey("P") ? 1f : 0f;

    }

    //
    public override void CollectObservations(VectorSensor sensor) {

        //
        sensor.AddObservation(transform.localPosition);

        //
        sensor.AddObservation(targetTransform.localPosition);

    }

    //
    public void Update() {

        //
        var layerMask = 1 << LayerMask.NameToLayer("Enemy");

        //
        Debug.DrawRay(shootingPoint.position, Vector3.forward * 30f, Color.green, 2f);

        //
        if (Physics.Raycast(shootingPoint.position, Vector3.forward, out var hit, 200f, layerMask)) {

            //
            Debug.Log("HIT ENEMY");

            //
            SetReward(+1f);

            //
            EndEpisode();

        }

        /*
        if (MaxStep >= 500000) {

            //
            Debug.Log("MAX STEP REACHED");

        }

        if (MaxStep <= 0) {

            //
            Debug.Log("MIN STEP REACHED");

        }
        */


    }

    //
    private void Shoot() {

        //
        if (!bShotAvailable) {

            //
            return;

        }

        //
        var layerMask = 1 << LayerMask.NameToLayer("Enemy");

        //
        var direction = transform.forward;

        //
        Debug.Log("Shot");

        //
        Debug.DrawRay(shootingPoint.position, direction * 200f, Color.green, 2f);

        //
        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 200f, layerMask)) {

            Debug.Log("HIT ENEMY");

            //
            hit.transform.GetComponent<Enemy>().GetShot(damage);//, this);

            //
            SetReward(+1f);

            //
            EndEpisode();

        }

        //
        bShotAvailable = false;

        //
        stepsUntilShotAvailable = minStepsBetweenShots;


    }

    
    //
    private void OnTriggerEnter(Collider other) {

        // Add reward for each kill / damage on enemy
        //AddReward

        //
        if (other.TryGetComponent<OGSCWall>(out OGSCWall OGSCwall)) {

            //
            SetReward(-1f);

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }

    }
    

    //
    private void FixedUpdate() {

        //
        if (!bShotAvailable) {

            //
            stepsUntilShotAvailable--;

            //
            if (stepsUntilShotAvailable <= 0) {

                bShotAvailable = true;

            }
                

        }

    }

    /*
    //
    public override void OnActionReceived(float[] vectorAction) {

        //
        if (Mathf.RoundToInt(vectorAction[0]) >= 1) {

            //
            Shoot();

        }

    }

    
    //
    public override void Heuristic(float[] actionsOut) {

        //
        actionsOut[0] = Input.GetKey("P") ? 1f : 0f;

    }
    */

}
