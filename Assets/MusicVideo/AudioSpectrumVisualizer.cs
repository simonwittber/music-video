using Unity.VisualCompositor;
using UnityEngine;

[CompositorNode("Audio Spectrum", "Audio Spectrum Visualizer")]
public class AudioSpectrumVisualizer : CompositorNode
{
    [InputPort] public RenderTexture input;
    [OutputPort] public RenderTexture output;
    
    [ExposeField] public Color color = Color.white;
    [ExposeField] [Range(-1, 1)] public float center;
    [ExposeField] public float scale = 1;
    
    private Material material;

    
    public override void Render()
    {
        if (input == null) return;
        
        if(material == null)
            material = new Material(Shader.Find("AudioSpectrum"));
        
        if (output == null)
            output = RenderTexture.GetTemporary(input.descriptor);
        
        material.SetColor("_Color", color);
        material.SetFloat("_SpectrumCenter", center);
        material.SetFloat("_SpectrumScale", scale);
        Graphics.Blit(input, output, material);
    }
}