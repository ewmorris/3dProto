using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class cameraDOF : MonoBehaviour
{

    // Array of targets
    public Transform[] focusTargets;

    // Current target
    public float focusTargetID;

    // Cache profile
    PostProcessingProfile postProfile;

    // Adjustable aperture - used in animations within Timeline
    [Range(1.5f, .1f)] public float aperture;


    void Start()
    {
        // Load the post processing profile
        postProfile = GetComponent<PostProcessingBehaviour>().profile;
    }

    void Update()
    {
        // Get distance from camera and target
        float dist = Vector3.Dot(focusTargets[Mathf.FloorToInt(focusTargetID)].position - transform.position, transform.forward);

        // Get reference to the DoF settings
        var dof = postProfile.depthOfField.settings;

        // Set variables
        dof.focusDistance = dist;
        dof.aperture = aperture;

        // Apply settings
        postProfile.depthOfField.settings = dof;
    }
}
