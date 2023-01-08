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

        private void Start()
        {
            var info = GameManager.Instance.ProductFactory.GetProductInfo(Type);

            Text.text = info.Name;
            Image.sprite = info.Sprite;
        }

        public void OnClick()
        {
            GameManager.Instance.InputController.SetProduct(Type);
        }
    }
}
