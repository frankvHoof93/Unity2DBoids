using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts.Utils
{
    public enum BoidType
    {
        Cohesion,
        Separation,
        Alignment
    }

    public static class BoidTypeExtensions
    {
        /// <summary>
        /// Classes per Type
        /// </summary>
        private static Dictionary<BoidType, Type[]> behaviourTypes = new Dictionary<BoidType, Type[]>
        {
            { BoidType.Alignment, new Type[] { typeof(AlignmentBehaviour) } },
            { BoidType.Cohesion, new Type[] { typeof(CohesionBehaviour), typeof(SmoothCohesionBehaviour) } },
            { BoidType.Separation, new Type[] { typeof(AvoidanceBehaviour) } },
        };

        public static bool IsClassOfType(this BoidType type, AFlockBehaviour behaviour)
        {
            Type[] types = behaviourTypes[type];
            return types.Contains(behaviour.GetType());
        }
    }
}
