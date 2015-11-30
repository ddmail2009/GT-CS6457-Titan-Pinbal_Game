using UnityEngine;
using System.Collections;

public class PortalIndicatorControl : MonoBehaviour
{
	public Material onMaterial, offMaterial;
	public AudioClip onSound, offSound;
	public bool initialOn = false;

	AudioSource source;
	MeshRenderer Mrenderer;
	bool status;
	void Start ()
	{
		source = GetComponent<AudioSource> ();
		Mrenderer = GetComponent<MeshRenderer> ();
		if (initialOn)
			setOn ();
		else
			setOff ();
	}

	public bool isOn ()
	{
		return status;
	}

	public void setOn ()
	{
		status = true;
		source.PlayOneShot (onSound);
		Mrenderer.material = onMaterial;
	}

	public void setOff ()
	{

		status = false;
		source.PlayOneShot (offSound);
		Mrenderer.material = offMaterial;
	}
}
