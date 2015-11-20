using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void Update () {
		
		if (!GetComponent<ParticleSystem>().IsAlive()) Destroy (gameObject);
	}
}
