using System;
using System.Collections.Generic;
using UnityEngine;
using Characters;

public class CommandViewController : MonoBehaviour 
{
	[SerializeField]
	private Controller controller;
	[SerializeField]
	private Character character;
	[SerializeField]
	private LayerMask terrain;
	private UIStateBase current;
    private Dictionary<ActionType, UIStateBase> states;

    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    public int RemainingActionPoints
    {
        get
        {
            return controller.RemainingActionPoints;
        }
    }
    private void Awake ()
	{
        source = GetComponent<AudioSource>();
        states = new Dictionary<ActionType, UIStateBase> ();
	}

	private void Start ()
	{
		SetState (ActionType.None);
		states.Add (ActionType.Move, new MoveUIState (this));
        states.Add(ActionType.Attack, new AttackUIState(this));
    }	

	private void Update ()
	{
        if (current == null) return;
        Vector3 position = GetCurrentPosition();
        current.OnMousePositionChanged(position);
        if (Input.GetMouseButtonDown(0))
        {
            current.OnClick(position);
             float vol = UnityEngine.Random.Range (volLowRange, volHighRange);
            source.PlayOneShot(shootSound, vol);
        }
    }

    public void SetState(ActionType state)
    {
		if (current != null)
			current.Disable ();
		if (states.TryGetValue (state, out current))
			current.Enable ();
    }

	public bool TryAddAction (Command toAdd)
	{
		bool wasAdded = controller.TryAddAction (toAdd);
		Debug.Log (string.Format ("The command was added {1}: {0} - {2}", wasAdded, toAdd.GetType ().Name, toAdd.Cost));
		return wasAdded;
	}

	private Vector3 GetCurrentDirection()
	{
		Vector3 position = GetCurrentPosition ();
		Vector3 distance = position - character.transform.position;
		distance.y = 0f;
		return distance.normalized;
	}

	private Vector3 GetCurrentPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		bool didHit = Physics.Raycast (ray, out hit);
		return didHit? hit.point : character.transform.position; 
	}
    public void RunActions()
    {
        current = null;
        controller.ExecuteActions();
    }
    public void Undo()
    {
        
        controller.Undo();

    }
}