using UnityEngine;
using System.Collections;

public class CharacterShockWave : MonoBehaviour
{
	public float velocityChange = 50f;
	public GameObject explodeAnimationPrefab = null;
	public Vector3 posOffset;
	public GameObject Ground;
	public Manabar bar;
	public AudioClip clip;
	public float manaCost = 0.5f;

	void Update ()
	{
		if (bar.Value >= manaCost) {
			bar.SetNewValue (bar.Value - manaCost);
			GameObject obj = (GameObject)Instantiate (explodeAnimationPrefab, transform.position, Ground.transform.rotation);
			obj.transform.parent = transform;
			obj.transform.localPosition = posOffset;
			obj.GetComponent<AudioSource> ().playOnAwake = true;
			obj.GetComponent<AudioSource> ().clip = clip;
			obj.GetComponent<AudioSource> ().Play ();
		}
		this.enabled = false;
	} 
}
