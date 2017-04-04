using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class ChangeStateButton : MonoBehaviour 
{
	[SerializeField]
	private ActionType state;
	[SerializeField]
	private CommandViewController viewController;
	private Button button;

	private void Awake ()
	{
		button = GetComponent<Button> ();
		button.onClick.AddListener (SetState);
	}

	private void SetState ()
	{
		viewController.SetState (state);
	}
}
