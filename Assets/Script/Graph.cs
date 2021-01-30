using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab = default;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function = default;

    public enum TransitionMode { Normal, Cycle, Random }

    [SerializeField]
    TransitionMode transitionMode = TransitionMode.Normal;

    [SerializeField, Min(0.0f)]
    float functionDuration = 1.0f;

    Transform[] points;

    float duration = 0.0f;

    private void Awake()
    {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    private void Update()
    {
        duration += Time.deltaTime;
        if (duration >= functionDuration)
        {
            duration -= functionDuration;
            PickNextFunction();
        }
        UpdateFunction();
    }

    private void PickNextFunction()
    {
        if (transitionMode == TransitionMode.Normal) return;
        function = transitionMode == TransitionMode.Cycle ?
            FunctionLibrary.GetNextFunctionName(function) :
            FunctionLibrary.GetRandomFunctionNameOtherThan(function);

    }

    private void UpdateFunction()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        var time = Time.time;
        var step = 2.0f / resolution;
        var v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
    }
    
}
