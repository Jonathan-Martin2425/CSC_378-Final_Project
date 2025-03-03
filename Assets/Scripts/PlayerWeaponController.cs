using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerWeaponController : MonoBehaviour
{
    // Gun prefab will be parented to weaponMount on instantiation
    public Transform weaponMount;
    public Gun[] weapons;
    public Gun currentWeapon;

    [Header("Gun Buttons")]
    public GameObject[] weaponButtons;
    public Color selectedColor = new Color(231, 210, 34);
    private Color defaultButtonColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultButtonColor = weaponButtons[0].GetComponent<Image>().color;
        weaponButtons[0].GetComponent<Image>().color = selectedColor;
        
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
        currentWeapon = weapons[0];
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(1);
        }

    }

    public void SwitchGun(int gunID)
    {
        Debug.Log($"{gunID}");
        Debug.Log($"{currentWeapon.id}");
        if (gunID != currentWeapon.id)
        {
            weapons[currentWeapon.id].gameObject.SetActive(false);
            weapons[gunID].gameObject.SetActive(true);
            HighlightButton(gunID);
            currentWeapon = weapons[gunID];
        }
    }

    void HighlightButton(int id)
    {
        weaponButtons[id].GetComponent<Image>().color = selectedColor;
        weaponButtons[currentWeapon.id].GetComponent<Image>().color = defaultButtonColor;
    }
}
