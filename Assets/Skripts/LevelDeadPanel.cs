using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDeadPanel : MonoBehaviour
{
    public int TextDeadPanel;
    [SerializeField] Text _LevelDeadPanel;
    private Renderer _renderer;
    [SerializeField]
    float timeDelay = .5f;
    [SerializeField]
    bool loop = false;
    void Awake()
    {
        TextDeadPanel = Random.Range(5, 20);
    }

    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        StartCoroutine(ChangeColor(timeDelay));


    }
    void Update()
    {
        _LevelDeadPanel.text = TextDeadPanel.ToString();

    }
    Color[] colors = { Color.blue, Color.green, Color.magenta, Color.red };
    IEnumerator ChangeColor(float delay)
    {
        _renderer.material.color = colors[Random.Range(0, colors.Length)];
        yield return new WaitForSeconds(delay);

        if (loop)
            StartCoroutine(ChangeColor(delay));
    }

    private void OnTriggerStay(Collider HeadSphere)
    {
        print(HeadSphere.gameObject.name);
        if (HeadSphere.gameObject.tag == "HeadSphere")
        {
            TextDeadPanel--;
            if (TextDeadPanel == 0)
            {
                Destroy(gameObject, .0f);
            }

        }
    }
}
