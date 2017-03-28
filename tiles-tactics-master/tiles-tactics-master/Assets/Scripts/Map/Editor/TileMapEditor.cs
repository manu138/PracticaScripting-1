using UnityEditor;
using UnityEngine;

namespace Map.Detail
{
    [CustomEditor (typeof (Tilemap))]
    public class TileMapEditor: Editor
    {
        [SerializeField]
        private Tile prefab;

        public override void OnInspectorGUI ()
        {   
            serializedObject.Update ();
            DrawPropertiesExcluding (serializedObject, "tiles");
            prefab = (Tile)EditorGUILayout.ObjectField ("Tile", prefab, typeof (Tile), false);
            if (GUILayout.Button("Generate"))
            {
                Clear();
                SetTileRows(Generate());
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void Clear()
        {
            Transform transform = ((Tilemap)target).transform;
            foreach (Transform child in transform)
                DestroyImmediate(child.gameObject);
        }

        private TileRow[] Generate ()
        {
            TileRow[] rows = new TileRow[((Tilemap)target).height];
            float tileSize = serializedObject.FindProperty ("tileSize").floatValue;
            Transform parent = ((Tilemap)target).transform;
            for (int y=0; y < rows.Length; y++)
                rows[y] = GenerateRow (y, tileSize, parent);
            return rows;
        }

        private void SetTileRows (TileRow[] tiles)
        {
            Tilemap map = (Tilemap)target;
            map.rows = tiles;
        }

        private TileRow GenerateRow (int y, float tileSize, Transform parent)
        {
            TileRow row = new TileRow ();
            Tile[] tiles = new Tile[((Tilemap)target).width];
            for (int x=0; x<tiles.Length; x++)
                tiles[x] = InstantiateTile (new Vector2 (x, y), tileSize, parent);
            row.tiles = tiles;
            return row;
        }

        private Tile InstantiateTile (Vector2 position, float tileSize, Transform parent)
        {
            Vector2 worldPosition = position * tileSize;
            Tile tile = Instantiate (prefab, worldPosition, Quaternion.identity, parent);
            return tile;
        }
    }
}