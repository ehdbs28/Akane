using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    private List<Material> _materials = new List<Material>();

    public void InstancingShader(Renderer renderer, Color color){
        Material sampleMaterial = renderer.material;
        sampleMaterial.SetColor("_Color", color);

        if(_materials.Contains(sampleMaterial)){
            renderer.material = _materials[_materials.IndexOf(sampleMaterial)];
        }
        else{
            Material newMaterial = Instantiate(sampleMaterial);
            renderer.material = newMaterial;
        }
    }
}
