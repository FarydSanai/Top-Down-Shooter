using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownShooter;

public class BFX_BloodSettings : MonoBehaviour
{
    public float AnimationSpeed = 1;
    public float GroundHeight = 0;
    [Range(0, 1)]
    public float LightIntensityMultiplier = 1;
    public bool FreezeDecalDisappearance = false;
    public _DecalRenderinMode DecalRenderinMode = _DecalRenderinMode.Floor_XZ;
    public bool ClampDecalSideSurface = false;

    private const float DestroyDelay = 14f;

    public enum _DecalRenderinMode
    {
        Floor_XZ,
        AverageRayBetwenForwardAndFloor
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyFXRoutine());
    }

    private IEnumerator DestroyFXRoutine()
    {
        yield return new WaitForSeconds(DestroyDelay);
        PoolSystem.Instance.BloodFXPool.Release(this);
    }
}
