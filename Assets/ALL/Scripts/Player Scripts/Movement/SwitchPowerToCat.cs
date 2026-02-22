using UnityEngine;
using System.Collections;
using System;




public class SwitchPowerToCat : MonoBehaviour
{
    [Header("Power")]
    public GameObject Cat;
    public GameObject Player;

    public float switchCooldown = 5f;
    public float cooldownTimer = 5f;


    [Header ("Audio")]
    public AudioSource PowerSound;
    public AudioClip switchSound;


    bool canSwitch = true;





    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canSwitch)
        {

            StartCoroutine(PowerUp());

        }

    }

        IEnumerator PowerUp()
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



}