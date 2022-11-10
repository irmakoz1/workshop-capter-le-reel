using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShaderController : MonoBehaviour
{

    public InputAction InputBindings;
    public float inputMultiplier = 0f;
    public float minValue = 0f;
    public float maxValue = 1f;
    private string name = "";

    public bool remap = true;
    public float inputMinRange = -1;
    public float inputMaxRange = 1;

    public Color firstColor;
    public Color secondColor;

    public enum ParamNames
    {
        SCALE,
        AMPLITUDE,
        SPEED,
        COLOR
    }

    public ParamNames ParamName;

    private void OnEnable()
    {
        InputBindings.Enable();
    }

    private void OnDisable()
    {
        InputBindings.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        switch (ParamName)
        {
            case ParamNames.AMPLITUDE:
                name = "_NoiseAmplitude";
                //inputMultiplier = 0.001f;
                //minValue = 0.003f;
                //maxValue = 100f;
                break;
            case ParamNames.SCALE:
                name = "_NoiseScale";
                //inputMultiplier = 0.001f;
                //minValue = 0.001f;
                //maxValue = 10f;
                break;
            case ParamNames.SPEED:
                name = "_NoiseSpeed";
                //inputMultiplier = 0.001f;
                //minValue = 0.001f;
                //maxValue = 10f;
                break;
            case ParamNames.COLOR:
                name = "_Tint";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var material = GetComponent<Renderer>().material;
        var remappedValue = InputBindings.ReadValue<float>();
        if (remap) remappedValue = Mathf.LerpUnclamped(inputMinRange, inputMaxRange, InputBindings.ReadValue<float>());
        
        if (ParamName == ParamNames.COLOR)
        {
            var oldColor = material.GetColor("_Tint");
            var newColor = Color.Lerp(oldColor, remappedValue > 0.5f ? secondColor : firstColor, Time.deltaTime);
            material.SetColor(name, newColor);
        }
        else
        {
            if (Mathf.Abs(remappedValue) < 0.1f) return;
            var inputValue = remappedValue * inputMultiplier;
            var oldValue = material.GetFloat(name);
            var newValue = Mathf.Clamp(oldValue + inputValue, minValue, maxValue);
            GetComponent<Renderer>().material.SetFloat(name, newValue);
        }
        
        
        // Debug.Log(InputBindings.ReadValue<float>());
    }

}
