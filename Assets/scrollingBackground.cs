using UnityEngine;

public class scrollingBackground : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Renderer backgroundRenderer;
    // Update is called once per frame
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}