/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class ItemSpawnManager : MonoBehaviour
{
	public Transform attachTo;
	public GameObject itemTemplate;
	public float spawningTime = 8f;
	public int maxNumOfItems = 3;

	float planeDefultExtent = 5.0f;
	int numOfItems = 0;

	public void ItemDestroyCallback ()
	{
		numOfItems -= 1;
	}

	void Start ()
	{
		InvokeRepeating ("Spawn", spawningTime, spawningTime);
	}

	void Spawn ()
	{
		if (VersusManager.instance.isEnded) {
			CancelInvoke ("Spawn");
			return;
		} else if (!VersusManager.instance.isStarting) {
			return;
		} else if (numOfItems >= maxNumOfItems) {
			return;
		}

		Vector3 worldSpawningPoint;
		RaycastHit hitInfo;

		do {
			float extentX = Random.Range (-planeDefultExtent, planeDefultExtent);
			float extentZ = Random.Range (-planeDefultExtent, planeDefultExtent);

			Vector3 localSpawningPoint = new Vector3 (transform.localPosition.x + extentX, 
		                                          10.0f,
		                                          transform.localPosition.z + extentZ);
			worldSpawningPoint = transform.TransformPoint (localSpawningPoint);
		} while (!Physics.Raycast(worldSpawningPoint, -transform.up, out hitInfo) || hitInfo.collider.gameObject.name != "Plane");
	
		worldSpawningPoint = hitInfo.point + itemTemplate.transform.position.y * transform.up;

		GameObject newObj = Instantiate (itemTemplate, worldSpawningPoint, transform.rotation * itemTemplate.transform.rotation) as GameObject;
		newObj.transform.parent = attachTo;

		newObj.GetComponent<ItemLifeTimeManager> ().spawnManager = this;
		numOfItems += 1;
	}
}
