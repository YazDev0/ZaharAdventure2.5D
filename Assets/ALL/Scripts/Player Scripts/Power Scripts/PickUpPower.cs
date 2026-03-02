using UnityEngine;

public class PickUpPower : MonoBehaviour
{

    public float rotationSpeed = 100f;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Pickup(other.gameObject);
    }

    void Pickup(GameObject player )
    {

  

        // ≈Œ›«¡ «·’‰œÊﬁ (√Ê  œ„Ì—Â)
        gameObject.SetActive(false);
    }
}