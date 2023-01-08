using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [Serializable]
    public class Building : IModel
    {
        public string Name;
        public string SpriteName;

        public int Width;
        public int Height;

        public Vector3 Position;

        public int UnitId;

        public void Deserialize(string jsonStr)
        {
            var deserialized = (Building)JsonUtility.FromJson(jsonStr, typeof(Building));
            if (deserialized != null)
            {
                Name = deserialized.Name;
                SpriteName = deserialized.SpriteName;
                Width = deserialized.Width;
                Height = deserialized.Height;
                UnitId = deserialized.UnitId;
                Position = deserialized.Position;
            }
            else
            {
                Debug.LogError("Unexpected deserialize!!");
            }
        }

        public string Serialize()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
