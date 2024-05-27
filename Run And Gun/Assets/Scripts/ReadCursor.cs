using UnityEngine;

public class ReadCursor : MonoBehaviour
{
    public Texture2D CrossAir;
    private void Start()
    {
        //set de texture van de muis naar de crossair
        Cursor.SetCursor(CrossAir, Vector2.zero, CursorMode.Auto);
    }


    void Update()
    {
        // pakt de muis positie en draait het wapen ernaar toe
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
    }
}
