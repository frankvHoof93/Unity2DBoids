using nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours;
using nl.FutureWhiz.Unity2DBoids.Editor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Editor.Inspectors
{
    [CustomEditor(typeof(ComplexFlockBehaviour))]
    public class ComplexFlockBehaviourEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Behaviour being Added
        /// </summary>
        private AFlockBehaviour adding;

        public override void OnInspectorGUI()
        {
            ComplexFlockBehaviour behaviour = (ComplexFlockBehaviour)target;
            // Use Reflection to keep private fields private (this is ok here since we are not at runtime)
            AFlockBehaviour[] behaviours = behaviour.GetField<AFlockBehaviour[]>("behaviours");
            float[] weights = behaviour.GetField<float[]>("weights");
            #region NoBehaviours
            // No Behaviours, show warning
            if (behaviours == null || behaviours.Length == 0)
            {
                EditorGUILayout.HelpBox("No Behaviours Set", MessageType.Warning);
            }
            #endregion

            #region ShowBehaviours
            else
            {
                // Header
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Index", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.LabelField("Behaviour", GUILayout.MinWidth(60f));
                EditorGUILayout.LabelField("Weight", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();

                // Values
                EditorGUI.BeginChangeCheck();
                for (int i = 0; i < behaviours.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    // Index
                    EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(30f), GUILayout.MaxWidth(30f));
                    // Remove
                    if (GUILayout.Button("-", GUILayout.MinWidth(15f), GUILayout.MaxWidth(15f)) || behaviours[i] == null)
                    {
                        // TODO: Clean this up. Don't use lists;
                        List<AFlockBehaviour> newBehaviours = behaviours.ToList();
                        List<float> newWeights = weights.ToList();
                        newBehaviours.RemoveAt(i);
                        newWeights.RemoveAt(i);
                        // Set through Reflection
                        behaviour.SetField("behaviours", newBehaviours.ToArray());
                        behaviour.SetField("weights", newWeights.ToArray());
                        return; // Skip this render
                    }
                    // Object
                    behaviours[i] = (AFlockBehaviour)EditorGUILayout.ObjectField(behaviours[i], typeof(AFlockBehaviour), false, GUILayout.MinWidth(60f));
                    // Weight
                    weights[i] = EditorGUILayout.FloatField(weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                    EditorGUILayout.EndHorizontal();
                }

                if (EditorGUI.EndChangeCheck())
                {
                    // Set through Reflection
                    behaviour.SetField("behaviours", behaviours);
                    behaviour.SetField("weights", weights);
                }
            }
            #endregion

            #region AddBehaviour
            // Add Behaviour
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Add Behaviour");
            EditorGUILayout.EndHorizontal();

            // Add Behaviour
            EditorGUILayout.BeginHorizontal();
            adding = (AFlockBehaviour)EditorGUILayout.ObjectField(adding, typeof(AFlockBehaviour), false);
            EditorGUILayout.EndHorizontal();
            if (adding != null)
            {
                AFlockBehaviour[] behavioursAfterAdd = new AFlockBehaviour[behaviours.Length + 1];
                float[] weightsAfterAdd = new float[behaviours.Length + 1]; // Same Length
                Array.Copy(behaviours, behavioursAfterAdd, behaviours.Length);
                Array.Copy(weights, weightsAfterAdd, behaviours.Length);
                behavioursAfterAdd[behaviours.Length] = adding;
                weightsAfterAdd[behaviours.Length] = weightsAfterAdd[behaviours.Length - 1]; // Copy last Weight
                                                                                             // Reset object
                adding = null;
                // Set through Reflection
                behaviour.SetField("behaviours", behavioursAfterAdd);
                behaviour.SetField("weights", weightsAfterAdd);
            }
            #endregion
        }
    }
}