﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject Gemcounttext;
    public GameObject lifeText;
    public GameObject RedWall;
    public GameObject EndGoal;

    float Maxjump = 1f;
    float xlimit = 4.7f;
    float zlimit = -4.7f;
    float newzlimit = 14.7f;
    float newxlimit = 3.2f;
    float newxLimit = 48;
    float newZLimit = 4;
    float ylimit = -2;
    int Gemcount = 0;
    int totalGemcount;
    int Totallife = 3;
    bool defplane = false;
    bool groundA = false;
    bool groundC = false;
    public Animator PlayerAni;
    public Rigidbody PlayerRb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        JumpPlayer();

        //Find GameObject name Diamond
        totalGemcount = GameObject.FindGameObjectsWithTag("Diamond").Length;
        Gemcounttext.GetComponent<Text>().text = "Gem Remaining: " + totalGemcount;
        lifeText.GetComponent<Text>().text = "Life :" + Totallife;
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(0, 0.83f, 0);
            Totallife -= 1;
        }
        if (Totallife == 0)
        {
            //Getting pushed to out of bound limit will render player to a lose scene
            SceneManager.LoadScene("LoseScene");
        }
        
    }

    void Movement()
    {
        //movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayerAni.SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            PlayerAni.SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerAni.SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            PlayerAni.SetBool("Running", true);
        }
        //idle
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            PlayerAni.SetBool("Running", false);
        }

        if (defplane == true)
        {
            //Set border for player when on spawn plane
            if (transform.position.x >= xlimit)
            {
                transform.position = new Vector3(xlimit, transform.position.y, transform.position.z);
            }
            else if (transform.position.x <= -xlimit)
            {
                transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
            }
            if (transform.position.z <= zlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zlimit);
            }
        }

        if (groundA == true)
        {
            //set border for PlaneA
            if (transform.position.z >= newzlimit && transform.position.x >= xlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, newzlimit);
            }
            else if (transform.position.z >= newzlimit && transform.position.x <= -xlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, newzlimit);
            }
        }
        if (groundC == true)
        {
            if (transform.position.z >= newZLimit && transform.position.x >= newxLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, newZLimit);
            }
            else if (transform.position.z >= newZLimit && transform.position.x <= -newxLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, newZLimit);
            }
            if (transform.position.y <= ylimit)
            {
                transform.position = new Vector3(-6, 4, 43);
            }
        }



    }
    void JumpPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Maxjump == 0)
        {
            PlayerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            PlayerAni.SetTrigger("Jumping");
            Maxjump++;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SpawnPlane"))
        {
            //jump will reset upon touching the tag "Ground"
            print("You can't get out");
            defplane = true;
            Maxjump = 0;
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            //jump will reset upon touching the tag "Ground"
            print("Try and escape");
            groundA = true;
            defplane = false;
            Maxjump = 0;
        }
        if (collision.gameObject.CompareTag("Save"))
        {
            //jump will reset upon touching the tag "Save"
            print("You have reached the checkpoint");
            speed = 12f;
            defplane = false;
            Maxjump = 0;
        }
        if (collision.gameObject.CompareTag("GroundB"))
        {
            //jump will reset upon touching the tag "GroundB"
            print("You are almost there . . .");
            defplane = false;
            Maxjump = 0;
            groundA = true;
        }
        if (collision.gameObject.CompareTag("GroundC"))
        {
            defplane = false;
            groundC = true;
            groundA = false;
            Maxjump = 0;
        }
        if (collision.gameObject.CompareTag("Diamond"))
        {
            Destroy(collision.gameObject);
            Gemcount++;
            if (Gemcount == 3)
            {
                Destroy(RedWall);
            }
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Getting hit by either enemies will subtract one from life and will teleport to the starting point of that plane
            transform.position = new Vector3(0f, 0.83f, 16.68f);
        }
        if (collision.gameObject.CompareTag("EndGoal"))
        {
            SceneManager.LoadScene("WinScene"); 
        }
        
    }
}
