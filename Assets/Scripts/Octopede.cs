using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopede : MonoBehaviour {

    GameObject femurL1;
    GameObject tibiaL1;
    GameObject femurR1;
    GameObject tibiaR1;

    GameObject femurL2;
    GameObject tibiaL2;
    GameObject femurR2;
    GameObject tibiaR2;

    GameObject femurL3;
    GameObject tibiaL3;
    GameObject femurR3;
    GameObject tibiaR3;

    float sumDeltaTimes = 0.0f;    // reset to 0 after every wavelength
    float femurAmplitude = 10.0f;  // amplitude of angle of femur
    float tibiaAmplitude = 5.0f;  // amplitude of angle of femur
    float frequency = 20.0f;       // cycles per second
    float femurHomeAngle = 0.0f;   // home angle of femur
    float tibiaHomeAngle = -45.0f;  // home angle of femur

    // Use this for initialization
    void Start () {
        femurL1 = GameObject.FindGameObjectWithTag("FemurL1");
        tibiaL1 = GameObject.FindGameObjectWithTag("TibiaL1");
        femurR1 = GameObject.FindGameObjectWithTag("FemurR1");
        tibiaR1 = GameObject.FindGameObjectWithTag("TibiaR1");

        femurL2 = GameObject.FindGameObjectWithTag("FemurL2");
        tibiaL2 = GameObject.FindGameObjectWithTag("TibiaL2");
        femurR2 = GameObject.FindGameObjectWithTag("FemurR2");
        tibiaR2 = GameObject.FindGameObjectWithTag("TibiaR2");

        femurL3 = GameObject.FindGameObjectWithTag("FemurL3");
        tibiaL3 = GameObject.FindGameObjectWithTag("TibiaL3");
        femurR3 = GameObject.FindGameObjectWithTag("FemurR3");
        tibiaR3 = GameObject.FindGameObjectWithTag("TibiaR3");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void AdjustFemurSpring(HingeJoint hingeJoint, float deltaPosition)
    {
        JointSpring jointSpring = hingeJoint.spring;
        jointSpring.targetPosition = femurHomeAngle + deltaPosition;
        hingeJoint.spring = jointSpring;
    }

    void AdjusTibiaSpring(HingeJoint hingeJoint, float deltaPosition)
    {
        JointSpring jointSpring = hingeJoint.spring;
        jointSpring.targetPosition = tibiaHomeAngle + deltaPosition;
        hingeJoint.spring = jointSpring;
    }

    void FixedUpdate()
    {
        // NOTE: These are far from optial spring settings as a function of time.
        //       This is just a first sdtab ait getting the critter to actually move.
        // Much more careful code has to be put in place here to more efficiently move the critter.

        AdjustFemurSpring(femurL1.GetComponent<HingeJoint>(), femurAmplitude * Mathf.Sin(frequency * sumDeltaTimes));
        AdjusTibiaSpring(tibiaL1.GetComponent<HingeJoint>(), tibiaAmplitude * -Mathf.Sin(frequency * sumDeltaTimes));
        AdjustFemurSpring(femurR1.GetComponent<HingeJoint>(), femurAmplitude * Mathf.Cos(frequency * sumDeltaTimes));
        AdjusTibiaSpring(tibiaR1.GetComponent<HingeJoint>(), tibiaAmplitude * -Mathf.Cos(frequency * sumDeltaTimes));

        AdjustFemurSpring(femurL2.GetComponent<HingeJoint>(), femurAmplitude * -Mathf.Cos(frequency * sumDeltaTimes));
        AdjusTibiaSpring(tibiaL2.GetComponent<HingeJoint>(), tibiaAmplitude * Mathf.Cos(frequency * sumDeltaTimes)); // ???
        AdjustFemurSpring(femurR2.GetComponent<HingeJoint>(), femurAmplitude * -Mathf.Sin(frequency * sumDeltaTimes));
        AdjusTibiaSpring(tibiaR2.GetComponent<HingeJoint>(), tibiaAmplitude * Mathf.Sin(frequency * sumDeltaTimes)); // ???

        AdjustFemurSpring(femurL3.GetComponent<HingeJoint>(), femurAmplitude * -Mathf.Sin(frequency * sumDeltaTimes));
        AdjusTibiaSpring(tibiaL3.GetComponent<HingeJoint>(), tibiaAmplitude * Mathf.Sin(frequency * sumDeltaTimes));
        AdjustFemurSpring(femurR3.GetComponent<HingeJoint>(), femurAmplitude * -Mathf.Cos(frequency * sumDeltaTimes));
        AdjusTibiaSpring(tibiaR3.GetComponent<HingeJoint>(), tibiaAmplitude * Mathf.Cos(frequency * sumDeltaTimes));
        
        sumDeltaTimes += Time.deltaTime;
        if (frequency * sumDeltaTimes >= 2*Mathf.PI)
        {
            sumDeltaTimes = 0.0f;
        }

    }
}
