using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class GridManager : MonoBehaviour
    {
        private TileBehaviour[,] m_Tiles;

        [NonSerialized]
        public int Width;
        [NonSerialized]
        public int Height;
        [NonSerialized]
        public float CellSize = 1f;

        public TileBehaviour TilePrefab;

        public Vector3 Offset { get; } = new Vector3(-2.25f, -2f, 0);

        private static GridManager m_Instance = null;
        public static GridManager Instance => m_Instance;

        private void Awake()
        {
            m_Instance = this;
        }

        private void Start()
        {
            Width = 8;
            Height = 5;

            m_Tiles = new TileBehaviour[Width, Height];

            TileBehaviour tile;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    tile = Instantiate(TilePrefab, transform);
                    tile.transform.localPosition = new Vector3(x * CellSize, y * CellSize, 0) + Offset;

                    tile.Init(x, y, CellSize);
                    m_Tiles[x, y] = tile;
                }
            }
        }

        private void OnDestroy()
        {
            m_Instance = null;
        }

        private void SetValue(int x, int y, int value)
        {
            var tile = m_Tiles[x, y];
            tile.Value = value;
        }

        private void SetValue(Vector3 worldPos, int value)
        {
            var gridPos = ConvertToGrid(worldPos - Offset);
            SetValue((int)gridPos.x, (int)gridPos.y, value);
        }

        public Vector3 ConvertToWorld(int x, int y)
        {
            return new Vector3(x * CellSize, y * CellSize, 0) - Offset;
        }

        public Vector3 ConvertToGrid(Vector3 wordPos)
        {
            var pos = wordPos - Offset;
            var x = Mathf.RoundToInt(pos.x / CellSize);
            var y = Mathf.RoundToInt(pos.y / CellSize);

            return new Vector3(x, y, 0);
        }

        public TileBehaviour ConvertToTile(Vector3 wordPos)
        {
            var pos = wordPos - Offset;

            var x = Mathf.RoundToInt(pos.x / CellSize);
            var y = Mathf.RoundToInt(pos.y / CellSize);

            if (m_Tiles.GetLength(0) < x + 1 ||
                m_Tiles.GetLength(1) < y + 1 ||
                x < 0 ||
                y < 0)
            {
                return null;
            }

            return m_Tiles[x, y];
        }
    }
}
