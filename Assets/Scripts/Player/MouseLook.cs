using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 400f;
    public Transform playerBody;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool CursorLocked = true;
    public GameObject inventoryUI;
    public GameObject playerStatsUI;
    public GameObject equipmentUI;
    private Vector3 posicionFP = new Vector3();
    private Vector3 posicionTP = new Vector3(0, 5f, -3.2f);
    bool firstPerson = true;

    // Start is called before the first frame update
    void Start()
    {
        posicionFP = transform.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Interface").Length==0)
        {
            CursorLocked = true;
        }else
        {
            CursorLocked = false;
        }
        //CursorLocked = !(inventoryUI.activeSelf || playerStatsUI.activeSelf || equipmentUI.activeSelf);
        if (CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;
            if (firstPerson==true)
            {
                playerBody.Rotate(Vector3.up * mouseX);
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }else
            {
                Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
                transform.localPosition = new Vector3(0,0,0) + rotation * posicionTP;
                transform.LookAt(playerBody.position);
            }

        }else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(2))
        {
            xRotation = 0;
            yRotation = 0;
            if (firstPerson)
            {
                transform.localPosition = posicionTP;
                firstPerson = false;
            }else
            {
                transform.localPosition = posicionFP;
                firstPerson = true;
            }
        }
    }

    public bool isCursorLocked(){
        return CursorLocked;
    }
}
