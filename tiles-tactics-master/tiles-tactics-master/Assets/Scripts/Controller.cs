using Characters;
using UnityEngine;

public class Controller: MonoBehaviour
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private Vector2 tilePosition;
    private Vector3 mousePos;
    public void start()
    {
       
    }
    private void Update ()

    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilePosition = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            character.MoveTo(tilePosition);
        }
           
           
    }
}