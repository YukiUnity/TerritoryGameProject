using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class simdata : MonoBehaviour {

    public static int N_Maps = 33;

    //MapObj描画
    public static List<GameObject> ObjList = new List<GameObject>();
    public static GameObject[,] MapObjEdgeAndPoint = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static GameObject[,] MapObjArea = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static GameObject[,] MapObjEdgeAndPointB = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static GameObject[,] MapObjAreaB = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static Vector3 MapStartPos = new Vector3(-8, 3, 0);
    public static Vector3[,] MapObjPos = new Vector3[N_Maps, N_Maps * 2 + 2];
    public static Vector3[,] MapObjPosArea = new Vector3[N_Maps, N_Maps * 2 + 2];
    public static int ObjCount = 0;
    public static GameObject obj;

    //MapObjゲーム内
    public static SpriteRenderer[,] MapSprite = new SpriteRenderer[N_Maps, N_Maps * 2 + 2];
    public static int[,] MapCondition = new int[N_Maps, N_Maps* 2 + 2];
    public static int[,] DefaultMapCondition = new int[N_Maps, N_Maps * 2 + 2];
    public static int[,,] pastMapCondition = new int[N_Maps * 3,N_Maps, N_Maps * 2 + 2];
    public static Vector2[] pastPoint = new Vector2[N_Maps * 3];
    public static int pastDoCount = 0;

    public static Vector2 FirstPoint;
    public static Vector2 CurrentPoint;


    public static CircleCollider2D[,] MapObjClick = new CircleCollider2D[N_Maps, N_Maps* 2 + 2];


    public static int number = 0;

    //csv
    public static string CSVName = "MapData180825v110";
    public static string CSVNumber = "";
    public static TextAsset csvFile; // CSVファイル
    public static List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリス
    public static string StringReader;
    public static int height = 0;

    public static string CSVNameB = "MapData_Area180825";
    public static string CSVNumberB = "";
    public static TextAsset csvFileB; // CSVファイル
    public static List<string[]> csvDatasB = new List<string[]>(); // CSVの中身を入れるリス
    public static string StringReaderB;
    public static int heightB = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
