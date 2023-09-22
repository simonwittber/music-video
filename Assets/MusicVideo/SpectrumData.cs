using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumData : MonoBehaviour
{

    public float coeffScale = 2;
    public float smoothCoeff = 10;
    
    public static float[] sampleBuffer = new float[1024];
    private float[] smoothSamples = new float[1023];
    
    
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        AudioListener.GetSpectrumData(sampleBuffer, 0, FFTWindow.Hamming);
        
        var coeff = 1f; 
        for (var i = 0; i < smoothSamples.Length; i++)
        {
            smoothSamples[i] = Mathf.Lerp(smoothSamples[i], sampleBuffer[i]*coeff, Time.deltaTime*smoothCoeff);
            coeff *= coeffScale;
        }
        
        Shader.SetGlobalFloatArray("_AudioSpectrum", smoothSamples);
    }
}
