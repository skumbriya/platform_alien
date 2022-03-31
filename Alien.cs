using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alien : MonoBehaviour
{
    Transform playerBody;
    CharacterController contr;
    
    public float speed = 12f;

    bool isGrounded = false;
    bool isDestroyed = false;

    float gravityValue = -9.81f;

    float jumpHeight = 5f;

    static int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Transform>();
        contr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 5;
        float vertical = Input.GetAxis("Vertical");

        contr.Move(playerBody.forward * vertical * speed * Time.deltaTime);

        contr.Move(playerBody.up * gravityValue * Time.deltaTime);

        playerBody.Rotate(0,mouseX,0);

        if(Input.GetKeyDown("space") && isGrounded == true){
            contr.Move(playerBody.up * jumpHeight);

        }
        isGrounded = false;
        isDestroyed = false;
    }

    void OnControllerColliderHit(ControllerColliderHit  col){
        if(col.gameObject.tag == "ground"){
            isGrounded = true;
        }

        if(col.gameObject.tag == "coin" && isDestroyed == false){
            score = score + 1;
            Destroy(col.gameObject);
            print(score);
            scoreText.text = score + "";
            isDestroyed = true;
        }
    }
}
