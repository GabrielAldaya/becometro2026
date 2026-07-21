using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    [Header("Escala")]
    public float scaleMin = 0.9f;
    public float scaleMax = 1.1f;
    public float scaleSpeed = 0.2f;

    [Header("Rotación")]
    public float rotationAmount = 360f;   // grados máximos de oscilación sobre la rotación inicial
    public float rotationSpeed = 0.3f;

    [Header("Posición (arriba/abajo)")]
    public float positionAmount = 10f;   // píxeles de desplazamiento sobre la posición inicial
    public float positionSpeed = 1.2f;

    [Header("Offset de fase (para que no sean idénticos)")]
    public float phaseOffset = 0f;

    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Quaternion _startRotation;

    void Start()
    {
        _startPosition = transform.localPosition;
        _startScale = transform.localScale;
        _startRotation = transform.localRotation;   // guarda la rotación original
    }

    void Update()
    {
        float t = Time.time + phaseOffset;

        // --- Escala: multiplica desde la escala original del objeto ---
        float scaleFactor = Mathf.Lerp(scaleMin, scaleMax, (Mathf.Sin(t * scaleSpeed) + 1f) / 2f);
        transform.localScale = _startScale * scaleFactor;

        // --- Rotación: suma al eje Z de la rotación original del objeto ---
        float angle = Mathf.Sin(t * rotationSpeed) * rotationAmount;
        transform.localRotation = _startRotation * Quaternion.Euler(0f, 0f, angle);

        // --- Posición: desplaza desde la posición original del objeto ---
        float offsetY = Mathf.Sin(t * positionSpeed) * positionAmount;
        transform.localPosition = _startPosition + new Vector3(0f, offsetY, 0f);
    }
}