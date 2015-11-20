using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	
	float detonateTime = 3f;
	public GameObject explosionBomb;
	
	void Start () {
		
		StartCoroutine (destroyBomb());
	}
	
	void OnTriggerExit (Collider other) {

		if (other.tag == "Player") {

			gameObject.GetComponent<Collider>().isTrigger = false;
		}
	}
	
	IEnumerator destroyBomb() {
		
		yield return new WaitForSeconds(detonateTime);
		Destroy (this.gameObject);
		Instantiate (explosionBomb, transform.position, Quaternion.identity);
	}
}
