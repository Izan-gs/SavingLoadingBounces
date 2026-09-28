using UnityEngine;
using UnityEngine.Events;

public class BounceController : MonoBehaviour
{
    [SerializeField] private int bounces = 0;
    public Rigidbody rb;
    [HideInInspector] public UnityEvent onBouncedOffGround = new UnityEvent();




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public int GetBounces() { return bounces; }




    // We increment bounces
    private void OnCollisionEnter(Collision collision)
    {
        bounces++;
        onBouncedOffGround.Invoke(); // Alert the UI that value changed
    }




    // Generates a DTO to send it to SaveSystem
    public BounceDto CaptureState()
    {
        BounceDto data = new BounceDto();
        data.savedBounces = this.bounces;
        return data;
    }
    // Apply the data from the DTO comming from the SaveSystem
    public void RestoreState(BounceDto data)
    {
        this.bounces = data.savedBounces;
        onBouncedOffGround.Invoke(); // Alert the UI that value changed
    }




    // Methods to turning on and off the gravity
    public void TurnOnGravity()
    {
        if (!rb.useGravity)
        {
            rb.linearVelocity = new Vector3(0f, 4f, 0f);
            rb.angularVelocity = new Vector3(0f, 4f, 0f);
        }
        rb.useGravity = true;
    }
    public void TurnOffGravity()
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
