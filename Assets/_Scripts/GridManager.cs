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
        [SerializeField]
        private float m_CellSize = 10;
        
        private TileBehaviour[,] m_Tiles;

        public TileBehaviour TilePrefab;

        private void Awake()
        {
            m_Tiles = new TileBehaviour[m_Width, m_Height];

            TileBehaviour tile;

            for (int x = 0; x < m_Width; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {
                    tile = Instantiate(TilePrefab, transform);
                    tile.transform.localPosition = new Vector3(x * m_CellSize, y * m_CellSize, 0);

                    tile.Init(x, y, m_CellSize);
                    m_Tiles[x, y] = tile;
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    var gridPos = ConvertToGrid(hit.point);
                    var tile = m_Tiles[(int)gridPos.x, (int)gridPos.y];
                    tile.Value++;
                }
            }
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

        private Vector3 ConvertToGrid(Vector3 wordPos)
        {
            var x = Mathf.RoundToInt(wordPos.x / m_CellSize);
            var y = Mathf.RoundToInt(wordPos.y / m_CellSize);

            return new Vector3(x, y, 0);
        }

        private Vector3 ConvertToWorld(int x, int y)
        {
            return new Vector3(x * m_CellSize, y * m_CellSize, 0);
        }
    }
}
