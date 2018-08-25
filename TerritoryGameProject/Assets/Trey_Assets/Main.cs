using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Main : MonoBehaviour {

    simdata simdata = new simdata();
    sim sim = new sim();
    DrawObj draw = new DrawObj();

	// Use this for initialization
	void Start () {
        sim.InitScene();
//DrawObj.DrawScene();
    }
	
	// Update is called once per frame
	void Update () {
        sim.UpdateScene();
        DrawObj.InitiarizeScene();
        DrawObj.DrawScene();
    }
}
