using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class levelGenerator : MonoBehaviour {

	public Texture2D map;
	public colorToPrefab[] colorMappings;
	public GameObject ingClone;
	public GameObject[] array;
	GameObject[] childArray;

	public void Start () {
		GenerateLevel();
		array = new GameObject[this.transform.childCount];
		array = childCount();
	}

	void Update(){
		array = new GameObject[this.transform.childCount];
		array = childCount();
	}

	void GenerateLevel ()
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile (int x, int y)
	{
		Color pixelColor = map.GetPixel(x, y);
		if (pixelColor.a == 0)
		{
			return;
		}
		else{
			int number = 0;
			foreach (colorToPrefab colorMapping in colorMappings)
			{
				if (colorMapping.color.Equals(pixelColor))
				{
					Vector2 position = new Vector2(x-6.9f, y-4.09f);
					ingClone = Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
					ingClone.name = ingClone.tag + number;
				}
			}
		}
	}

	GameObject[] childCount(){
		childArray = new GameObject[this.transform.childCount];
		for (int i = 0; i < this.transform.childCount; i++)
		{
			childArray[i] = this.transform.GetChild(i).gameObject;
		}

		return childArray;
	}

}