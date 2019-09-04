using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelControl : MonoBehaviour {
	GameObject generator;
	GameObject playAgain;
	GameObject[] array;
 	Texture2D map;
	string newName;
	string nameNumber;
	string[] splitArray;
	int levelNumber;

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			generator = GameObject.FindGameObjectWithTag("levelGenerator");
			array = generator.GetComponent<levelGenerator>().array;
        	foreach(GameObject obj in array){
				obj.transform.parent = null;
				Destroy(obj);
            }
			map = generator.GetComponent<levelGenerator>().map;
            splitArray =  map.name.Split(char.Parse("-"));
			levelNumber = int.Parse(splitArray[1]);
			levelNumber += 1;
			nameNumber = levelNumber.ToString();
			newName = "level-" + nameNumber;
			map = Resources.Load<Texture2D>("Textures/"+newName);
            if(map != null){
                generator.GetComponent<levelGenerator>().map = map;
                generator.GetComponent<levelGenerator>().Start();
				close();
            }
			else{
				close();
				playAgain = GameObject.FindGameObjectWithTag("completed");
				Transform children = playAgain.transform.GetChild(0);
				children.gameObject.SetActive(true);
                children.GetComponent<gameCompleted>().enabled = true;
			}
		}
	}

	void close(){
		this.gameObject.SetActive(false);
		this.GetComponent<levelControl>().enabled = false;
	}
}