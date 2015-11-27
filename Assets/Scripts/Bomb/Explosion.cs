using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
	public int playerDamage = 10;

	ParticleSystem ps;

	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();

		Vector3 center = transform.position;
		float radius = 2.0f;
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);

		foreach (Collider collider in hitColliders) {
			if (collider.gameObject.tag == "Player") {
				PlayerHealthManager.instance.Heal (-playerDamage);
			} else if (collider.gameObject.tag == "PlayerItem") {
				Destroy (collider.gameObject);
			} else if (collider.gameObject.tag == "Box") {
				Vector3 spawnLocation = collider.transform.position;
				GameObject newItem = collider.GetComponent<ItemDrop> ().GetItem ();

				spawnLocation.y = 0.5f;
				Instantiate (newItem, spawnLocation, newItem.transform.rotation);
				Destroy (collider.gameObject);
			}
		}
	}

	void Update ()
	{
		if (!ps.IsAlive ()) {
			Destroy (gameObject);
		}
	}
}
