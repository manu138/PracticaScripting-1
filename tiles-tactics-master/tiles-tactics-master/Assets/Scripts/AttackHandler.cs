using UnityEngine;

public class AttackHandler : MonoBehaviour 
{
	[SerializeField]
	private float minRange = 1f;
	[SerializeField]
	private float maxRange = 5f;
	[SerializeField]
	private float shootRadius = 1f;
	[SerializeField]
	private float shootForce = 2f;
	[SerializeField]
	private LayerMask enemies;
	

	public void Shoot (Vector3 direction)
	{
		RaycastHit hit;
		Vector3 startPosition = transform.position + direction * (minRange - shootRadius - 0.01f);
		float raycastDistance = maxRange - minRange;
		if (Physics.SphereCast (transform.position, shootRadius, direction, out hit, raycastDistance, enemies))
		{
			Debug.Log ("Hit");
			Vector3 hitDirection = (hit.point - startPosition).normalized;
			hitDirection.y = 0f;
			float multiplier = ((1f - hit.distance / raycastDistance) * shootForce);
			Vector3 force = hitDirection * multiplier;
			hit.rigidbody.AddForce (force);
		}
	}

	public void Explode ()
	{
		
	}

	private void OnDrawGizmos()
	{
		Vector3 startPosition = transform.position + transform.forward * (minRange - shootRadius - 0.01f);
		float raycastDistance = maxRange - minRange;
		Vector3 endPosition = startPosition + transform.forward * raycastDistance;
		Debug.DrawLine (startPosition, endPosition);
		Gizmos.DrawWireSphere (endPosition, shootRadius);
	}
}
