using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;



public class SwitchPowerToCat : MonoBehaviour
{
    [Header("Power")]
    public GameObject Cat;
    public GameObject Player;
    public GameObject Turtle;
    public GameObject Cube;


    [Header("Settings")]
    public KeyCode SwitchPowerKeyNext = KeyCode.Q;
    public KeyCode SwitchPowerKeyBack = KeyCode.E;


    public float switchCooldown = 5f;
    public float cooldownTimer = 5f;


    [Header("UI Icon")]
    public Image PowerIcon;
    public Sprite CatIcon;
    public Sprite TurtleIcon;
    public Sprite CubeIcon;

    [Header ("Audio")]
    public AudioSource PowerSound;
    public AudioClip switchSound;

    [Header("PowerPickUp")]
    public GameObject PowerCatBox;


    int currentPower = 0;



   


    bool canSwitch = true;



    void Start()
    {
        UpdatePowerIcon();
    }




    void Update()
    {

        if (Input.GetKeyDown(SwitchPowerKeyNext))
        {

            NextPower();
        }

        if(Input.GetKeyDown(SwitchPowerKeyBack))
        {
                       PreviousPower();
        }


        //!PowerCatBox.activeSelf &&
        if ( Input.GetKeyDown(KeyCode.F) && canSwitch)
        {


    
                UseCurrentPower();
        }

    }




    void NextPower()
    {
               currentPower++;
        if (currentPower > 2)
        {
            currentPower = 0;
        }

        UpdatePowerIcon();
        ShowCurrentPower();

    }

    void PreviousPower()
    {
        currentPower--;
        if (currentPower < 0)
        {
            currentPower = 2;
        }

        UpdatePowerIcon();
        ShowCurrentPower();
    }


    void UpdatePowerIcon()
    {
        switch (currentPower)
        {
            case 0:
                PowerIcon.sprite = CubeIcon;
                Debug.Log("Cube");
                break;

            case 1:
                PowerIcon.sprite = CatIcon;

                Debug.Log("Cat");
                break;

            case 2:
                PowerIcon.sprite = TurtleIcon;

                Debug.Log("Turtle");
                break;

            default:
                break;
        }
        }


    void ShowCurrentPower()
    {


        switch (currentPower)
        {
            case 0:
                               Debug.Log("Cube");
                break;

            case 1:
                                Debug.Log("Cat");
                break;

                case 2:
                                Debug.Log("Turtle");
                break;

            default:
                break;

        }


    }

    void UseCurrentPower()
    {
        switch (currentPower)
        {
            case 0:
                StartCoroutine(CatPower());
                Debug.Log("Use Cube Power");
                break;
            case 1:
                StartCoroutine(TurtlePower());
                Debug.Log("Use Cat Power");
                break;
            case 2:
                StartCoroutine(PowerCube());
                Debug.Log("Use Turtle Power");
                break;
            default:
                break;
        }
    }

        IEnumerator CatPower()
   {



        canSwitch = false;



        //-----------------------------

        Vector3 pos = Player.transform.position;
        Cat.transform.position = pos;

        Cat.SetActive(true);
        Player.SetActive(false);
        switchSound = PowerSound.clip;
        PowerSound.PlayOneShot(switchSound);
  

        yield return new WaitForSeconds(switchCooldown);

        //---------------------------------------
        Vector3 pos2 = Cat.transform.position;
        Player.transform.position = pos2;

        Cat.SetActive(false);
        Player.SetActive(true);
        PowerSound.PlayOneShot(switchSound);
  


        canSwitch = true;
    }

    IEnumerator TurtlePower()
    {



        canSwitch = false;



        //-----------------------------

        Vector3 pos = Player.transform.position;
        Turtle.transform.position = pos;

        Turtle.SetActive(true);
        Player.SetActive(false);
        switchSound = PowerSound.clip;
        PowerSound.PlayOneShot(switchSound);


        yield return new WaitForSeconds(switchCooldown);

        //---------------------------------------
        Vector3 pos2 = Turtle.transform.position;
        Player.transform.position = pos2;

        Turtle.SetActive(false);
        Player.SetActive(true);
        PowerSound.PlayOneShot(switchSound);



        canSwitch = true;
    }

    IEnumerator PowerCube()
    {



        canSwitch = false;



        //-----------------------------

        Vector3 pos = Player.transform.position;
        Cube.transform.position = pos;

        Cube.SetActive(true);
        Player.SetActive(false);
        switchSound = PowerSound.clip;
        PowerSound.PlayOneShot(switchSound);


        yield return new WaitForSeconds(switchCooldown);

        //---------------------------------------
        Vector3 pos2 = Cube.transform.position;
        Player.transform.position = pos2;

        Cube.SetActive(false);
        Player.SetActive(true);
        PowerSound.PlayOneShot(switchSound);



        canSwitch = true;
    }




}