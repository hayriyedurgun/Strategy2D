using Assets._Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    [RequireComponent(typeof(InfinityScroll))]
    public class ScrollController : MonoBehaviour
    {
        public InfinityScroll Scroll;

        private void Awake()
        {
            Scroll.InitAwake();
        }

        private void Start()
        {
            Scroll.InitStart();
        }
    }
}
