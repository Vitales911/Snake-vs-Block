using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EatForSphere : MonoBehaviour
{
    public int _Eat;
    [SerializeField] Text _EatTextPanel;


    private void Awake()
    {
        _Eat = Random.Range(1, 5);
    }
    void Update()
    {
        _EatTextPanel.text = _Eat.ToString();
    }
    private void OnTriggerEnter(Collider Head)
    {
        print(Head.gameObject.name);
        if (Head.gameObject.tag == "HeadSphere")
        {
            Destroy(gameObject, .0f);
        }
    }
}
