using UnityEngine;
using System.Collections;

using ProBuilder2.Common;
using ProBuilder2.Math;
using ProBuilder2.MeshOperations;

public class Selector : MonoBehaviour {

	public Camera camera;
	pb_Object pb;

	public GameObject wall;
	public GameObject floor;

	void Start () {

	}

	private enum Face {None, Up, Down, Left, Right, Front, Back}

	private Face getHitFace (RaycastHit hit){
		Vector3 incomingVec = hit.normal - Vector3.up;
		if (incomingVec == new Vector3 (0, -1, -1)) {return Face.Back;}
		if (incomingVec == new Vector3 (0, -1, 1)) {return Face.Front;}
		if (incomingVec == new Vector3 (0, 0, 0)) {return Face.Up;}
		if (incomingVec == new Vector3 (1, 1, 1) || incomingVec == new Vector3 (0, -2, 0)) {return Face.Down;}
		if (incomingVec == new Vector3 (-1, -1, 0)) {return Face.Left;}
		if (incomingVec == new Vector3 (1, -1, 0)) {return Face.Right;}
		return Face.None;
	}

	private Vector3 calcuatePosition (Vector3 vec, Face face){
		float xOff = 0;
		float yOff = 0;
		float zOff = 0;

		if (face == Face.Up) {
			zOff++;
		} else if (face == Face.Down) {
			zOff++;
			yOff--;
		} else if (face == Face.Right) {
			zOff++;
		} else if (face == Face.Left) {
			xOff--;
			zOff++;
		} else if (face == Face.Front) {
			zOff++;
		}
		return new Vector3 (Mathf.Floor(vec.x + xOff), Mathf.Floor(vec.y + yOff), Mathf.Floor(vec.z + zOff));
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = new Ray (camera.transform.position, camera.transform.forward);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.tag == "Wall") {
				GetComponent<MeshRenderer> ().enabled = true;
				transform.position = calcuatePosition (hit.point, getHitFace (hit));
			} else {
				GetComponent<MeshRenderer> ().enabled = false;
			}
		}

		if (GetComponent<MeshRenderer> ().enabled) {
			if (Input.GetMouseButtonDown (0)) {

				GameObject wallClone = (GameObject) Instantiate (wall, transform.position, transform.rotation);

				//pb = pb_ShapeGenerator.CubeGenerator (new Vector3(2,3,1));
				//pb.gameObject.transform.position = new Vector3(transform.position.x + .5f, transform.position.y + .5f, transform.position.z + .5f - 1f);
				//pb.gameObject.AddComponent<MeshCollider> ();
				//pb.gameObject.tag = "Wall";
			}
		}
	}
}
