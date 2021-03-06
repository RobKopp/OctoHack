﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniJSON;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class EditorControls : MonoBehaviour {
	

	static List<GameObject> mapping = new List<GameObject>();

	public string FileName;
	

	public static string FILE_PATH = "Assets/Levels/";




	static EditorControls() {
#if UNITY_EDITOR
		GameObject[] objs = Resources.LoadAll<GameObject>("LevelAssets");
		mapping.AddRange(objs);
#endif
	}

	public void Awake() {
#if !UNITY_EDITOR
		GameObject[] objs = Resources.LoadAll<GameObject>("LevelAssets");
		mapping.AddRange(objs);
#endif
	}

	public void ExportLevel() {
		Serialize();
	}

	public void Serialize () {

		StreamWriter sw = new StreamWriter(FILE_PATH+FileName+".beans");
		Debug.Log ("Exporting Level...");
		string serializedLevel = "";
		Dictionary<string, object> level;
		GameObject levelRoot = GameObject.FindGameObjectWithTag("LevelRoot");

		level = levelRoot.GetComponent<GameObjectDefinition>().GetSerialization();
		if(level.Count <=0){
			Debug.Log ("Level has no definition!");
		} else {
			serializedLevel += Json.Serialize(level);
			sw.Write(serializedLevel);
			sw.Flush();
			sw.Close();
			Debug.Log ("Level Saved");
		}

	}

	GameObject GetGameObjectByName(string name) {
		foreach(GameObject obj in mapping) {
			if(obj.name == name) {
				return obj;
			}
		}

		return null;
	}

	GameObject createChild(Dictionary<string,object> itemDef) {
		if(itemDef.ContainsKey("name")) {
			string name = (string)itemDef["name"];
			GameObject prefab = GetGameObjectByName(name);
			GameObject itemObj;
			if(prefab != null) {
#if UNITY_EDITOR
				itemObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#endif
#if !UNITY_EDITOR
				itemObj = Instantiate(prefab) as GameObject;
#endif
			} else {
				itemObj = new GameObject();
				itemObj.AddComponent<GameObjectDefinition>();
			}
			if(itemObj != null) {
				itemObj.name = (string)itemDef["friendlyName"];
				itemObj.transform.position = Json.ParseVector3((string)itemDef["pos"]);
				itemObj.transform.rotation = Quaternion.Euler(Json.ParseVector3((string)itemDef["rot"]));
				itemObj.transform.localScale = Json.ParseVector3((string)itemDef["scale"]);
				if(itemDef.ContainsKey("components")) {
					Dictionary<string,object> comps = itemDef["components"] as Dictionary<string, object>;
					foreach(string key in comps.Keys) {
						(itemObj.GetComponent(key) as ISerializable).DeSerialize(comps[key] as Dictionary<string,object>);
					}
				}
				if(itemDef.ContainsKey("children")) {
					List<object> children = itemDef["children"] as List<object>;
					foreach(object child in children) {
							Dictionary<string, object> childDef = child as Dictionary<string,object>;
							if(childDef != null) {
								GameObject myChild = createChild(childDef);
								if(myChild != null) {
									myChild.transform.parent = itemObj.transform;
								}
							}
					}
				}
				return itemObj;
			} else {
				Debug.LogWarning("Name not found in mapping: "+itemDef["name"]);
			}
		} else {
			Debug.LogWarning ("Name not found in definition.");
		}

		return null;
	}

	public void DestroyChildren(Transform parent) {
		List<GameObject> children = new List<GameObject>();
		foreach(Transform child in parent) {
			if(child.transform.childCount > 0) { 
				DestroyChildren(child);
			}
			children.Add (child.gameObject);
		}
		children.ForEach(child => DestroyImmediate(child));
	}
	

	public void LoadLevel(string levelFileName) {

		string fileToLoad = FileName;
		//IF we don't include a filename lets use the one defined in the editor
		if(levelFileName != "") {
			fileToLoad = levelFileName;
		}

		fileToLoad += ".beans";

		StreamReader sr = new StreamReader(FILE_PATH+fileToLoad);
		string levelDefinition = sr.ReadToEnd();
		sr.Close();

//		GameObject root = GameObject.Find ("LevelRoot");

//		if(root== null) {
//
//		} else {
//			if(root.transform.childCount > 0) {
//				DestroyChildren(root.transform);
//			}
//		}
		
//		root.name = "LevelRoot";
		Dictionary<string,object> level = (Dictionary<string,object>)MiniJSON.Json.Deserialize(levelDefinition);

		createChild(level);

//		List<object> level = (List<object>)MiniJSON.Json.Deserialize(levelDefinition);
//
//		foreach(object item in level) {
//			Dictionary<string,object> itemDef = item as Dictionary<string,object>;
//			GameObject child = createChild(itemDef);
//			child.transform.parent = root.transform;
//		}
	}
}
