using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class sim : MonoBehaviour {

    simdata simdata = new simdata();

    public static void InitScene ()
    {
        // Resouces/CSV下のCSV読み込み 
        simdata.csvFile = Resources.Load("CSV/" + simdata.CSVName + simdata.CSVNumber) as TextAsset;
        StringReader reader = new StringReader(simdata.csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // 配列に格納
            simdata.csvDatas.Add(line.Split(','));
            // 行数加算
            simdata.height++;
        }

        // Resouces/CSV下のCSV読み込み (エリア用)
        simdata.csvFileB = Resources.Load("CSV/" + simdata.CSVNameB + simdata.CSVNumberB) as TextAsset;
        StringReader readerB = new StringReader(simdata.csvFileB.text);

        while (readerB.Peek() > -1)
        {
            string lineB = readerB.ReadLine();
            // 配列に格納
            simdata.csvDatasB.Add(lineB.Split(','));
            // 行数加算
            simdata.heightB++;
        }

        PlaceEdgeAndPoint();
        PlaceArea();
        //180825追加、面を別変数を用いて別関数で配置
        //PlaceArea();

        //なにに使ってるか分からない？
        /*for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps * 2 + 2; k++)
            {
                if (i % 2 == 0)
                {
                    if (k < simdata.N_Maps)
                    {
                        simdata.MapSprite[i,k] = simdata.MapObj[i,k].GetComponent<SpriteRenderer>();
                    }
                }
                else
                {
                    simdata.MapSprite[i, k] = simdata.MapObj[i, k].GetComponent<SpriteRenderer>();
                }
            }
        }*/
    }

    public static void PlaceEdgeAndPoint ()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        //線と点の配置(17~116まで)
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlag)
                        {
                            simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("PointBlack");
                            //
                            //simdata.MapObj[i, k].name = "MapObj";
                            simdata.ObjCount++;

                            simdata.MapObjClick[i, k] = simdata.MapObjEdgeAndPoint[i, k].GetComponent<CircleCollider2D>();
                            MapObjFlag = !MapObjFlag;
                        }
                        else
                        {
                            simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("EdgeUnderBlack");
                            MapObjFlag = !MapObjFlag;
                        }
                        simdata.MapObjEdgeAndPoint[i, k].name = "MapObj";
                    }
                }

                else
                {
                    if (MapObjFlagB)
                    {
                        simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("EdgeRightBlack");
                        MapObjFlagB = !MapObjFlagB;
                    }
                    else
                    {
                        simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("EdgeLeftBlack");
                        MapObjFlagB = !MapObjFlagB;
                        break;
                    }      
                    simdata.MapObjEdgeAndPoint[i, k].name = "MapObj";
                }
            }
        }



        for (int i = 0; i < simdata.N_Maps; i++)
        {

            for (int k = 0; k < simdata.N_Maps; k++)
            {

                simdata.MapCondition[i, k] = 0;
            }
        }

        float x = 1.36f;
        float y = 1.15f;
        bool MapObjFlagD = true;

        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlagC)
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 0);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) - x, simdata.MapStartPos.y + (y * -i), 0);
                                Debug.Log("Array2");
                            }
                        }
                        else
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + x, simdata.MapStartPos.y + (y * -i), 0);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) , simdata.MapStartPos.y + (y * -i), 0);

                            }
                        }
                    }
                }
                else
                {
                    simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + 0.68f, simdata.MapStartPos.y + (y * -i), 0);
                    MapObjFlagD = !MapObjFlagD;
                }
            }
            if (i % 2 == 0)
            {
                MapObjFlagC = !MapObjFlagC;
            }
        }
    }

    public static void PlaceArea()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        //面の配置(173~116まで)
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                }

                else
                {
                    if(MapObjFlagB)
                    {
                        simdata.MapObjArea[i, k] = Resources.Load<GameObject>("AreaDownBlack");
                        MapObjFlagB = !MapObjFlagB;
                    }
                    else
                    {
                        simdata.MapObjArea[i, k] = Resources.Load<GameObject>("AreaUpBlack");
                        MapObjFlagB = !MapObjFlagB;

                    }
                    simdata.MapObjArea[i, k].name = "MapObj";
                }
            }
        }

        float x = 1.36f;
        float y = 1.15f;
        bool MapObjFlagD = true;

        /*
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlagC)
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 0);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) - x, simdata.MapStartPos.y + (y * -i), 0);
                                Debug.Log("Array2");
                            }
                        }
                        else
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + x, simdata.MapStartPos.y + (y * -i), 0);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 0);

                            }
                        }
                    }
                }
                else
                {
                    simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + 0.68f, simdata.MapStartPos.y + (y * -i), 0);
                    MapObjFlagD = !MapObjFlagD;
                }
            }
            if (i % 2 == 0)
            {
                MapObjFlagC = !MapObjFlagC;
            }
        }
        */
    }

        /*------------------------------------------------------------------------------------*/
        //演算用関数
        public static void clickPoint(int y, int x)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        //クリックされた際、未取得なら自らを"2"に

        if (simdata.MapCondition[ya, xa] == 0)
        {


            //バックアップ保存
            for (y = 0; y <= 10; y++)
            {
                for (x = 0; x <= 10; x++)
                {
                    simdata.DefaultMapCondition[y, x] = simdata.MapCondition[y, x];

                    //0415
                    simdata.pastMapCondition[simdata.pastDoCount,y, x] = simdata.MapCondition[y, x];
                }
            }

            simdata.MapCondition[ya, xa] = 2;

            //初期座標入力

            simdata.FirstPoint.y = ya;
            simdata.FirstPoint.x = xa;

            simdata.CurrentPoint.y = ya;
            simdata.CurrentPoint.x = xa;

            //0415変記
            simdata.pastPoint[simdata.pastDoCount] = simdata.CurrentPoint;
            //↑変更後
            /*simdata.pastMapConditionB[simdata.pastDoCount,0] = (int)CurrentPoint.y;
            returnCanTakeB[returnCount][1] = (int)CurrentPoint.x;*/


            //クリックされた際、周囲の辺を"1"に

            for (y = ya - 1; y <= ya + 1; y++)
            {
                for (x = xa - 1; x < xa + 1; x++)
                {
                    if (simdata.MapCondition[y, x] == 0)
                    {
                        simdata.MapCondition[y, x] = 1;
                    }

                }
            }

            if (simdata.MapCondition[ya, xa + 1] == 0)
            {
                simdata.MapCondition[ya, xa + 1] = 1;
            }
        }

    }

    public static void changePointSprite(int y, int x)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        // SpriteRenderのspriteを設定済みの他のspriteに変更

        // 例) HoldSpriteに変更

        if (simdata.MapCondition[ya, xa] == 0)
        {
            //simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointBlack");
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointBlack");
        }

        if (simdata.MapCondition[ya, xa] == 1)
        {
            simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointGreen");
        }

        if (simdata.MapCondition[ya, xa] == 2)
        {
            //simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointBlue");
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointBlue");
            Debug.Log("Blue" +ya +xa+ simdata.MapCondition[ya,xa]);
        }

        if (simdata.MapCondition[ya, xa] == 3)
        {
            simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointRed");
        }

    }

    public static void changeEdgeSprite(int y, int x, int i)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        // SpriteRenderのspriteを設定済みの他のspriteに変更

        // 例) HoldSpriteに変更

        if (simdata.MapCondition[ya, xa] == 0)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderBlack");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftBlack");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightBlack");
                    break;
            }
        }

        if (simdata.MapCondition[ya, xa] == 1)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderGreen");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftGreen");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightGreen");
                    break;
            }
            Debug.Log("Green" + ya + xa + simdata.MapCondition[ya, xa]);
        }

        if (simdata.MapCondition[ya, xa] == 2)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderBlue");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftBlue");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightBlue");
                    break;
            }
            Debug.Log("Blue" + ya + xa + simdata.MapCondition[ya, xa]);
        }

        if (simdata.MapCondition[ya, xa] == 3)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderRed");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftRed");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightRed");
                    break;
            }
        }
    }

    /*----------------------------------------------------------------------------------------------------*/
    //ゲーム内の更新をここで行う

    public static void UpdateScene()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        //クリックの判定OnMOuseDown
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < simdata.N_Maps; i++)
            {
                for (int k = 0; k < simdata.N_Maps; k++)
                {
                    if (i % 2 == 0)
                    {
                        if (MapObjFlag && calc.HitTest(simdata.MapObjPos[i, k], Input.mousePosition))
                        {
                            //クリックでかつポイント座標に近い場合

                            Debug.Log("Click" + simdata.number);
                            simdata.number++;

                            //0415移植テスト
                            clickPoint(i, k);
                            //simdata.MapCondition[i, k] = 2;

                            MapObjFlag = !MapObjFlag;
                        }
                        else
                        {
                            MapObjFlag = !MapObjFlag;
                        }
                    }
                    else { }
                }
            }
        }

        MapObjFlag = true;

        //マウスが上を通った判定OnMouseEnter
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                    if (MapObjFlag && calc.HitTest(simdata.MapObjPos[i, k], Input.mousePosition))
                    {
                        Debug.Log("Enter");
                        MapObjFlag = !MapObjFlag;

                        //0415移植テスト
                        //DataBase2.enterPoint(ya, xa);
                    }
                    else
                    {
                        MapObjFlag = !MapObjFlag;
                    }
                }
            }
        }

        MapObjFlag = true;
        MapObjFlagB = true;

        //0415移植テスト
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            //180825 -1に変更
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0) //偶数の場合は線と点のみの列、奇数の場合はエッジ2種類とエリアの列
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlag)
                        {
                            changePointSprite(i, k);
                            
                            MapObjFlag = !MapObjFlag; //MapObjFlagのOn/Offで点とエッジを区別
                        }
                        else
                        {
                            //simdata.MapObj[i, k] = Resources.Load<GameObject>("EdgeUnderBlack");
                            changeEdgeSprite(i, k, 0);
                            MapObjFlag = !MapObjFlag;
                        }
                    }
                }

                else
                {
                    if (MapObjFlagB)
                    {
                        changeEdgeSprite(i, k, 2);
                        MapObjFlagB = !MapObjFlagB; //MapObjFlagBの数でエッジの左右とエリアを区別
                    }
                    else
                    {
                        changeEdgeSprite(i, k, 1);
                        MapObjFlagB = !MapObjFlagB;
                    }
                    /*
                        case (0):
                            //simdata.MapObj[i, k] = Resources.Load<GameObject>("EdgeRightBlack");
                            changeEdgeSprite(i, k, 2);
                            MapObjFlagB++; //MapObjFlagBの数でエッジの左右とエリアを区別
                            break;
                        case (1):
                            //simdata.MapObj[i, k] = Resources.Load<GameObject>("AreaDownBlack");

                            MapObjFlagB++;
                            break;
                        case (2):
                            // simdata.MapObj[i, k] = Resources.Load<GameObject>("EdgeLeftBlack");
                            changeEdgeSprite(i, k, 1);
                            MapObjFlagB++;
                            break;
                        case (3):
                           // simdata.MapObj[i, k] = Resources.Load<GameObject>("AreaUpBlack");
                            MapObjFlagB = 0;
                            break;
                        default:
                            MapObjFlagB = 0;
                            break;
                    */

                }
            }
        }
    }
}
