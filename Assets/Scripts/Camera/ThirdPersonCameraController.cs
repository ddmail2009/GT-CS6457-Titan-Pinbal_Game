/**
 * Team Titan
 *
 * Meng-Hsin Tung
 * Based on http://www.3dbuzz.com/training/view/3rd-person-character-system
 **/

using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController: MonoBehaviour
{
	public Transform targetLookAt;

	public float distance = 5.0f;
	public float distanceMin = 3.0f, distanceMax = 10.0f;
	public float distanceSmooth = 0.05f;

	public float smoothX = 0.05f, smoothY = 0.1f;
	public float mouseSensitivityX = 5.0f, mouseSensitivityY = 5.0f;
	public float mouseWheelSensitivity = 5.0f;
	public float minLimitY = -40.0f, maxLimitY = 80.0f;

	public float occlusionDistanceStep = 0.5f;
	public int maxOcclusionChecks = 10;

	float mouseX = 0f, mouseY = 0f;
	float velX = 0f, velY = 0f, velZ = 0f;
	float velDistance = 0f;
	float desiredDistance = 0f;

	Vector3 desiredPosition = Vector3.zero;
	Vector3 position = Vector3.zero;

	Camera currCamera;

	void Start ()
	{
		currCamera = GetComponent <Camera> ();
		distance = Mathf.Clamp (distance, distanceMin, distanceMax);

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
	}

	void HandleInput ()
	{
		mouseX += Input.GetAxis ("Mouse X") * mouseSensitivityX;
		mouseY -= Input.GetAxis ("Mouse Y") * mouseSensitivityY;

		mouseY = clampAngle (mouseY, minLimitY, maxLimitY);

		desiredDistance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * mouseWheelSensitivity,
	    	                           distanceMin, distanceMax);
	}

	void CalculateDesiredPosition ()
	{
		distance = Mathf.SmoothDamp (distance, desiredDistance, ref velDistance, distanceSmooth);

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

				if (distance < 0.25f) {
					distance = 0.25f;
				}
			} else {
				distance = nearestDistance - currCamera.nearClipPlane;
			}

			desiredDistance = distance;
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

	void UpdatePosition ()
	{
		float posX = Mathf.SmoothDamp (position.x, desiredPosition.x, ref velX, smoothX);
		float posY = Mathf.SmoothDamp (position.y, desiredPosition.y, ref velY, smoothY);
		float posZ = Mathf.SmoothDamp (position.z, desiredPosition.z, ref velZ, smoothX);

		position = new Vector3 (posX, posY, posZ);
		transform.position = position;
		transform.LookAt (targetLookAt);
	}

	float clampAngle (float angle, float angleMin, float angleMax)
	{
		return Mathf.Clamp (angle % 360, angleMin, angleMax);
	}
}
