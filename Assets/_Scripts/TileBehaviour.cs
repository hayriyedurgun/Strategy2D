using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class TileBehaviour : MonoBehaviour
    {
        public int PosX;
        public int PosY;
        public float CellSize;

        public Image AvailityImage;

        public SpriteRenderer TileRenderer;
        public Sprite OddSprite;
        public Sprite EvenSprite;

        [NonSerialized]
        public ProductBehaviour Product;

        public void Init(int posX, int posY, float cellSize)
        {
            PosX = posX;
            PosY = posY;
            CellSize = cellSize;

            if ((posX % 2 == 0 && posY % 2 != 0) ||
                posX % 2 != 0 && posY % 2 == 0)
            {
                TileRenderer.sprite = EvenSprite;
            }
            else
            {
                TileRenderer.sprite = OddSprite;
            }
        }

        public void SetStatus(bool canPlace)
        {
            AvailityImage.gameObject.SetActive(true);
            AvailityImage.color = canPlace ? new Color(0, 1, 0, .5f) : new Color(1, 0, 0, .5f);
        }

        public void ClearStatus()
        {
            AvailityImage.gameObject.SetActive(false);
        }
    }

    public enum TileStatus
    {
        None,
        Available,
        NotAvailable
    }
}
