using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class replay : MonoBehaviour {
	GameObject levelGenerator;
	GameObject[] array;
	GameObject[] second;
	GameObject[] objects;

	void Start(){
		
	}

	public void refreshGame(){
		objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        levelGenerator = GameObject.FindGameObjectWithTag("levelGenerator");
		array = new GameObject[levelGenerator.transform.childCount];
		for (int i = 0; i < levelGenerator.transform.childCount; i++)
		{	
			array[i] = levelGenerator.transform.GetChild(i).gameObject;
			if(array[i].transform.childCount != 0){
				array[i].GetComponent<swipe>().childArray = new GameObject[array[i].transform.childCount];
			}
			Vector3 position = array[i].GetComponent<swipe>().startPosition;
			array[i].transform.position = position;
			
			if(array[i].transform.childCount>0){
				second = new GameObject[array[i].transform.childCount];
				for (int j = 0; j<array[i].transform.childCount; j++){
					second[j] = array[i].transform.GetChild(j).gameObject;
					Vector3 pos = second[j].GetComponent<swipe>().startPosition;
					second[j].transform.position = pos;
				}
			}
		}

		foreach (GameObject go in objects)
        {
			if(go.tag == "tomato" || go.tag == "lettuce" || go.tag == "bread" || go.tag == "cheese" || go.tag == "egg"){
				go.transform.parent = levelGenerator.transform;
				go.GetComponent<swipe>().enabled = true;	
				if(go.tag == "lettuce" || go.tag == "tomato" || go.tag == "egg"){
					go.GetComponent<CircleCollider2D>().enabled = true;
				}
				else{
					go.GetComponent<BoxCollider2D>().enabled = true;
				}
			}
			else{
				break;
			}
		}
    }
}