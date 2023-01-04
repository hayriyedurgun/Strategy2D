using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class ProductItem : MonoBehaviour
    {
        public Image Image;
        public TextMeshProUGUI Text;
        public ProductType Type;

        public Sprite BarrackSprite;
        public Sprite PowerPlantSprite;
        public Sprite SoldierUnitSprite;

        private void Start()
        {
            Text.text = Type.ToString();

            if (Type == ProductType.Barrack)
            {
                Image.sprite = BarrackSprite;
            }
            else if (Type == ProductType.PowerPlant)
            {
                Image.sprite = PowerPlantSprite;
            }
            else if (Type == ProductType.SoldierUnit)
            {
                Image.sprite = SoldierUnitSprite;
            }
        }

        public void OnClick()
        {
            GameManager.Instance.Player.SetProduct(Type);
        }
    }
}
