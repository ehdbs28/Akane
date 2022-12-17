using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    private Dictionary<Color, Material> _materials = new Dictionary<Color, Material>();

    public void InstancingShader(Renderer renderer, Color color){
        Material sampleMaterial = renderer.material;
        sampleMaterial.SetColor("_Color", color);

        if(_materials.ContainsKey(color)){
            renderer.material = _materials[color];
        }
        else{
            renderer.material = sampleMaterial;
            _materials.Add(color, sampleMaterial);
        }
    }
}
