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
        public ProductBehaviour BarrackPrefab;
        public ProductBehaviour PowerPlantPrefab;
        public ProductBehaviour SoldierUnitPrefab;

        public ProductBehaviour GetProduct(ProductType type)
        {
            if (type == ProductType.Barrack)
            {
                return BarrackPrefab;
            }
            else if (type == ProductType.PowerPlant)
            {
                return PowerPlantPrefab;
            }
            else if (type == ProductType.SoldierUnit)
            {
                return SoldierUnitPrefab;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
