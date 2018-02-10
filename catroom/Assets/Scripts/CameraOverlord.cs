using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOverlord : MonoBehaviour {

	static CameraOverlord _instance;
	public static CameraOverlord instance {
		get { 
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<CameraOverlord> ();
			}
			return _instance;
		}
	}

	List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

	// Use this for initialization
	void Start () {
		cameras.AddRange(GameObject.FindObjectsOfType<CinemachineVirtualCamera>());
	}
	
	public void PromoteCamera(CinemachineVirtualCamera camera) {
		for (int i = 0; i < cameras.Count; i++) {
			cameras [i].enabled = camera == cameras [i];
		}
	}
}
