using System.Collections.Generic;
using UnityEngine;
using Map;
using System;
using PathFind;
using System.Collections;

namespace Characters
{
    [RequireComponent(typeof(Health))]
    [RequireComponent (typeof (TileMovement))]
    public class Character: MonoBehaviour
    {
        [SerializeField]
        private Vector2 tilePosition;
        private Vector3 mousePos;
        private TileMovement movement;
        public Tilemap tileMap;
      
        private void Awake ()
        {
            movement = GetComponent<TileMovement> ();
        }

        private IEnumerator WaitForMoveCompleted(Action onFinished)
        {
            while (movement.isMoving)
                yield return new WaitForSeconds(0.5f);
            onFinished();
        }
        public void Move(Vector2 tilePositon, Action onFinished)
        {
            Vector2 currentPosition = tileMap.WorldToTilePosition(transform.position);
            Point from = MakePoint(currentPosition);
            Point to = MakePoint(tilePositon);
          
            List<Point> points = Pathfinding.FindPath(tileMap.grid, from, to);  
            List<Vector2> path = points.ConvertAll<Vector2>(MakeVector);
            movement.Move(path);
            
            StartCoroutine(WaitForMoveCompleted(onFinished));
        
        }

        private Vector2 MakeVector(Point point)
        {
            return new Vector2(point.x, point.y);
        }
        public Vector2 MousePos()
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilePosition = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            return mousePos;
        }

        private Point MakePoint(Vector2 vector)
        {
            return new Point((int)vector.x, (int)vector.y);
        }
    }
}