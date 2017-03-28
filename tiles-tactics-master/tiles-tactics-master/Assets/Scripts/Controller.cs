using Characters;
using UnityEngine;

public class Controller: MonoBehaviour
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private Vector2 tilePosition;

    private void Update ()

    {

        if (Input.GetKeyDown(KeyCode.Space))
            character.MoveTo(tilePosition);
           
    }
}