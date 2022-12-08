using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    Camera _mainCam;

    public float angle = 0;

    private float lerpTime = 0;
    private float speed = 2f;

    private void Update()
    {
        lerpTime += Time.deltaTime * speed;
        transform.rotation = CalculateMovementOfPendulum();
    }

    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }
    private void Rotations()
    {
        Vector2 inputVec = _mainCam.ScreenToViewportPoint(Input.mousePosition);
        float angle = Mathf.Atan2(inputVec.y- transform.position.y, inputVec.x - transform.position.x) * Mathf.Rad2Deg -90;
        Debug.Log(angle);
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
