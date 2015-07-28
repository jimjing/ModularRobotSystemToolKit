﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class ModuleStateObject {

    [XmlAttribute("name")]
    public string name;
    public Vector3 position;
    public Quaternion rotation;

    [XmlArray("JointCommands")]
    [XmlArrayItem("JointCommand")]
    public List<JointCommandObject> listOfJointCommands;

	[XmlArray("PartStates")]
	[XmlArrayItem("PartState")]
	public List<PartStateObject> listOfPartStates;

    public ModuleStateObject () {
        name = "";
        listOfJointCommands = new List<JointCommandObject> ();
		listOfPartStates = new List<PartStateObject> ();
    }
}