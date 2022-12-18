using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    Camera _mainCam;

    PlayerAttack _playerAttack;
    private Weapon _playerWeapon;

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
        if (_playerAttack.IsRotate)
        {
            Vector2 inputVec = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(inputVec.y - transform.position.y, inputVec.x - transform.position.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
    }

    public void WeaponAttack(){
        _playerWeapon.Attack(_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }
}
