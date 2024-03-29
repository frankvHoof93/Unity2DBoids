﻿using nl.FutureWhiz.Unity2DBoids.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace nl.FutureWhiz.Unity2DBoids.UserInput
{
    public class InputController : SingletonBehaviour<InputController>
    {
        #region Variables
        #region Editor
        /// <summary>
        /// Tag for GameCamera
        /// </summary>
        [SerializeField]
        [TagSelector]
        [Tooltip("Tag for GameCamera")]
        private string cameraTag;
        [SerializeField]
        [Tooltip("Camera for GameScene")]
        private Camera gameCamera;
        #endregion

        #region Private
        /// <summary>
        /// Whether Input is currently being applied
        /// </summary>
        private bool isMouseDown;
        #endregion
        #endregion

        #region Methods
        #region Public
        public Vector2? InputPosition()
        {
            #region CheckForInput
            // Negate Previous Input
            if (isMouseDown)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isMouseDown = false;
                    return null;
                }
                if (Input.touchSupported && Input.touchCount == 0)
                {
                    isMouseDown = false;
                    return null;
                }
            }
            // Check for NEW Input
            if (!isMouseDown && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
            {
                if (Input.touchCount > 0)
                {
                    isMouseDown = true;
                    for (int i = 0; i < Input.touchCount; i++) // IsPointerOverGameObject Bugs on Android if Touch.ID is not checked
                    {
                        Touch touch = Input.GetTouch(i);
                        isMouseDown = isMouseDown && !EventSystem.current.IsPointerOverGameObject(touch.fingerId); // Negate Input if Press was on Canvas instead of Play-Area
                    } // TODO: Code above still bugs on Android (on Release of Touch)
                }
                else if (Input.GetMouseButtonDown(0))
                    isMouseDown = !EventSystem.current.IsPointerOverGameObject(); // Negate Input if Press was on Canvas instead of Play-Area
            }
            #endregion

            #region HandleInput
            if (isMouseDown)
            {
                if (!gameCamera)
                    Start(); // Grab reference again
                Vector2? mousePos = null;
                // Prioritise Touch
                if (Input.touchCount > 0)
                {
                    // Get WorldPos from Touch
                    Touch touch = Input.GetTouch(0);
                    mousePos = gameCamera.ScreenToWorldPoint(touch.position);
                }
                // FallBack to Mouse
                else if (Input.GetMouseButton(0))
                {
                    // Get WorldPos from Mouse
                    mousePos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
                }
                return mousePos;
            }
            else return null;
            #endregion
        }
        #endregion

        #region Unity
        /// <summary>
        /// Reset for IsMouseDown
        /// </summary>
        private void OnEnable()
        {
            isMouseDown = false;
        }
        /// <summary>
        /// Reset for IsMouseDown
        /// </summary>
        private void OnDisable()
        {
            isMouseDown = false;
        }
        /// <summary>
        /// Grab Reference to Camera
        /// </summary>
        private void Start()
        {
            if (!gameCamera)
                gameCamera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
        }
        #endregion
        #endregion
    }
}