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
    public class InfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI Header;
        public Image Image;

        public RectTransform ProductionParent;
        public Image ProductionImage;
        [NonSerialized]
        public bool HasProduct;
        
        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void Init(ProductType type)
        {
            var info = GameManager.Instance.ProductFactory.GetProductInfo(type);
            Header.SetText(info.Name);
            Image.sprite = info.Sprite;
            HasProduct = info.HasProduct;
            if (HasProduct)
            {
                ProductionParent.gameObject.SetActive(true);
                ProductionImage.sprite = info.ProductSprite;
            }
            else
            {
                ProductionParent.gameObject.SetActive(false);
            }
        }

        public void OnClosePressed()
        {
            SetVisibility(false);
        }
    }
}
