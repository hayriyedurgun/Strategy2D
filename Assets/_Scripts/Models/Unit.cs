using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [Serializable]
    public class Unit : IModel
    {
        public int Id;
        public string Name;
        public string SpriteName;
        public float Cooldown;

        public Vector3 Position;

        public void Deserialize(string jsonStr)
        {
            var deserialized = (Unit)JsonUtility.FromJson(jsonStr, typeof(Unit));
            if (deserialized != null)
            {
                Id = deserialized.Id;
                Name = deserialized.Name;
                SpriteName = deserialized.SpriteName;
                Cooldown = deserialized.Cooldown;
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
