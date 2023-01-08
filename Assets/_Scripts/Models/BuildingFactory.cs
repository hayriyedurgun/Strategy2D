using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [CreateAssetMenu(fileName = "BuildingFactory", menuName = "ScriptableObjects/BuildingFactory", order = 3)]
    public class BuildingFactory : ScriptableObject
    {
        public BaseBuilding UnitPrefab;
        public BaseBuilding BuildingPrefab;

        public BaseBuilding Create(Building building)
        {
            BaseBuilding buildingBehaviour;

            if (building.UnitId != -1)
            {
                buildingBehaviour = new BarrackBuilding();
                buildingBehaviour.Initialize(building);
            }
            else if (building.UnitId == -1)
            {
                buildingBehaviour = new BaseBuilding();
                buildingBehaviour.Initialize(building);
            }
            else
            {
                throw new NotImplementedException();
            }

            return buildingBehaviour;
        }

        public static BaseBuilding Create(Unit unit)
        {
            var buildingBehaviour = new SoldierBuilding();

            buildingBehaviour.Initialize(unit);

            return buildingBehaviour;

        }
    }
}
