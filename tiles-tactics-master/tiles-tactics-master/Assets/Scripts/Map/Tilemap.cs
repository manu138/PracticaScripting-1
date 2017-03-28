using PathFind;
using UnityEngine;

namespace Map
{
	public class Tilemap : MonoBehaviour 
	{
		public TileRow[] rows;
        public float tileSize = 1f;
        public int height = 8;
        public int width = 8;
        public float[,] costs;
        public Grid grid;

        public void Start()
        {
            costs = BuildCosts();

            grid = new Grid(width, height, costs);
        }

        public Vector2 TileToWorldPosition (Vector2 position)
		{
			return position * tileSize;
		}

		public Vector2 WorldToTilePosition (Vector2 position)
		{
			return position / tileSize;
		}

		public Tile GetTileAt (Vector2 position)
		{
			return rows[(int)position.y].tiles[(int)position.x];
		}

        private float[,] BuildCosts()
        {
            int width = rows[0].tiles.Length;
            int height = rows.Length;
            float[,] costs = new float[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    costs[j, i] = 1f;
            return costs;
        }
	}
}