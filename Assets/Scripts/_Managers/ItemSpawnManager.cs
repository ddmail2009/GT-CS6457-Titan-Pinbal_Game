using UnityEngine;
using System.Collections;

public class ItemSpawnManager : MonoBehaviour
{
	public GameObject[] items;
	public float spawningTime = 8f;

	float planeDefultExtent = 5.0f;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawningTime, spawningTime);
	}

	void Spawn ()
	{
		if (PlayerHealthManager.instance.isDead == true) {
			CancelInvoke ("Spawn");
			return;
		}

		if (BallManager.instance.numOfBalls <= 0) {
			return;
		}

		for (int i = 0; i < items.Length; i++) {
			float extentX = Random.Range (-planeDefultExtent, planeDefultExtent);
			float extentZ = Random.Range (-planeDefultExtent, planeDefultExtent);

			Vector3 localSpawningPoint = new Vector3 (transform.localPosition.x + extentX, 
			                                          0.5f,
			                                          transform.localPosition.z + extentZ);
			Vector3 worldSpawningPoint = transform.TransformPoint (localSpawningPoint);


			GameObject newObj = Instantiate (items [i], worldSpawningPoint, transform.rotation * items [i].transform.rotation) as GameObject;
			newObj.transform.parent = transform.parent;
		}
	}
}
