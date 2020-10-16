using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Utils
{
    /// <summary>
    /// Select Tag from DropDown in Inspector
    /// </summary>
    public class TagSelectorAttribute : PropertyAttribute
    {
        public bool UseDefaultTagFieldDrawer = false;
    }
}
