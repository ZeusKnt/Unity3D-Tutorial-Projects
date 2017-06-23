using UnityEngine;
using System.Collections;

public class testShoot : MonoBehaviour {

	public GameObject shell;
	public float shootPower;
	public Transform shotTransform;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot();
		}
	}

	void Shoot() {
		GameObject newShell = Instantiate (shell, shotTransform.position, shotTransform.rotation) as GameObject;
		Rigidbody r = newShell.GetComponent<Rigidbody> ();
		r.velocity = shotTransform.forward * shootPower;
	}
}
