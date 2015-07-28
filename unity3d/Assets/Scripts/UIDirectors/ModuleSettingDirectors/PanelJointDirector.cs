﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelJointDirector : MonoBehaviour {

    private InputField inputFieldJointAngle;
    private Slider sliderJointAngle;
    private string jointName;
    private ModuleMotionController moduleMotionController;
	public Text unit;
	public Toggle toggleCmdType;

    public float initialAngle;

	public JointCommandObject.CommandTypes cmdType;

	// Use this for initialization
	void Start () {
		cmdType = JointCommandObject.CommandTypes.Position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake () {
        inputFieldJointAngle = gameObject.GetComponentInChildren<InputField> ();
        sliderJointAngle = gameObject.GetComponentInChildren<Slider> ();

        SetJointAngle (initialAngle);
    }

    public void UpdateText (float newValue) {
        inputFieldJointAngle.text = newValue.ToString();
    }

    public void UpdateSliderValue (string newValue) {
        sliderJointAngle.value = float.Parse(newValue);
    }

    public void UpdateSliderValue (float newValue) {
        sliderJointAngle.value = newValue;
    }

    public void SetJointAngle (float newValue) {
        UpdateText (newValue);
        UpdateSliderValue (newValue);
    }

    public void SetJointInfo (ModuleMotionController mmc, string name) {
        moduleMotionController = mmc;
        jointName = name;
		if (moduleMotionController.jointCommandObjectDict[jointName].commandType == JointCommandObject.CommandTypes.Velocity) {
			if (toggleCmdType != null) {
				toggleCmdType.isOn = true;
			}
		}
		else if (moduleMotionController.jointCommandObjectDict[jointName].commandType == JointCommandObject.CommandTypes.Position){
			if (toggleCmdType != null) {
				toggleCmdType.isOn = false;
			}
		}
        SetJointAngle (mmc.GetJointValue (jointName));
    }

    public void ResetJointInfo () {
        moduleMotionController = null;
        jointName = null;
        SetJointAngle (initialAngle);
    }

    public void OnUserChangeJointAngle (float newValue) {
		if (cmdType == JointCommandObject.CommandTypes.Position) {
			moduleMotionController.UpdateJointAngle (newValue, jointName);
		}
		else if (cmdType == JointCommandObject.CommandTypes.Velocity) {
			moduleMotionController.UpdateJointVelocity (newValue, jointName);
		} 
    }

    public void OnUserChangeJointAngle (string newValue) {
		if (cmdType == JointCommandObject.CommandTypes.Position) {
			moduleMotionController.UpdateJointAngle (float.Parse (newValue), jointName);
		}
		else if (cmdType == JointCommandObject.CommandTypes.Velocity) {
			moduleMotionController.UpdateJointVelocity (float.Parse (newValue), jointName);
		}
    }

	public void OnToggleJointCmdMode (bool value) {
		if (value) {
			cmdType = JointCommandObject.CommandTypes.Velocity;
			unit.text = "Deg/s";
		}
		else {
			cmdType = JointCommandObject.CommandTypes.Position;
			unit.text = "Degree";
		}
	}

}
