using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets._Scripts
{
    public class TileBehaviour : MonoBehaviour
    {
        public int PosX;
        public int PosY;
        public float CellSize;

        public TextMeshPro Text;

        [NonSerialized]
        public bool IsOccupied = false;


        private int m_Value;
        public int Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                Text.text = Value.ToString();
            }
        }

        public void Init(int posX, int posY, float cellSize)
        {
            PosX = posX;
            PosY = posY;
            CellSize = cellSize;

            Text.text = Value.ToString();
        }
    }
}
