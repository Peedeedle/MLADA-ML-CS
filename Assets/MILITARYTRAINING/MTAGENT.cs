////////////////////////////////////////////////////////////
// File: MTAGENT.cs
// Author: Jack Peedle
// Date Created: 08/03/22
// Last Edited By: Jack Peedle
// Date Last Edited: 08/03/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MTAGENT : Agent {

    public RandomWallManager randomWallManager;

    //
    [SerializeField] public Transform targetTransform;

    //
    [SerializeField] private Material winMaterial;

    //
    [SerializeField] private Material loseMaterial;

    //
    [SerializeField] private MeshRenderer floorMeshRenderer;


    //
    public Transform ringToFollow1;

    //
    public Transform ringToFollow2;

    //
    public Transform ringToFollow3;


    //
    public override void OnEpisodeBegin() {

        randomWallManager.RandomizeInt();

        if (randomWallManager.randomWallInt == 0) {

            //
            targetTransform = ringToFollow1;

            Debug.Log("RING1");

        }

        if (randomWallManager.randomWallInt == 1) {

            //
            targetTransform = ringToFollow2;  //.transform;

            Debug.Log("RING2");

        }

        if (randomWallManager.randomWallInt == 2) {

            //
            targetTransform = ringToFollow3;

            Debug.Log("RING3");

        }


        //targetTransform.gameObject.SetActive(true);

        //
        transform.localPosition = new Vector3(UnityEngine.Random.Range(0.7f, 4.5f), 0f, UnityEngine.Random.Range(1.2f, 3.2f));

        //
        //targetTransform.localPosition = new Vector3(UnityEngine.Random.Range(-4f, +4f), -1.5f, UnityEngine.Random.Range(-3.5f, 3.5f));


    }

    //
    public override void CollectObservations(VectorSensor sensor) {

        //
        sensor.AddObservation(transform.localPosition);

        //
        sensor.AddObservation(targetTransform.localPosition);

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

    }

    //
    public override void Heuristic(in ActionBuffers actionsOut) {

        //
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;

        //
        continousActions[0] = Input.GetAxisRaw("Horizontal");

        //
        continousActions[1] = Input.GetAxisRaw("Vertical");

    }

    //
    private void OnTriggerEnter(Collider other) {

        // Add reward for each kill / damage on enemy
        //AddReward

        //
        if (other.TryGetComponent<ENDCONDITIONS>(out ENDCONDITIONS eNDCONDITIONS)) {

            //
            SetReward(+1f);

            //
            floorMeshRenderer.material = winMaterial;

            //
            EndEpisode();

        }

        //
        if (other.TryGetComponent<MTBOXES>(out MTBOXES mtBOXES)) {

            //
            SetReward(-1f);

            //
            floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }

        //
        if (other.TryGetComponent<MTRINGS>(out MTRINGS mtRINGS)) {

            //
            SetReward(+1f);

            //other.gameObject.SetActive(false);

            //
            floorMeshRenderer.material = winMaterial;

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }




    }



















    /*

    //
    public GameObject enemyBox;

    //
    public Transform shootingPoint;

    //
    public Rigidbody Rb;


    //
    public override void OnEpisodeBegin() {

        //
        transform.localPosition = new Vector3(0.7f, 4.5f, UnityEngine.Random.Range(1.2f, 3.2f));

    }

    //
    public override void CollectObservations(VectorSensor sensor) {

        //
        sensor.AddObservation(enemyBox.transform.position);

        //
        Vector3 dirToT = (enemyBox.transform.localPosition - transform.localPosition).normalized;

        //
        sensor.AddObservation(dirToT.x);

        //
        sensor.AddObservation(dirToT.z);



        //sensor.AddObservation(this.transform.rotation.y);



    }

    //
    public override void OnActionReceived(ActionBuffers actions) {

        //
        // 6 Vector Space Size
        //
        //
        float moveX = actions.DiscreteActions[0];

        //
        float moveZ = actions.DiscreteActions[1];

        //
        float moveSpeed = 1f;

        //
        //float rotationSpeed = 1f;

        //
        this.transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        //transform.Rotate(Vector3.up, actions.DiscreteActions[1] * rotationSpeed);

        //transform.Rotate(Vector3.down, actions.DiscreteActions[1] * rotationSpeed);

    }

    //
    public override void Heuristic(in ActionBuffers actionsOut) {

        //
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        //
        discreteActions[0] = (int)Input.GetAxisRaw("Vertical");

        //
        discreteActions[1] = (int)Input.GetAxisRaw("Horizontal");

    }

    //
    public void Update() {



    }

    //
    private void OnTriggerEnter(Collider other) {

        // Add reward for each kill / damage on enemy
        //AddReward

        //
        if (other.TryGetComponent<MTBOXES>(out MTBOXES MTboxes)) {

            //
            AddReward(-1f);

            Debug.Log("HIT BOX");

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }

        //
        if (other.TryGetComponent<MTRINGS>(out MTRINGS MTrings)) {

            //
            AddReward(+1f);

            Debug.Log("HIT RING");

            other.gameObject.SetActive(false);

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            //EndEpisode();

        }

        //
        if (other.TryGetComponent<ENDCONDITIONS>(out ENDCONDITIONS ENDCONDITIONS)) {

            //
            AddReward(+2f);


            Debug.Log("FINISHED");

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }

    }
    */

}
