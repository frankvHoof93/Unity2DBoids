using nl.FutureWhiz.Unity2DBoids.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace nl.FutureWhiz.Unity2DBoids.UI
{
    /// <summary>
    /// Controller for 2D-UI (Canvas)
    /// </summary>
    public class MenuController : SingletonBehaviour<MenuController>
    {
        #region Variables
        #region Editor
        /// <summary>
        /// UI-Slider for Alignment
        /// </summary>
        [SerializeField]
        [Tooltip("UI-Slider for Alignment")]
        private Slider sld_alignment;
        /// <summary>
        /// UI-Slider for Coherence
        /// </summary>
        [SerializeField]
        [Tooltip("UI-Slider for Coherence")]
        private Slider sld_coherence;
        /// <summary>
        /// UI-Slider for Separation
        /// </summary>
        [SerializeField]
        [Tooltip("UI-Slider for Separation")]
        private Slider sld_separation;
        #endregion
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Sets Alignment to FlockController based on Slider
        /// </summary>
        /// <param name="value">Value to Set</param>
        public void SetAlignment(float value)
        {
            FlockController.Instance.SetWeight(BoidParamType.Alignment, value);
        }
        /// <summary>
        /// Sets Cohesion to FlockController based on Slider
        /// </summary>
        /// <param name="value">Value to Set</param>
        public void SetCohesion(float value)
        {
            FlockController.Instance.SetWeight(BoidParamType.Cohesion, value);
        }
        /// <summary>
        /// Sets Separation to FlockController based on Slider
        /// </summary>
        /// <param name="value">Value to Set</param>
        public void SetSeparation(float value)
        {
            FlockController.Instance.SetWeight(BoidParamType.Separation, value);
        }
        #endregion

        #region Unity
        /// <summary>
        /// Sets initial slider values from current values in ScriptableObjects
        /// </summary>
        private void Start()
        {
            sld_alignment.value = FlockController.Instance.GetWeight(BoidParamType.Alignment);
            sld_coherence.value = FlockController.Instance.GetWeight(BoidParamType.Cohesion);
            sld_separation.value = FlockController.Instance.GetWeight(BoidParamType.Separation);
        }
        #endregion
        #endregion
    }
}