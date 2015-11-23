using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour
{
	public static PlayerHealthManager instance;

	public int startingHealth = 100;
	public int healthDropPerSecond = 1;
	public bool isDead = false;

	public Color minHealthColor, midHealthColor, maxHealthColor;

	public GameObject ragdollTemplate;

	int currentHealth;
	int midHealth;

	Slider healthSlider;
	Text healthText;
	Image healthSliderFill;

	void Awake ()
	{
		instance = this;

		healthSlider = GetComponent <Slider> ();

		healthSliderFill = transform.Find ("Slider").Find ("Fill").GetComponent <Image> ();
		healthText = transform.Find ("HealthText").GetComponent <Text> ();

		midHealth = startingHealth / 2;
		currentHealth = startingHealth;
		InvokeRepeating ("DropHealth", 1f, 1f);
	}
	
	void DropHealth ()
	{
		if (currentHealth <= 0) {
			CancelInvoke ("DropHealth");
			return;
		}

		if (BallManager.instance.numOfBalls <= 0) {
			return;
		}

		TakeDamage (healthDropPerSecond);
	}


	void Die ()
	{
		isDead = true;
		ReplacePlayerWithRagdoll ();
	}

	void ReplacePlayerWithRagdoll ()
	{
		GameObject player = GameObject.FindWithTag ("Player");
		ThirdPersonCameraControllerBeta playerCamera = GameObject.FindWithTag ("ThirdPersonCamera").GetComponent<ThirdPersonCameraControllerBeta> ();

		GameObject ragdoll = Instantiate (ragdollTemplate, player.transform.position, player.transform.rotation) as GameObject;
		playerCamera.targetLookAt = ragdoll.transform;

		Destroy (player);
	}


	void UpdateHUD ()
	{
		healthSlider.value = currentHealth;

		if (currentHealth >= midHealth) {
			float t = (float)(healthSlider.value - midHealth) / (startingHealth - midHealth);
			healthSliderFill.color = Color.Lerp (midHealthColor, maxHealthColor, t);
		} else {
			float t = (float)(healthSlider.value) / midHealth;
			healthSliderFill.color = Color.Lerp (minHealthColor, midHealthColor, t);
		}

		healthText.text = currentHealth + "/" + startingHealth;
	}

	public void TakeDamage (int amount)
	{
		currentHealth -= amount;
		UpdateHUD ();

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public void Heal (int amount)
	{
		if (currentHealth > 0) {
			currentHealth += amount;
			if (currentHealth > startingHealth) {
				currentHealth = startingHealth;
			}

			UpdateHUD ();
		}
	}
}
