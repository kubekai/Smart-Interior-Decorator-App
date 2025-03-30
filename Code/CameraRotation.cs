using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // 旋转速度
    public float rotationSpeed = 0.075f;

    // 垂直旋转角度
    private float rotationX = 0f;

    void Update()
    {
        // 当鼠标左键按下时才执行旋转
        if (Input.GetMouseButton(0)&&GlobalValue.ViewLock == false)
        {
            // 获取鼠标移动的距离
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // 横向旋转
            transform.Rotate(Vector3.up, mouseX * rotationSpeed*0.025f);

            // 纵向旋转
            rotationX -= mouseY * rotationSpeed*0.025f;

            // 限制纵向旋转角度在一定范围内
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // 应用旋转
            transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, 0f);
        }
        if (Input.touchCount > 0&&GlobalValue.ViewLock == false)
        {
            // 获取第一个触摸
            Touch touch = Input.GetTouch(0);

            // 获取触摸移动的距离
            float mouseX = touch.deltaPosition.x;
            float mouseY = touch.deltaPosition.y;

            // 横向旋转
            transform.Rotate(Vector3.up, mouseX * rotationSpeed*0.5f);

            // 纵向旋转
            rotationX -= mouseY * rotationSpeed*0.5f;

            // 限制纵向旋转角度在一定范围内
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // 应用旋转
            transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, 0f);
        }
    }
}