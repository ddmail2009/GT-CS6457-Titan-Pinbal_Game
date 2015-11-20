using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    void Start () {
        Vector3 center = transform.position;
        float radius = 5.0f;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (Collider collider in hitColliders)
        {
            Debug.Log(collider.gameObject.tag);
            if (collider.gameObject.tag == "Player") {
                PlayerHealthManager.instance.Heal(-10);
            }
            else if (collider.gameObject.tag == "PlayerItem") {
                Destroy(collider.gameObject);
            }
        }
    }

	void Update () {
		
		if (!GetComponent<ParticleSystem>().IsAlive()) Destroy (gameObject);
	}
}
