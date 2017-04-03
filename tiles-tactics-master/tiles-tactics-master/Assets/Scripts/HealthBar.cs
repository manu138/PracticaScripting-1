using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Image))]
public class HealthBar : MonoBehaviour 
{

	[SerializeField]
	private Health health;
	private Image image;

	private void Awake ()
	{
		image = GetComponent<Image> ();
	}

	void Start () 
	{
		health.OnDamage += UpdateSize;	
	}

	private void UpdateSize (float percentage)
	{
		image.fillAmount = percentage;
	}
}
