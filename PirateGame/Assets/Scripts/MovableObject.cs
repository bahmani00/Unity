using UnityEngine;

public class MovableObject : MonoBehaviour
{

    public enum MoveOptions { UpDown, LeftRight };
    public MoveOptions movementOptions;

    public float movementSpeed = 125f;

    Rigidbody2D rb;

    void Start()
    {
        //Try and set rb to a rigidbody on our object/ship
        //If there is no rigidbody we will get an error
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not existing!");
        }

        if(movementOptions == MoveOptions.LeftRight)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;//Freezes rotation and up and down movement if ship goes left and right
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;//Freezes rotation of left and right movement
        }
    }

    //This runs the code inside of it the frame we click down
    private void OnMouseDown()
    {
        rb.isKinematic = false;//Allow us to move our ship
    }

    //This runs when we stop moving our ship
    private void OnMouseUp()
    {
        rb.isKinematic = true;//Stop moving our ship 
        rb.velocity = new Vector2(0, 0);//Stop moving completley
    }

    //This gets called when we are dragging the mouse over our ship
    private void OnMouseDrag()
    {
        //Vector3 is x,y,z coordinates

        //This turns our mouse point into usable 2d coordinates
        Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shipPos = transform.position; //This is our current ships position in coordinates

        Vector2 directionToMove = new Vector2(0, 0);//This is a variable which we will use to see whether we move the ship

        if (movementOptions == MoveOptions.UpDown)
        {
            //If our cursor is above the ship
            if (shipPos.y < touchPoint.y)
            {
                //We are going up
                directionToMove.y += touchPoint.y - shipPos.y;//We need to move up so set the directionToMove to be the distance between our cursor and the ship
            }
            else if (shipPos.y > touchPoint.y)//If the cursor is below the ship
            {
                //We are going down
                directionToMove.y -= shipPos.y - touchPoint.y; //We need to move down so set the directionToMove to be the distance between our cursor and the ship
            }
        }
        else
        {
            //If our cursor is to the left the ship
            if (shipPos.x < touchPoint.x)
            {
                //We are going left
                directionToMove.x += touchPoint.x - shipPos.x;//We need to move left so set the directionToMove to be the distance between our cursor and the ship
            }
            else if (shipPos.x > touchPoint.x)//If the cursor is right of the ship
            {
                //We are going right
                directionToMove.x -= shipPos.x - touchPoint.x; //We need to move right so set the directionToMove to be the distance between our cursor and the ship
            }
        }

        rb.velocity = new Vector2(0, 0);
        rb.AddForce(directionToMove * movementSpeed, ForceMode2D.Force);//Tells the rigidbody/physics calculator to move us in the direction we want

    }
}
