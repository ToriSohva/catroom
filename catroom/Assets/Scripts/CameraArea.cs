using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraArea : MonoBehaviour {

	public CinemachineVirtualCamera areaCamera;

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("switch to " + areaCamera.gameObject.name);
		CameraTarget target = collider.GetComponent<CameraTarget> ();
		if (target != null) {
			CameraOverlord.instance.PromoteCamera (areaCamera);
		}
	}

}
