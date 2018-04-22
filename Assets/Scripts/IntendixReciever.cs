using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntendixReciever : MonoBehaviour {

    [SerializeField]
    HelicopterController _helicopterController;

    private void Update()
    {
        ProcessIntendixInput();
    }

    void ProcessIntendixInput()
    {
        //Replace keyboard input with intendix one
        if (Input.GetKeyDown(KeyCode.W))
        {
            _helicopterController.MaximizeEngineForce();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _helicopterController.TurnOffEngine();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _helicopterController.ManualTurnRight();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _helicopterController.ManualTurnLeft();
        }
    }
}
