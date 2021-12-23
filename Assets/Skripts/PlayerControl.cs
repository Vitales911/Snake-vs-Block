using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    private Transform curBodyPart;
    private Transform PrevBodyPart;
    public GameObject bodyprefabs;
    private float dis;
    public float minDistance = 0.25f;

    public float ForwardSpeed = 5;
    public float Sensitivity = 10;

    private Camera mainCamera;
    private Rigidbody componentRigidbody;

    private Vector3 touchLastPos;
    private float sidewaysSpeed;
    public int TextHeadSphere = 2;
    [SerializeField] Text LevelHeadSphere;

    public AudioSource Yami;
    public AudioSource DeadPanelSound;

    [SerializeField] public GameObject panelOver;
    [SerializeField] public GameObject panelWon;
    //public EatForSphere eat;
    private void Awake()
    {
        for (int i = 0; i < TextHeadSphere; i++)
        {
            AddBodyPart();
        }
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        LevelHeadSphere.text = TextHeadSphere.ToString();
        GameOver();
        WonGame();
    }
    public void GameOver()
    {

        if (TextHeadSphere == 0)
        {
            Time.timeScale = 0;
            panelOver.SetActive(true);
        }
    }
    public void WonGame()
    {
            if (TextHeadSphere >= 30)
            {
                Time.timeScale = 0;
                panelWon.SetActive(true);
            }

    }

    public void AddBodyPart()
    {
        Transform newpart = (Instantiate(bodyprefabs, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
        newpart.SetParent(transform);
        bodyParts.Add(newpart);
    }
    public void RemoveCircle()
    {
        Destroy(bodyParts[1].gameObject);
        bodyParts.RemoveAt(1);
    }
    public void Move()
    {

        bodyParts[0].Translate(bodyParts[0].forward * ForwardSpeed * Time.smoothDeltaTime, Space.World);
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            //bodyParts[0].Rotate(Sensitivity * Time.deltaTime * Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }            
        for (int i = 1; i < bodyParts.Count; i++)            
        {                
            curBodyPart = bodyParts[i];                
            PrevBodyPart = bodyParts[i - 1];                
            dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);                
            Vector3 newpos = PrevBodyPart.position;                
            newpos.y = bodyParts[0].position.y;                
            float T = Time.deltaTime * dis / minDistance * ForwardSpeed;                
            if (T > 0.5f)                    
                T = 0.5f;                
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);                
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddBodyPart();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RemoveCircle();
        }
    }



    private void OnTriggerStay(Collider DeadPanel)
    {
        print(DeadPanel.gameObject.name);
        if (DeadPanel.CompareTag ("DeadPanel"))
        {
            DeadPanelSound.Play();
            TextHeadSphere--;
            for (int i = bodyParts.Count; i > TextHeadSphere; i--)
            {
                RemoveCircle();
            }
        } else { return; }

        if (DeadPanel.CompareTag("EasyDeadPanel"))
        {
            DeadPanelSound.Play();
            TextHeadSphere--;
                for (int i = bodyParts.Count; i > TextHeadSphere; i--)
                {
                    RemoveCircle();
                }
        }
        else { return; }
    }
    private void OnTriggerEnter(Collider Eat)
    {
        
        if (Eat.CompareTag("Eat"))
        {
            Yami.Play();
            EatForSphere E = Eat.GetComponent<EatForSphere>();
            for (int i = 0; i < E._Eat; i++)
            {
                AddBodyPart();
            }
            TextHeadSphere += E._Eat;
        }
        else { return; }
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        componentRigidbody.velocity = new Vector3(sidewaysSpeed * 5, 0f, ForwardSpeed);
        sidewaysSpeed = 0;
    }
}