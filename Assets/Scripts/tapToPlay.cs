﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tapToPlay : MonoBehaviour {

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			 SceneManager.LoadScene("Play");
		}
	}
}