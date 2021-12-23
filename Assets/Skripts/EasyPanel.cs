using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EasyPanel : MonoBehaviour
{
    private int TextEasyDeadPanel;
    [SerializeField] Text _LevelEasyDeadPanel;
    private void Awake()
    {
        TextEasyDeadPanel = Random.Range(1, 5);
    }
    void Update()
    {
        _LevelEasyDeadPanel.text = TextEasyDeadPanel.ToString();

    }

    private void OnTriggerStay(Collider Sphere)
    {
        print(Sphere.gameObject.name);
        if (Sphere.gameObject.tag == "HeadSphere")
        {
            TextEasyDeadPanel--;
            if (TextEasyDeadPanel == 0)
            {
                Destroy(gameObject,.0f);
            }

        }
    }
}
