using nl.FvH.Library.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : SingletonBehaviour<MenuController>
{
    [SerializeField]
    private Slider sld_alignment;
    [SerializeField]
    private Slider sld_coherence;
    [SerializeField]
    private Slider sld_separation;


    private void Start()
    {
        // TODO: Set initial slider values
    }

    public void SetAlignment(float value)
    {

    }

    public void SetCoherence(float value)
    {

    }

    public void SetSeparation(float value)
    {

    }
}
