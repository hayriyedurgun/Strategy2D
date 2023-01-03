using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class Grid
    {
        public TileBehaviour TilePrefab;

        private int m_Width;
        private int m_Height;
        private float m_CellSize;
        private List<TileBehaviour> m_Tiles;

        public Grid(int width, int height, float cellSize)
        {
            m_Width = width;
            m_Height = height;
            m_CellSize = cellSize;

            //m_Tiles = new int[width, height];

            //for (int x = 0; x < width; x++)
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        Debug.Log(ConvertToWorld(x, y));
            //        //Utils.CreateWorldText(m_GridArray[x,y].ToString(), null, GetWorldPos(x,y), Color.white, TextAnchor.MiddleCenter);
            //        //Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.red);
            //        //Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.red);
            //    }
            //}
        }

        //private void SetValue(int x, int y, int value)
        //{
        //    m_Tiles[x, y] = value;
        //}

        //private void SetValue(Vector3 worldPos, int value)
        //{
        //    var gridPos = ConvertToGrid(worldPos);
        //    SetValue((int)gridPos.x, (int)gridPos.y, value);
        //}

        //private Vector3 ConvertToGrid(Vector3 wordPos)
        //{
        //    var x = Mathf.FloorToInt(wordPos.x / m_CellSize);
        //    var y = Mathf.FloorToInt(wordPos.y / m_CellSize);

        //    return new Vector3(x, y, 0);
        //}

        //private Vector3 ConvertToWorld(int x, int y)
        //{
        //    return new Vector3(x * m_CellSize, y * m_CellSize, 0);
        //}
    }
}
