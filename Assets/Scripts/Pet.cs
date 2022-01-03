using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    public float hunger;
    public float happiness;    

    public float maxHunger;
    public float maxHappiness;

    public Text petName;
    public string petNameStr;
    public string petType;
    public int petTypeInt;

    public bool interactable;

    public float speed;
    public int point;

    public float timer;
    //time it takes for the pet to gain hunger and lose happiness
    public float timeTillDecay;
    public Player player;

    public bool startMoving;
    public Animator animator;


    public AudioClip[] SFX;
    AudioSource audiosource;

    public void Start()
    {        
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        StartCoroutine(TryToMove());        
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeTillDecay)
        {
            UpdatePets();           
        }

        if (startMoving)
        {            
            MoveToPoint();
        }
        

    }

    public void UpdatePets()
    {   
        hunger += 1;
        happiness -= 1;        
    }

    private void OnMouseDown()
    {
        player.selectedPet = this;
        player.UpdatePetStats();
    }

    //if pet dislikes food or the interaction they lose two happiness and gain two hunger
    public void Dislike()
    {
        hunger += 2;
        happiness -= 2;        
    }

    //function to check if pets happiness or hunger are at the threshold for interaction
    public void CheckIfInteractable()
    {
        if (hunger >= maxHunger || happiness <= 0)
        {
            interactable = false;
        }
        else
        {
            interactable = true;
        }
    }

    public void MoveToPoint()
    {
        Vector3 lastPos = transform.position;
       
        if (point == 4)
        {
            startMoving = false;
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, GetComponentInParent<MovePoints>().movementPoints[point].transform.position, speed * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position, GetComponentInParent<MovePoints>().movementPoints[point].transform.position) < 0.01f)
        {
            startMoving = false;
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);

        }

        if(lastPos.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(lastPos.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator TryToMove()
    {
        point = Random.Range(0, 4);
        startMoving = true;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", true);        
        yield return new WaitForSeconds(5f);

        StartCoroutine(TryToMove());
    }
    public void Petting()
    {
        audiosource.clip = SFX[0];
        audiosource.Play();
        hunger += 1;
        happiness += 1;
        player.UpdatePetStats();
    }

    

}

