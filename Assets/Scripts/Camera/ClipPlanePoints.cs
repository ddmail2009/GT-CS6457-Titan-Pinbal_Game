/**
 * Team Titan
 *
 * Meng-Hsin Tung
 * Based on http://www.3dbuzz.com/training/view/3rd-person-character-system
 **/

using UnityEngine;

public class ClipPlane
{
	public struct ClipPlanePoints
	{
		public Vector3 upperLeft, upperRight;
		public Vector3 lowerLeft, lowerRight;
	};

	public static ClipPlanePoints ClipPlaneAtNear (Camera camera, Vector3 pos)
	{
		var clipPlanePoints = new ClipPlanePoints ();

		var halfFOV = (camera.fieldOfView / 2) * Mathf.Deg2Rad;
		var aspect = camera.aspect;
		var distance = camera.nearClipPlane;
		var height = distance * Mathf.Tan (halfFOV);
		var width = height * aspect;

		clipPlanePoints.lowerLeft = pos - camera.transform.right * width;
		clipPlanePoints.lowerLeft -= camera.transform.up * height;
		clipPlanePoints.lowerLeft += camera.transform.forward * distance;

		clipPlanePoints.lowerRight = pos + camera.transform.right * width;
		clipPlanePoints.lowerRight -= camera.transform.up * height;
		clipPlanePoints.lowerRight += camera.transform.forward * distance;

		clipPlanePoints.upperLeft = pos - camera.transform.right * width;
		clipPlanePoints.upperLeft += camera.transform.up * height;
		clipPlanePoints.upperLeft += camera.transform.forward * distance;

		clipPlanePoints.upperRight = pos + camera.transform.right * width;
		clipPlanePoints.upperRight += camera.transform.up * height;
		clipPlanePoints.upperRight += camera.transform.forward * distance;

		return clipPlanePoints;
	}
}
