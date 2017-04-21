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
            //costos 1 caminable 0 no caminable 2 duro de caminar
            int width = rows[0].tiles.Length;
            int height = rows.Length;
            float[,] costs = new float[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    costs[j, i] = 1f;
                }

            costs[7, 4] = 0.0f;
           costs[4, 4] = 0.0f;
            costs[3, 4] = 0.0f;
            costs[5, 4] = 0.0f;
            costs[6, 4] = 0.0f;
            costs[7, 4] = 0.0f;
            costs[3, 0] = 2.0f;
            costs[2, 1] = 2.0f;
            costs[3, 2] = 2.0f;
            costs[3, 3] = 2.0f;
            costs[2, 4] = 2.0f;
            costs[2, 5] = 2.0f;
            costs[3, 5] = 2.0f;
            costs[3, 6] = 2.0f;
            costs[2, 6] = 2.0f;
            return costs;
        }
	}
}