using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;


 //get imput from player
 //apply movement to sprite
 private void Update()
 {
    float horizontal = Input.GetAxisRaw("Horizontal");  
    float vertical = Input.GetAxisRaw("Vertical");

    Vector3 direction = new Vector3(horizontal, vertical);
    transform.position += direction * speed * Time.deltaTime;
    
 }
}
