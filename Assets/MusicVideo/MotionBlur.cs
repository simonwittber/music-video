using System.Collections;
using System.Collections.Generic;
using Unity.VisualCompositor;
using UnityEngine;


[CompositorNode("Motion Blur", "Motion Blur effect")]
public class MotionBlur : CompositorNode
{
    [InputPort] public RenderTexture input;
    [OutputPort] public RenderTexture output;
    [OutputPort] public RenderTexture blurBuffer;

    [ExposeField] [Range(0, 1)] public float fade;

    private Material material;
    
    
     

    public override void Render()
    {
        if (input == null) return;
        
        if(material == null)
            material = new Material(Shader.Find("MotionBlur"));
        
        if (blurBuffer == null)
            blurBuffer = RenderTexture.GetTemporary(input.descriptor);

        if (output == null)
            output = RenderTexture.GetTemporary(input.descriptor);
        
        
        material.SetFloat("_BlurFade", fade);
        material.SetTexture("_LastFrameTex", blurBuffer);
        
        Graphics.Blit(input, output, material);
        Graphics.Blit(output, blurBuffer);
    }

    protected override void OnDestroyInternalV()
    {
        if(blurBuffer != null) RenderTexture.ReleaseTemporary(blurBuffer);
        if(output != null) RenderTexture.ReleaseTemporary(output);
        blurBuffer = null;
        output = null;
    }
}