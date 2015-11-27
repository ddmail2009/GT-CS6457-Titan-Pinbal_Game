using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	public float detonateTime = 3f;
	public GameObject explosionBomb;
	
	void Start ()
	{
		Invoke ("destroyBomb", detonateTime);
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			gameObject.GetComponent<Collider> ().isTrigger = false;
		}
	}
	
	void destroyBomb ()
	{
		Instantiate (explosionBomb, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
