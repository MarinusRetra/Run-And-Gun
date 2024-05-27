using UnityEngine;

public class ReadCursor : MonoBehaviour
{
    public Texture2D CrossAir;
    Vector2 mousePosition;
    private void Start()
    {
        Cursor.SetCursor(CrossAir, Vector2.zero, CursorMode.Auto);
    }


    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
        transform.eulerAngles = new Vector3(0f, 0f, -transform.eulerAngles.z);
    }
}
