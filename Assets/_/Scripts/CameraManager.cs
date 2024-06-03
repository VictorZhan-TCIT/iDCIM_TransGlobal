using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cameraLandscape;


    public void LookAt(Transform target, CameraMode cameraMode)
    {
        Debug.Log($"CameraManager LookAt: {target.name}");
        cameraLandscape.LookAt = cameraLandscape.Follow = target;
    }
}

public enum CameraMode
{
    Landscape, FirstPerspective
}