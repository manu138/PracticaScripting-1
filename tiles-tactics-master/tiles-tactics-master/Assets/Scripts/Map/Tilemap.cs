using UnityEngine;

namespace Map
{
	public class Tilemap : MonoBehaviour 
	{
		public TileRow[] tiles;
        public float tileSize = 1f;
        public int height = 8;
        public int width = 8;
        public float[,] tilesmap;
        public static PathFind.Grid grid;

        public void Start()
        {
            tilesmap = new float[width, height];
           
            for (int i = 1; i < width; i++)
            {
                for (int j = 1; j < height; j++)
                {
                    tilesmap[i, j] = 1.0f;
                }
            }
            for (int i = 0; i < width; i++)
            {
                
                    tilesmap[0, i] = 0.0f;
                
            }
            for (int i = 0; i < height; i++)
            {

                tilesmap[i, 0] = 0.0f;

            }
            for (int i = 0; i < height; i++)
            {

                tilesmap[i, 7] = 0.0f;

            }

            grid = new PathFind.Grid(width, height, tilesmap);
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
			return tiles[(int)position.y].tiles[(int)position.x];
		}
	}
}