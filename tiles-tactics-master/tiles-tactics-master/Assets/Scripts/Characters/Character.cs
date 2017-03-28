using System.Collections.Generic;
using UnityEngine;
using Map;
using System;

namespace Characters
{
    [RequireComponent (typeof (TileMovement))]
    public class Character: MonoBehaviour
    {
        private TileMovement movement;
        public PathFind.Grid grid;

        public void Start()
        {
     

        }

        private void Awake ()
        {
            movement = GetComponent<TileMovement> ();
        }
        private Vector2 MakeVector(PathFind.Point p)
        {
            return new Vector2(p.x, p.y);
        }
        public void MoveTo(Vector2 tile)
        {
            //TODO: pathfinding

            grid = Tilemap.grid;
            PathFind.Point _from = new PathFind.Point((int)tile.x,(int) tile.y);
            PathFind.Point _to = new PathFind.Point(3, 6);
          
            List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to);
            
            List<Vector2> lp = path.ConvertAll<Vector2>(MakeVector);




            movement.Move (lp);
        }

        private static List<Vector2> GetDummyPath ()
        {
          
            List<Vector2> path = new List<Vector2> ();
            path.Add (new Vector2(0, 0));
            path.Add (new Vector2(0, 1));
            path.Add (new Vector2(1, 1));
            path.Add (new Vector2(2, 1));
            path.Add (new Vector2(2, 2));
            path.Add (new Vector2(2, 3));
            return path;
        }
    }
}