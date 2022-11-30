using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem inputSystem;
    public GameObject drop;
    Rigidbody rig;
    public float spead;
    bool input = false;
    private void Awake()
    {
        rig = drop.GetComponent<Rigidbody>();
        inputSystem = new();
       
        inputSystem.Control.Jump.performed += context => rig.AddForce(0, 300, 0); ;
        inputSystem.Transformation.Steam.performed += context => Debug.Log("Steam");
        inputSystem.Transformation.Water_Ice.performed += context => Debug.Log("Ñìåííèë ñîñòîÿíèå");
        Debug.Log("ЛВЬУцщуабцщзуаб")
            ;
        var f = new Dictionary<string, string>()
        {
            {"выва","ВЫВФФФФ"}
        };


        var g = f["выва"];
        Debug.Log(g);
    }
    private void Update()
    {
        //if (inputSystem.Control.Move.ReadValue<Vector2>().x != 0 || inputSystem.Control.Move.ReadValue<Vector2>().y != 0)
        //    input = true;
        //if (input)
        //{
            Debug.Log("ВВОД00");
            rig.AddForce(inputSystem.Control.Move.ReadValue<Vector2>().x * spead, 0, inputSystem.Control.Move.ReadValue<Vector2>().y * spead);
            var lastLook = new Vector3(inputSystem.Control.Move.ReadValue<Vector2>().x * 90, 0, inputSystem.Control.Move.ReadValue<Vector2>().y * 90);
           // drop.transform.LookAt(new Vector3( - Camera.main.transform.position.x,0,- Camera.main.transform.position.z )  );
           
        

    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }


}
