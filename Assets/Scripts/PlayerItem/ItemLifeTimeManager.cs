using UnityEngine;
using System.Collections;

public class ItemLifeTimeManager : MonoBehaviour
{
	public ItemSpawnManager spawnManager;

	void OnDestroy ()
	{
		if (spawnManager) {
			spawnManager.ItemDestroyCallback ();
		}
	}
}
