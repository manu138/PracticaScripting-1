using Characters;
using UnityEngine;

public class Controller: MonoBehaviour
{
    [SerializeField]
    private Character character;

    private void Update ()

    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tile = new Vector2(pz.x, pz.y);
        if (Input.GetKeyDown (KeyCode.Space))
            character.MoveTo (tile);
    }
}