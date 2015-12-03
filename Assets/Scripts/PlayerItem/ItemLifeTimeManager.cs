/**
 * Team Titan
 * 
 * Meng-Hsin Tung
 */

using UnityEngine;
using System.Collections;

public class ItemLifeTimeManager : MonoBehaviour
{
	public ItemSpawnManager spawnManager;
	public int secToDestroy = 30;

	public void CancelSelfDestroy ()
	{
		CancelInvoke ();
		enabled = false;
	}

	void Awake ()
	{
		Invoke ("SelfDestroy", secToDestroy);
	}

	void SelfDestroy ()
	{
		Destroy (gameObject);
	}
	

	void OnDestroy ()
	{
		if (spawnManager) {
			spawnManager.ItemDestroyCallback ();
		}
	}
}
