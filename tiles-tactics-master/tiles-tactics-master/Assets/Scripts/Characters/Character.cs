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
            RaycastHit hit;
            Vector3 direction = new Vector3();
            Vector3 startPosition = transform.position + direction * (minRange - shootRadius - 0.01f);
            float raycastDistance = maxRange - minRange;
            if (Physics.SphereCast(transform.position, shootRadius, direction, out hit, raycastDistance, enemies))
            {
                Debug.Log("Hit");
                Vector3 hitDirection = (hit.point - startPosition).normalized;
                hitDirection.y = 0f;
                float multiplier = ((1f - hit.distance / raycastDistance) * shootForce);
                Vector3 force = hitDirection * multiplier;
                hit.rigidbody.AddForce(force);
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