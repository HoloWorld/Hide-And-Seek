// Make camera wobble in a funky way!
using UnityEngine;
using System.Collections;

public class ProjectionMatrixController : MonoBehaviour
{
    public Matrix4x4 originalProjection;
    Camera ccamera;

    void Start()
    {
        ccamera = GetComponent<Camera>();
    }

    void Update()
    {
        Matrix4x4 p = originalProjection;
        p.m01 += Mathf.Sin(Time.time * 1.2F) * 0.1F;
        p.m10 += Mathf.Sin(Time.time * 1.5F) * 0.1F;
        ccamera.projectionMatrix = p;
    }
}