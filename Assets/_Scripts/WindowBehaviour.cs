﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Assets._Scripts
{
    public class WindowBehaviour : UIBehaviour
    {
        public Action WindowResized;

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            WindowResized?.Invoke();
        }
    }
}
