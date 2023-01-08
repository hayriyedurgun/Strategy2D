using Assets._Scripts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_Instance = null;
        public static GameManager Instance => m_Instance;

        public InputController InputController;

        //public ProductFactory ProductFactory;

        public GamePlaySettings GamePlaySettings;

        public List<Building> Buildings = new List<Building>();
        public List<Unit> Units = new List<Unit>();

        private void Awake()
        {
            m_Instance = this;

            var files = Directory.GetFiles("/Serializations/Buildings");
            Building building;
            foreach (var file in files)
            {
                building = new Building();
                building.Deserialize(File.ReadAllText(file));
                Buildings.Add(building);

                BuildingFactory.Create(building);
            }

            files = Directory.GetFiles("/Serializations/Units");
            Unit unit;
            foreach (var file in files)
            {
                unit = new Unit();
                unit.Deserialize(File.ReadAllText(file));
                Units.Add(unit);

                BuildingFactory.Create(unit);
            }
        }

        private void OnDestroy()
        {
            Buildings.ForEach(x => File.WriteAllText($"/Serializations/Buildings/{x.Name}.json", x.Serialize()));
            Units.ForEach(x => File.WriteAllText($"/Serializations/Units/{x.Name}.json", x.Serialize()));

            m_Instance = null;
        }
    }
}

