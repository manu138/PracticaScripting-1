using UnityEngine;
using System;

public class Health : MonoBehaviour 
{
	public event Action<float> OnDamage;

	[SerializeField]
	private float maxLifePoints;

	public float LifePoints
	{
		get; private set;
	}

	private void Awake ()
	{
		LifePoints = maxLifePoints;
	}

	public void ApplyDamage (float damagePoints)
	{
		LifePoints -= damagePoints;
		if (OnDamage != null)
			OnDamage (LifePoints / maxLifePoints);
	}
}
