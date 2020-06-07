using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PMove : MonoBehaviour
{
    private float speed = 6f;

    Vector3 movement;                   
    Animator anim;                      
    Rigidbody playerRigidbody;          
    int floorMask;                      
    float camRayLength = 100f;

    

   
    public float rotSpeed = 80.0f;
    public float rot = 0.0f;

    public float Cnum = 0f;

    public Inventory inventory;
    public InventorySlot iSlot;
    private IInventoryItem mCurrentItem = null;
    public GameObject Hand;

    public Text Count;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        inventory.ItemUsed += Inventory_ItemUsed;
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);

        Count.text = Cnum.ToString("GEM : 0");
        if (Cnum == 3)
        {
            print("test");
        }



    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position /* Time.deltaTime*/;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);

        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }



	 void OnTriggerEnter(Collider hit)
    {
		
		IInventoryItem item = hit.GetComponent<Collider>().GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
            item.OnPickup();
            Cnum += 1;
        }
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (mCurrentItem != null)
        {
            SetItemActive(mCurrentItem, false);
        }
        IInventoryItem item = e.Item;
        SetItemActive(item, true);
        mCurrentItem = e.Item;
    }
    private void SetItemActive(IInventoryItem item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }
    
}

