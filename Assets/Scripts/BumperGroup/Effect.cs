using UnityEngine;

public abstract class Effect : MonoBehaviour {
	protected BumperManager manager;
	public abstract void BeginEffect(BumperManager mgr);
}
