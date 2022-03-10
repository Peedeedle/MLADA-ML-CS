////////////////////////////////////////////////////////////
// File: OGSCAgent.cs
// Author: Jack Peedle
// Date Created: 27/02/22
// Last Edited By: Jack Peedle
// Date Last Edited: 27/02/22
// Brief: 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class OGSCTAgent : Agent
{

    public GameObject CTEnemy;

    //public GameObject T;

    //
    //public event EventHandler OnThis1;

    //
    public event EventHandler OnEpisodeBeginEvent;

    //
    public Transform TshootingPoint;

    //
    private Rigidbody CTRigidBody;

    //
    private Rigidbody TRigidBody;

    //
    private void Awake() {

        //
        CTRigidBody = CTEnemy.GetComponent<Rigidbody>();

        //
        TRigidBody = this.GetComponent<Rigidbody>();

    }

    //
    public override void OnEpisodeBegin() {

        //
        //if (this.gameObject.tag == "CT") {

            //
            //transform.localPosition = new Vector3(3.5f, 0.1f, UnityEngine.Random.Range(18, 23));

        //}

        //
        //if (this.gameObject.tag == "T") {

        //
        transform.localPosition = new Vector3(-13.5f, 0.1f, UnityEngine.Random.Range(18, 23));

        //}

        //
        OnEpisodeBeginEvent?.Invoke(this, EventArgs.Empty);

    }

    //
    public override void CollectObservations(VectorSensor sensor) {

        //
        //if (this.gameObject.tag == "CT") {

            //
            //sensor.AddObservation(T.transform.position);

            //
            //Vector3 dirToT = (T.transform.localPosition - transform.localPosition).normalized;

            //
            //sensor.AddObservation(dirToT.x);

            //
            //sensor.AddObservation(dirToT.z);


        //}

        //
        if (this.gameObject.tag == "T") {

            //
            sensor.AddObservation(CTEnemy.transform.position);

            //
            Vector3 dirToCT = (CTEnemy.transform.localPosition - transform.localPosition).normalized;

            //
            sensor.AddObservation(dirToCT.x);

            //
            sensor.AddObservation(dirToCT.z);


            sensor.AddObservation(this.transform.rotation.y);


        }



    }

    //
    public override void OnActionReceived(ActionBuffers actions) {

        //
        //if (this.gameObject.tag == "CT") {

            //
            // 6 Vector Space Size
            //
            //
            //float moveX = actions.DiscreteActions[0];

            //
            //float moveZ = actions.DiscreteActions[1];

            //
            //float moveSpeed = 1f;

            //
            //CT.transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        //}


        //
        if (this.gameObject.tag == "T") {

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
            float rotationSpeed = 1f;

            //
            this.transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;


            transform.Rotate(Vector3.up, actions.DiscreteActions[1] * rotationSpeed);

            transform.Rotate(Vector3.down, actions.DiscreteActions[1] * rotationSpeed);

        }



    }

    //
    public override void Heuristic(in ActionBuffers actionsOut) {

        //
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        //
        discreteActions[0] = (int)Input.GetAxisRaw("Horizontal");

        //
        discreteActions[1] = (int)Input.GetAxisRaw("Vertical");

    }

    //
    public void Update() {

        //
        var layerMaskEnemy = 1 << LayerMask.NameToLayer("CT");

        //
        //var layerMaskWall = 1 << LayerMask.NameToLayer("Wall");

        //
        var direction = transform.forward;

        //
        //Debug.DrawRay(CTshootingPoint.position, direction * 20f, Color.blue, 20f);

        //
        if (Physics.Raycast(TshootingPoint.position, direction, out var Hit, 20f)) {

            //
            //Debug.Log("HIT Wall");

            Debug.DrawLine(TshootingPoint.position, Hit.point, Color.red);

        }

        /*
        //
        if (Physics.Raycast(CTshootingPoint.position, direction, out var enemyHit, 20f, layerMaskEnemy)) {

            //
            Debug.Log("CT WINS");

            //
            SetReward(+1f);


            //hit.transform.gameObject.SetActive(false);


            //
            EndEpisode();

        }
        */

        if (Hit.transform.gameObject.tag == "CT") {

            //
            Debug.Log("T WINS");

            //
            SetReward(+1f);


            //hit.transform.gameObject.SetActive(false);


            //
            EndEpisode();

        }



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




























    /*

    //
    [SerializeField] private Transform targetTransform;


    //
    public Transform shootingPoint;

    //
    public int minStepsBetweenShots = 50;

    //
    public int damage = 100;

    //
    private Vector3 startingPosition;

    //
    private Rigidbody rb;

    public float rotationSpeed = 3f;

    public GameObject collectable1;



    //
    public override void OnEpisodeBegin() {

        //
        transform.localPosition = new Vector3(27.88f, 0.344f, 4);

        //
        targetTransform.localPosition = new Vector3(23.23f, 1.0134f, -7.5f);


    }

    //
    public override void OnActionReceived(ActionBuffers actions) {


        //
        // 6 Vector Space Size
        //
        //
        float moveX = actions.ContinuousActions[0];

        //
        float moveZ = actions.ContinuousActions[1];

        //
        float moveSpeed = 1f;

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
        var direction = transform.forward;

        //
        Debug.DrawRay(shootingPoint.position, direction * 20f, Color.green, 20f);

        //
        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 20f, layerMask)) {

            //
            Debug.Log("HIT ENEMY");

            //
            SetReward(+1f);


            hit.transform.gameObject.SetActive(false);


            //
            //EndEpisode();

        }



    }

    //
    private void OnTriggerEnter(Collider other) {

        // Add reward for each kill / damage on enemy
        //AddReward

        //
        if (other.TryGetComponent<OGSCWall>(out OGSCWall OGSCwall)) {

            //
            SetReward(-5f);

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }

        //
        if (other.TryGetComponent<CTAgent>(out CTAgent CTAgent)) {

            //
            SetReward(+2f);

            //
            //floorMeshRenderer.material = loseMaterial;

            //
            EndEpisode();

        }


    }

    */

}
