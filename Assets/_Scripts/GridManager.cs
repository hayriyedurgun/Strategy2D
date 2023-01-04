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
        [SerializeField]
        private int m_Width = 5;
        [SerializeField]
        private int m_Height = 2;

        public float CellSize = 10;

        private TileBehaviour[,] m_Tiles;

        public TileBehaviour TilePrefab;

        private static GridManager m_Instance = null;
        public static GridManager Instance => m_Instance;

        private void Awake()
        {
            m_Instance = this;
            m_Tiles = new TileBehaviour[m_Width, m_Height];

            TileBehaviour tile;

            for (int x = 0; x < m_Width; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {
                    tile = Instantiate(TilePrefab, transform);
                    tile.transform.localPosition = new Vector3(x * CellSize, y * CellSize, 0);

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
            var gridPos = ConvertToGrid(worldPos);
            SetValue((int)gridPos.x, (int)gridPos.y, value);
        }

        private Vector3 ConvertToWorld(int x, int y)
        {
            return new Vector3(x * CellSize, y * CellSize, 0);
        }

        public Vector3 ConvertToGrid(Vector3 wordPos)
        {
            var x = Mathf.RoundToInt(wordPos.x / CellSize);
            var y = Mathf.RoundToInt(wordPos.y / CellSize);

            return new Vector3(x, y, 0);
        }

        public TileBehaviour ConvertToTile(Vector3 wordPos)
        {
            var x = Mathf.RoundToInt(wordPos.x / CellSize);
            var y = Mathf.RoundToInt(wordPos.y / CellSize);

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
