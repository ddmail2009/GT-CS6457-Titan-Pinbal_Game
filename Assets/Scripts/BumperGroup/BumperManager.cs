using UnityEngine;
using System.Collections;

public class BumperManager : MonoBehaviour {
	public Bumper[] bumpers;
	public Effect[] effects;
	public int threshold = 3;

	private int targetIndex = -1;
	private int counter = 0;

	// Use this for initialization
	void Start() {
		for (int i = 0; i < bumpers.Length; i++) {
			bumpers[i].index = i;
			bumpers[i].manager = this;
		}

		randomSelect();
	}

	public void onEffectEnd() {
		randomSelect();
	}

	void randomSelect() {
		if (targetIndex == -1) {
			targetIndex = Random.Range (0, bumpers.Length);
		} else {
			targetIndex = (targetIndex + Random.Range (1, bumpers.Length - 1)) % bumpers.Length;
		}
		bumpers[targetIndex].state = Bumper.BumperState.Targeted;

	}

	public void onBumperCollide(int index) {
		if (index == targetIndex) {
			bumpers[index].state = Bumper.BumperState.Idle;
			randomSelect();

			counter++;
			if (counter >= threshold) {
				for (int i = 0; i < bumpers.Length; i++) {
					bumpers[i].state = Bumper.BumperState.Idle;;
					targetIndex = -1;
					effects[Random.Range(0, effects.Length)].BeginEffect(this);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
