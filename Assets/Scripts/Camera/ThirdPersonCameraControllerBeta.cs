/**
 * Team Titan
 *
 * Meng-Hsin Tung
 * Based on http://www.3dbuzz.com/training/view/3rd-person-character-system
 **/

using UnityEngine;
using System.Collections;

public class ThirdPersonCameraControllerBeta: MonoBehaviour
{
	public Transform targetLookAt;

	public float distance = 5.0f;
	public float distanceMin = 3.0f, distanceMax = 10.0f;
	public float distanceSmooth = 0.05f;
	public float distanceResumeSmooth = 0.1f;
	
	public float smoothX = 0.05f, smoothY = 0.1f;
	public float mouseSensitivityX = 5.0f, mouseSensitivityY = 5.0f;
	public float mouseWheelSensitivity = 5.0f;
	public float minLimitY = -40.0f, maxLimitY = 80.0f;

	public float occlusionDistanceStep = 0.05f;
	public int maxOcclusionChecks = 50;

	float mouseX = 0f, mouseY = 0f;
	float velX = 0f, velY = 0f, velZ = 0f;
	float velDistance = 0f;
	float desiredDistance = 0f;
	float currDistanceSmooth;
	float preOccludedDistance;

	Vector3 desiredPosition = Vector3.zero;
	Vector3 position = Vector3.zero;

	Camera currCamera;

	void Start ()
	{
		currCamera = GetComponent <Camera> ();
		distance = Mathf.Clamp (distance, distanceMin, distanceMax);

		Reset ();
		Cursor.visible = false;
	}

	void LateUpdate ()
	{
		HandleInput ();

		int count = 0;
		do {
			CalculateDesiredPosition ();
			count += 1;
		} while (CheckIfOccluded(count));

		UpdatePosition ();

		// Debug.DrawLine (targetLookAt.transform.position, transform.position, Color.magenta);
	}

	void HandleInput ()
	{
		mouseX += Input.GetAxis ("Mouse X") * mouseSensitivityX;
		mouseY -= Input.GetAxis ("Mouse Y") * mouseSensitivityY;

		mouseY = clampAngle (mouseY, minLimitY, maxLimitY);

		var deadZone = 0.01f;

		if (Input.GetAxis ("Mouse ScrollWheel") < -deadZone || Input.GetAxis ("Mouse ScrollWheel") > deadZone) {
			desiredDistance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * mouseWheelSensitivity,
			                               distanceMin, distanceMax);
			preOccludedDistance = desiredDistance;
			currDistanceSmooth = distanceSmooth;
		}
	}

	void CalculateDesiredPosition ()
	{
		ResetDesiredDistance ();
		distance = Mathf.SmoothDamp (distance, desiredDistance, ref velDistance, currDistanceSmooth);

		desiredPosition = CalculatePosition (mouseY, mouseX, distance);
	}

	Vector3 CalculatePosition (float rotationX, float rotationY, float distance)
	{
		Vector3 direction = new Vector3 (0, 0, -distance);
		// direction = Vector3.ProjectOnPlane (direction, targetLookAt.up);
		Quaternion rotation = Quaternion.Euler (rotationX, rotationY, 0);

		return targetLookAt.position + rotation * direction;
	}

	bool CheckIfOccluded (int count)
	{
		bool isOccluded = false;

		float nearestDistance = CheckCameraPoints (targetLookAt.position, desiredPosition);

		if (nearestDistance != -1) {
			if (count < maxOcclusionChecks) {
				isOccluded = true;
				distance -= occlusionDistanceStep;
			} else {
				distance = nearestDistance - currCamera.nearClipPlane;
			}

			if (distance < 0.5f) {
				distance = 0.5f;
				isOccluded = false;
			}

			desiredDistance = distance;
			currDistanceSmooth = distanceResumeSmooth;
		}

		return isOccluded;
	}

	float CheckCameraPoints (Vector3 from, Vector3 to)
	{
		float nearestDistance = -1f;

		RaycastHit hitInfo;

		ClipPlane.ClipPlanePoints clipPlanePoints = ClipPlane.ClipPlaneAtNear (currCamera, to);

		// Debug code to visualize
		Debug.DrawLine (from, to + transform.forward * -currCamera.nearClipPlane, Color.red);
		Debug.DrawLine (from, clipPlanePoints.upperLeft);
		Debug.DrawLine (from, clipPlanePoints.upperRight);
		Debug.DrawLine (from, clipPlanePoints.lowerLeft);
		Debug.DrawLine (from, clipPlanePoints.lowerRight);

		Debug.DrawLine (clipPlanePoints.upperLeft, clipPlanePoints.upperRight);
		Debug.DrawLine (clipPlanePoints.upperLeft, clipPlanePoints.lowerLeft);
		Debug.DrawLine (clipPlanePoints.lowerLeft, clipPlanePoints.lowerRight);
		Debug.DrawLine (clipPlanePoints.upperRight, clipPlanePoints.lowerRight);

		if (Physics.Linecast (from, clipPlanePoints.upperLeft, out hitInfo) && hitInfo.collider.tag != "Player") {
			nearestDistance = hitInfo.distance;
		}

		if (Physics.Linecast (from, clipPlanePoints.lowerLeft, out hitInfo) && hitInfo.collider.tag != "Player") {
			if (hitInfo.distance < nearestDistance || nearestDistance == -1) {
				nearestDistance = hitInfo.distance;
			}
		}

		if (Physics.Linecast (from, clipPlanePoints.upperRight, out hitInfo) && hitInfo.collider.tag != "Player") {
			if (hitInfo.distance < nearestDistance || nearestDistance == -1) {
				nearestDistance = hitInfo.distance;
			}
		}

		if (Physics.Linecast (from, clipPlanePoints.lowerRight, out hitInfo) && hitInfo.collider.tag != "Player") {
			if (hitInfo.distance < nearestDistance || nearestDistance == -1) {
				nearestDistance = hitInfo.distance;
			}
		}

		if (Physics.Linecast (from, to + transform.forward * -currCamera.nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player") {
			if (hitInfo.distance < nearestDistance || nearestDistance == -1) {
				nearestDistance = hitInfo.distance;
			}
		}

		return nearestDistance;
	}

	void ResetDesiredDistance ()
	{
		if (desiredDistance < preOccludedDistance) {
			var pos = CalculatePosition (mouseY, mouseX, preOccludedDistance);

			var nearestDistance = CheckCameraPoints (targetLookAt.position, pos);

			if (nearestDistance == -1 || nearestDistance > preOccludedDistance) {
				desiredDistance = preOccludedDistance;
			} else {
				desiredDistance = nearestDistance;
				if (desiredDistance < 0.5f) {
					desiredDistance = 0.5f;
				}
			}
		}
	}

	void UpdatePosition ()
	{
		float posX = Mathf.SmoothDamp (position.x, desiredPosition.x, ref velX, smoothX);
		float posY = Mathf.SmoothDamp (position.y, desiredPosition.y, ref velY, smoothY);
		float posZ = Mathf.SmoothDamp (position.z, desiredPosition.z, ref velZ, smoothX);

		position = new Vector3 (posX, posY, posZ);
		transform.position = position;
		transform.LookAt (targetLookAt);
	}

	void Reset ()
	{
		mouseX = 0;
		mouseY = 10;

		desiredDistance = distance;
		preOccludedDistance = distance;
		currDistanceSmooth = distanceSmooth;
	}

	float clampAngle (float angle, float angleMin, float angleMax)
	{
		return Mathf.Clamp (angle % 360, angleMin, angleMax);
	}
}
