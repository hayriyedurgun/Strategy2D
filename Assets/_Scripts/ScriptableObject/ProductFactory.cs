using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    [CreateAssetMenu(fileName = "ProductFactory", menuName = "ScriptableObjects/ProductFactory", order = 1)]
    public class ProductFactory : ScriptableObject
    {
        public List<ProductInfo> Products;

        public BaseProduct GetProduct(ProductType type)
        {
            var info = GetProductInfo(type);
            return info.ProductPrefab;
        }

        public ProductInfo GetProductInfo(ProductType type)
        {
            var product = Products.FirstOrDefault(x => x.Type == type);
            if (product != null)
            {
                return product;
            }

            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ProductInfo
    {
        public ProductType Type;
        public string Name;
        public Sprite Sprite;
        public bool HasProduction;
        public ProductType ProductionType;
        public Sprite ProductionSprite;
        public float Cooldown;

        public int Width;
        public int Height;

        public BaseProduct ProductPrefab;
    }
}
