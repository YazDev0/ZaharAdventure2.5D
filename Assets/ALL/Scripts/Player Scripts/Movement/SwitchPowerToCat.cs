using UnityEngine;
using System.Collections;
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

    [Header("UI Icon")]
    public Image PowerIcon;
    public Sprite CatIcon;
    public Sprite TurtleIcon;
    public Sprite CubeIcon;

    [Header("Audio")]
    public AudioSource PowerSound;
    public AudioClip switchSound;

    [Header("PowerPickUp")]
    public GameObject PowerCatBox;
    public GameObject PowerTurtleBox;
    public GameObject PowerCubeBox;

    int currentPower = 0;
    bool canSwitch = true;

    void Start()
    {
        // بدأ بأول قوة مفتوحة
        for (int i = 0; i <= 2; i++)
        {
            if (IsPowerUnlocked(i))
            {
                currentPower = i;
                break;
            }
        }

        UpdatePowerIcon();
    }

    void Update()
    {
        if (Input.GetKeyDown(SwitchPowerKeyNext))
        {
            NextPower();
        }

        if (Input.GetKeyDown(SwitchPowerKeyBack))
        {
            PreviousPower();
        }

        if (Input.GetKeyDown(KeyCode.F) && canSwitch)
        {
            UseCurrentPower();
        }
    }

    bool IsPowerUnlocked(int powerIndex)
    {
        switch (powerIndex)
        {
            case 0: return !PowerCubeBox.activeSelf;   
            case 1: return !PowerCatBox.activeSelf;    
            case 2: return !PowerTurtleBox.activeSelf; 
            default: return false;
        }
    }

    void NextPower()
    {
        int originalPower = currentPower;

        do
        {
            currentPower++;
            if (currentPower > 2) currentPower = 0;

            if (currentPower == originalPower)
                break;

        } while (!IsPowerUnlocked(currentPower));

        UpdatePowerIcon();
    }

    void PreviousPower()
    {
        int originalPower = currentPower;

        do
        {
            currentPower--;
            if (currentPower < 0) currentPower = 2;

            if (currentPower == originalPower)
                break;

        } while (!IsPowerUnlocked(currentPower));

        UpdatePowerIcon();
    }

    void UpdatePowerIcon()
    {
        if (IsPowerUnlocked(currentPower))
        {
            switch (currentPower)
            {
                case 0:
                    PowerIcon.sprite = CubeIcon;
                    PowerIcon.enabled = true;
                    Debug.Log("Cube - متاحة");
                    break;
                case 1:
                    PowerIcon.sprite = CatIcon;
                    PowerIcon.enabled = true;
                    Debug.Log("Cat - متاحة");
                    break;
                case 2:
                    PowerIcon.sprite = TurtleIcon;
                    PowerIcon.enabled = true;
                    Debug.Log("Turtle - متاحة");
                    break;
            }


        }
        else
        {
            PowerIcon.enabled = true;
            Debug.Log("القوة " + currentPower + " مقفولة - البحث عن قوة متاحة");

            // ابحث عن أي قوة متاحة
            for (int i = 0; i <= 2; i++)
            {
                if (IsPowerUnlocked(i))
                {
                    currentPower = i;
                    UpdatePowerIcon();
                    break;
                }
            }
        }
    }

    void UseCurrentPower()
    {
        if (!IsPowerUnlocked(currentPower))
        {
            Debug.Log("هذي القوة مقفولة!");
            return;
        }

        switch (currentPower)
        {
            case 0:
                StartCoroutine(CubePower());
                Debug.Log("استخدم قوة المكعب");
                break;
            case 1:
                StartCoroutine(CatPower());
                Debug.Log("استخدم قوة القطة");
                break;
            case 2:
                StartCoroutine(TurtlePower());
                Debug.Log("استخدم قوة السلحفاء");
                break;
        }
    }

    IEnumerator CatPower()
    {
        canSwitch = false;

        Vector3 pos = Player.transform.position;
        Cat.transform.position = pos;

        Cat.SetActive(true);
        Player.SetActive(false);
        PowerSound.PlayOneShot(switchSound);

        yield return new WaitForSeconds(switchCooldown);

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

        Vector3 pos = Player.transform.position;
        Turtle.transform.position = pos;

        Turtle.SetActive(true);
        Player.SetActive(false);
        PowerSound.PlayOneShot(switchSound);

        yield return new WaitForSeconds(switchCooldown);

        Vector3 pos2 = Turtle.transform.position;
        Player.transform.position = pos2;

        Turtle.SetActive(false);
        Player.SetActive(true);
        PowerSound.PlayOneShot(switchSound);

        canSwitch = true;
    }

    IEnumerator CubePower()
    {
        canSwitch = false;

        Vector3 pos = Player.transform.position;
        Cube.transform.position = pos;

        Cube.SetActive(true);
        Player.SetActive(false);
        PowerSound.PlayOneShot(switchSound);

        yield return new WaitForSeconds(switchCooldown);

        Vector3 pos2 = Cube.transform.position;
        Player.transform.position = pos2;

        Cube.SetActive(false);
        Player.SetActive(true);
        PowerSound.PlayOneShot(switchSound);

        canSwitch = true;
    }
}