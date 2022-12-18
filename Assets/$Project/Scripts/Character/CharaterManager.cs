using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharaterManager : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 10f;
    public float Sensitivity = 10f;

    Rigidbody _rig;
    bool _isShowJoystick;
    void Awake()
    {
        _isShowJoystick = SystemInfo.deviceType == DeviceType.Handheld;
        _rig = GetComponent<Rigidbody>();
        if (!_isShowJoystick)
            UltimateJoystick.DisableJoystick("Movement");
    }
    void Start()
    {
        Move();
        RotateView();
    }
    void Move()
    {
        var input = new Vector2();
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                if (_isShowJoystick)
                {
                    input.x = UltimateJoystick.GetHorizontalAxis("Movement");
                    input.y = UltimateJoystick.GetVerticalAxis("Movement");
                }
                else
                {
                    input.x = Input.GetAxis("Horizontal");
                    input.y = Input.GetAxis("Vertical");
                }
            });

        // move the character based on the input
        this.FixedUpdateAsObservable()
            .Subscribe(_ =>
            {
                var movement = new Vector3(input.x, _rig.velocity.y, input.y);
                movement.Normalize();
                transform.Translate(movement * Speed * Time.fixedDeltaTime, Space.Self);
            });
    }
    void RotateView()
    {
        // rotate the character based on mouse input
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ =>
            {
                float x = Input.GetAxis("Mouse X");
                //float y = Input.GetAxis("Mouse Y");
                transform.Rotate(Vector3.up, x * Sensitivity);
                //transform.Rotate(Vector3.right, -y * Sensitivity);
            });

        // rotate the character based on touch input
        this.UpdateAsObservable()
            .Where(_ => Input.touchCount > 0)
            .Subscribe(_ =>
            {
                Touch touch = Input.GetTouch(0);
                float x = touch.deltaPosition.x;
                //float y = touch.deltaPosition.y;
                transform.Rotate(Vector3.up, x * Sensitivity);
                //transform.Rotate(Vector3.right, -y * Sensitivity);
            });
    }
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<OreManager>()?.OpenView();
    }
    void OnTriggerExit(Collider other)
    {
        other.GetComponent<OreManager>()?.CloseView();
    }
}
