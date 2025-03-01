using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Gun prefab will be parented to weaponMount on instantiation
    public Transform weaponMount;
    public Gun currentWeapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPoint2D = Camera.main.ScreenToWorldPoint(mousePosition);
        float angle = Mathf.Atan2(worldPoint2D.y, worldPoint2D.x) * Mathf.Rad2Deg;

        // Add 180 to angle because rotation is opposite otherwise... for some reason
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Fire(worldPoint2D);
        }
    }
}
