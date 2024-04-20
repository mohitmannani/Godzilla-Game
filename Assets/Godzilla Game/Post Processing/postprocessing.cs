using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class postprocessing : MonoBehaviour
{


    public PostProcessProfile vignetteLayer;
    public PostProcessProfile depthOfFieldLayer;
    public PostProcessProfile grainLayer;
    public PostProcessProfile motionBlurLayer;
    public PostProcessProfile lensDistortionLayer;



    public GameObject vignetteMenu;
    public GameObject depthOfFieldMenu;
    public GameObject grainMenu;
    public GameObject motionBlurMenu;
    public GameObject lensDistortionMenu;


    public static int activevignette;
    public static int activevdepthoffield;
    public static int activegrain;
    public static int activemotionblur;
    public static int activelenstortion;


    public GameObject vignetteGame;
    public GameObject depthOfFieldGame;
    public GameObject grainGame;
    public GameObject motionBlurGame;
    public GameObject lensDistortionGame;





    public Slider vignetteIntensitySlider;
    public Slider depthOfFieldFocusDistanceSlider;
    public Slider grainIntensitySlider;
    public Slider lensDistortionIntensitySlider;
    public Slider motionblurslider;



    private void Start()
    {
        // Disable all post-processing effects initially
        SetPostProcessingEffect(vignetteGame, false);
        SetPostProcessingEffect(depthOfFieldGame, false);
        SetPostProcessingEffect(grainGame, false);
        SetPostProcessingEffect(motionBlurGame, false);
        SetPostProcessingEffect(lensDistortionGame, false);



        

        EnableVignetteGame();
        EnableGrainGame();
        EnableDepthOfFieldGame();
        EnableMotionBlurGame();
        EnableLensDistortionGame();




        vignetteIntensitySlider.onValueChanged.AddListener(ChangeVignetteIntensity);
        depthOfFieldFocusDistanceSlider.onValueChanged.AddListener(ChangeDepthOfFieldFocusDistance);
        grainIntensitySlider.onValueChanged.AddListener(ChangeGrainIntensity);
        lensDistortionIntensitySlider.onValueChanged.AddListener(ChangeLensDistortionIntensity);
        motionblurslider.onValueChanged.AddListener(ChangeShutterAngle);
    }



    public void ChangeVignetteIntensity(float intensity)
    {

        //  float maxIntensity = 1.0f;
        Vignette vignette;

        Debug.Log("Vignette intensity changed: " + intensity);
        if (vignetteLayer.TryGetSettings(out vignette))
        {
            vignette.intensity.value = intensity ;
            
        }
    }

    public void ChangeDepthOfFieldFocusDistance(float focusDistance)
    {
        DepthOfField depthOfField;
        if (depthOfFieldLayer.TryGetSettings(out depthOfField))
        {
            depthOfField.focusDistance.value = focusDistance;
        }
    }

    public void ChangeGrainIntensity(float grainIntensity)
    {
        Grain grain;
        if (grainLayer.TryGetSettings(out grain))
        {
            grain.intensity.value = grainIntensity;
        }
    }

    public void ChangeLensDistortionIntensity(float lensIntensity)
    {
        LensDistortion lensDistortion;
        if (lensDistortionLayer.TryGetSettings(out lensDistortion))
        {
            lensDistortion.intensity.value = lensIntensity;
        }
    }
    public void ChangeShutterAngle(float angle)
    {
        MotionBlur motionBlur;
        if (motionBlurLayer.TryGetSettings(out motionBlur))
        {
            motionBlur.shutterAngle.value = angle;
        }
    }



    public void EnableVignetteMenu()
    {
        activevignette = (vignetteMenu.activeInHierarchy) ? 1 : 0;
       
        
    }

    public void EnableDepthOfFieldMenu()
    {
        activevdepthoffield = (depthOfFieldMenu.activeSelf) ? 1 : 0;
    }

    public void EnableGrainMenu()
    {
        activegrain = (grainMenu.activeInHierarchy) ? 1 : 0;
    }

    public void EnableMotionBlurMenu()
    {
        activemotionblur = (motionBlurMenu.activeSelf) ? 1 : 0;
    }

    public void EnableLensDistortionMenu()
    {
        activelenstortion = (lensDistortionMenu.activeSelf) ? 1 : 0;
    }

    public void EnableVignetteGame()
    {
        SetPostProcessingEffect(vignetteGame, activevignette == 1);
    }

    public void EnableDepthOfFieldGame()
    {
        SetPostProcessingEffect(depthOfFieldGame, activevdepthoffield == 1);
    }

    public void EnableGrainGame()
    {
        SetPostProcessingEffect(grainGame, activegrain == 1);
    }

    public void EnableMotionBlurGame()
    {
        SetPostProcessingEffect(motionBlurGame, activemotionblur == 1);
    }

    public void EnableLensDistortionGame()
    {
        SetPostProcessingEffect(lensDistortionGame, activelenstortion == 1);
    }

    private void SetPostProcessingEffect(GameObject effectObject, bool isEnabled)
    {
        effectObject.SetActive(isEnabled);

        // Enabling/disabling post-processing effects through PostProcessLayer component
        PostProcessLayer postProcessLayer = effectObject.GetComponent<PostProcessLayer>();
        if (postProcessLayer != null)
        {
            postProcessLayer.enabled = isEnabled;
        }
    }
    private void Update()
    {
        EnableVignetteMenu();
        
        EnableGrainMenu();

        Debug.Log(activegrain);
        Debug.Log(activevignette);
    }
}