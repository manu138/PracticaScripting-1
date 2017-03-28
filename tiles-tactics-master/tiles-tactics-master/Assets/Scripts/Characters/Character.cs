using System.Collections.Generic;
using UnityEngine;
using Map;
using System;
using PathFind;

namespace Characters
{
    [RequireComponent (typeof (TileMovement))]
    public class Character: MonoBehaviour
    {
        private TileMovement movement;
        public Tilemap tileMap;
      
        private void Awake ()
        {
            movement = GetComponent<TileMovement> ();
        }

        public void MoveTo(Vector2 tilePositon)
        {
            Vector2 currentPosition = tileMap.WorldToTilePosition(transform.position);
            Point from = MakePoint(currentPosition);
            Point to = MakePoint(tilePositon);
          
            List<Point> points = Pathfinding.FindPath(tileMap.grid, from, to);  
            List<Vector2> path = points.ConvertAll<Vector2>(MakeVector);

            movement.Move (path);
        }

        private Vector2 MakeVector(Point point)
        {
            return new Vector2(point.x, point.y);
        }

        private Point MakePoint(Vector2 vector)
        {
            return new Point((int)vector.x, (int)vector.y);
        }
    }
}