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
        Vector2 targetPosition;
        private TileMovement movement;             
        public Tilemap tileMap;
     

     [SerializeField]
	private float minRange = 1f;
	[SerializeField]
	private float maxRange = 5f;
	[SerializeField]
	private float shootRadius = 1f;
	[SerializeField]
	private float shootForce = 2f;
	[SerializeField]
	private LayerMask enemies;
	
        public void Update()
        {
           
            if (Input.GetMouseButtonDown(1))
            {
             
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               targetPosition = new Vector2(Mathf.Abs(Mathf.Floor(mousePos.x)),Mathf.Abs(Mathf.Floor( mousePos.y)));
                Debug.Log(targetPosition);
            }
            
         }   
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
        public void Move( Action onFinished)
        {
          
            Vector2 currentPosition = tileMap.WorldToTilePosition(transform.position);
            Point from = MakePoint(currentPosition);
            if(from==null )
            {
                return;
            }
            Point to = MakePoint(targetPosition);
            if (to == null) return;
            if (from == to) return;
           
            List<Point> points = Pathfinding.FindPath(tileMap.grid, from, to);
            if (points == null) return;
            List<Vector2> path = points.ConvertAll<Vector2>(MakeVector);
            if (path == null) return;
            movement.Move(path);
          
            StartCoroutine(WaitForMoveCompleted(onFinished));
        
        }
        public void Attack(Vector2 tilePosition, Action onFinished)
        {
          
            Vector2 direction = new Vector2();
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 startPosition = pos + direction * (minRange - shootRadius - 0.01f);
            float raycastDistance = maxRange - minRange;
            RaycastHit2D hitResult = Physics2D.CircleCast(transform.position, shootRadius, direction,enemies);
            if(hitResult.rigidbody!=null)
            {
                Vector2 hitdirection = new Vector2();
                hitdirection = (hitResult.point - startPosition).normalized;
                float multiplier = ((1f - hitResult.distance / raycastDistance) * shootForce);
                Vector2 force = hitdirection * multiplier;
              
                hitResult.rigidbody.AddForce(force);
                Debug.Log("Hit");
            }
               
               
            Debug.Log("attack");
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