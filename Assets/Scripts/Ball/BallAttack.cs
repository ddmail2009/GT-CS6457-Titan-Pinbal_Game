/**
 * Team Titan
 *
 * Xiaoyu Chen, Meng-Hsin
 **/

using UnityEngine;
using System.Collections;

public class BallAttack : MonoBehaviour
{
	public int attackDamage = 40;
	public AudioClip effectClip;
	public GameObject burningEffectTemplate;
	
	bool isDetecting = true;
	Collider thisCol;
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag != "Player") {
			return;
		}
		
		if (PowerPelletManager.instance.isEnabled) {
			// Oops. Player is more powerful than ball now....
			Physics.IgnoreCollision (col.collider, gameObject.GetComponent<Collider> ());
			
			Rigidbody rig = gameObject.GetComponent<Rigidbody> ();
			rig.AddExplosionForce (50.0f, col.gameObject.transform.position, 1f, 0, ForceMode.VelocityChange);
			
			GameObject burningEffect = Instantiate (burningEffectTemplate, transform.position, burningEffectTemplate.transform.rotation) as GameObject;
			burningEffect.transform.parent = transform;
			burningEffect.GetComponent<ParticleSystem> ().Play ();
			
			AudioSource.PlayClipAtPoint (effectClip, transform.position, 1.0f);
			
			Destroy (gameObject, 2.5f);
		} else if (isDetecting) {
			VersusManager.instance.ChangeValueBy (-attackDamage);
			isDetecting = false;
			
			Invoke ("ResumeCollision", 3.0f);
		}
	}
	
	void ResumeCollision ()
	{
		isDetecting = true;
	}
}
