using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    Camera _mainCam;

    PlayerAttack _playerAttack;
    private Weapon _playerWeapon;

    public Vector2 MouseInput;

    public bool IsWeaponSlash {
        get {return _playerWeapon.IsAttack;}
        set {_playerWeapon.IsAttack = value;}
    }
    public float angle { get; private set; }

    private void Awake()
    {
        _playerAttack = GetComponentInParent<PlayerAttack>();
        _playerWeapon = GameObject.Find("PlayerWeapon").GetComponent<Weapon>();
    }

    private void Update()
    {
        MouseInput = _mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (_playerAttack.IsRotate)
        {
            angle = Mathf.Atan2(MouseInput.y, MouseInput.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void WeaponAttack(){
        _playerWeapon.Attack((Vector3)MouseInput - transform.position);
    }
}
