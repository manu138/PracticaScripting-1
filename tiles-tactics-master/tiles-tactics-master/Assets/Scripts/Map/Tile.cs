using Characters;
using UnityEngine;

namespace Map
{
    public class Tile: MonoBehaviour
    {
        [SerializeField]
        private TerrainType terrain;
        public Character Character { get; set; }
        public Renderer rend;
       

        public float Cost
        {
            get
            {
                //TODO: Get Cost
                return 1f;
            }
        }
        private void Start()
        {
            rend = GetComponent<Renderer>();

        }
     
        public bool IsOccupied
        {
            get
            {
                return Character != null;
            }
        }
        private void OnMouseOver()
        {
           
            rend.material.color = Color.red;
        }
        void OnMouseExit()
        {
            rend.material.color = Color.white;
        }
    }
}