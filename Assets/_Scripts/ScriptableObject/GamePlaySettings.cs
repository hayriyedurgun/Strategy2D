using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    [CreateAssetMenu(fileName = "GamePlaySettings", menuName = "ScriptableObjects/GamePlaySettings", order = 2)]
    public class GamePlaySettings : ScriptableObject
    {
        public bool IsDebugMode = false;

        public float MovementSpeed = 5;

    }
}
