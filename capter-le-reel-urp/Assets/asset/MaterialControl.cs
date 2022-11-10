using UnityEngine;
using UnityEngine.InputSystem;

public class MaterialControl : MonoBehaviour
{
    [SerializeField] private InputAction inputBindings;
    [SerializeField] private Renderer controledRenderer;
    [SerializeField] private string PropertyName;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float lerpSpeed = 0.1f;
    [SerializeField] private float decay = 1f;

    private Material tempMaterial;
    private float lerpedValue = 0f;

    private void OnEnable()
    {
        inputBindings.Enable();
    }

    private void OnDisable()
    {
        inputBindings.Disable();
    }

    private void Start()
    {
        if (controledRenderer == null) controledRenderer = GetComponent<Renderer>();
        tempMaterial = controledRenderer.material;
    }

    void Update()
    {
        var value = inputBindings.ReadValue<float>() * multiplier;
        if (Mathf.Abs(value) > 0.01f) lerpedValue = Mathf.Lerp(lerpedValue, value, lerpSpeed * Time.deltaTime);
        else lerpedValue = Mathf.Lerp(lerpedValue, value, decay * Time.deltaTime);
        tempMaterial.SetFloat(PropertyName, lerpedValue);
    }
}
