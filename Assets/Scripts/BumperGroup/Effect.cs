using UnityEngine;

public abstract class Effect : MonoBehaviour
{
	protected BumperManager manager;
	public void BeginEffect (BumperManager mgr)
	{
		manager = mgr; 
		enabled = true;
	}
}
