using UnityEngine;
using System.Collections;

public class BallDestroy : MonoBehaviour
{
	public void DestroyAfter (float sec)
	{
		Invoke ("DestroyThis", sec);
	}

	void DestroyThis ()
	{
		Debug.Log ("QQ");
	}
}
