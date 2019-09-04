using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class swipe : MonoBehaviour {
    Vector2 firstPressPos;
	Vector3 secondPressPos;
	Vector3 currentSwipe;
    public Vector3 startPosition;
    GameObject[] array;
    public GameObject[] childArray;
    GameObject[] secondArray;
    GameObject generator;
    int number = 0;

	void Start () {
        startPosition = this.transform.position;
        generator = GameObject.FindGameObjectWithTag("levelGenerator");
        array = generator.GetComponent<levelGenerator>().array;
        foreach(GameObject obj in array){
            for(int i = 0; i<array.Length; i++){
                if(obj.name == array[i].name){
				    number ++;
				    obj.name = array[i].tag + number;
			    }
            }
		}
	}
	
	void Update () {
        array = generator.GetComponent<levelGenerator>().array;
        childArray = childCount();
        check();
	}

	void OnMouseDown(){
         {
          firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
         }
    }
    void OnMouseUp()
        {
           secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
           currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
           currentSwipe.Normalize();

           if (Mathf.Abs (currentSwipe.x) > Mathf.Abs (currentSwipe.y)){
                if(currentSwipe.x > 0){
                    Vector3 x_temp = new Vector3(2f,0,0);
                    moveX(x_temp);
                }

                if(currentSwipe.x < 0){
                    Vector3 x_temp = new Vector3(-2f,0,0);
                    moveX(x_temp);
                }
           }

           if (Mathf.Abs (currentSwipe.x) < Mathf.Abs (currentSwipe.y)){
                if(currentSwipe.y > 0){
                    Vector3 y_temp = new Vector3(0,2f,0);
                    moveY(y_temp);
                }

               if(currentSwipe.y < 0){
                    Vector3 y_temp = new Vector3(0,-2f,0);
                    moveY(y_temp);
                }
           }
    }

    void moveX(Vector3 x_temp){
        for(int j = 0; j<array.Length; j++){
            if(array[j].transform.position == this.transform.position + x_temp){
                this.transform.position = array[j].transform.position;
                createParent(j);
                break;
            }
        }
    }

    void moveY(Vector3 y_temp){
        for(int j = 0; j<array.Length; j++){
            if(array[j].transform.position == this.transform.position + y_temp){
                this.transform.position = array[j].transform.position;
                createParent(j);
                break;
            }
        }
    }

    void createParent(int j){
        array[j].transform.parent = this.transform;
        if(array[j].transform.childCount != 0){
            Transform[] children = array[j].GetComponentsInChildren<Transform>();
            for(int i = 0; i < children.Length; i++){
                children[i].transform.parent = this.transform;
                children[i].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.5f);
            } 
        }
        array[j].transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.5f);
        array[j].GetComponent<swipe>().enabled = false;
        if( array[j].tag == "tomato" || array[j].tag == "lettuce" || array[j].tag == "egg"){
            array[j].GetComponent<CircleCollider2D>().enabled = false;
        }
        else{
            array[j].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void check(){
        GameObject replay = GameObject.FindGameObjectWithTag("replay");
        GameObject next = GameObject.FindGameObjectWithTag("nextLevel");
        if(generator.transform.childCount != 1){
            replay.GetComponent<Canvas>().enabled = true;
        }
        if(generator.transform.childCount == 1){
            if(array[0].tag == "bread"){
                if(array[0].GetComponent<swipe>().childArray[array[0].transform.childCount-1].tag == "bread"){
                    Transform children = next.transform.GetChild(0);
                    children.gameObject.SetActive(true);
                    children.GetComponent<levelControl>().enabled = true;
                }
            }
        }
    }
    
    GameObject[] childCount(){
        secondArray = new GameObject[this.transform.childCount];
		for (int i = 0; i < this.transform.childCount; i++)
		{
			secondArray[i] = this.transform.GetChild(i).gameObject;
		}

        return secondArray;
    }
}