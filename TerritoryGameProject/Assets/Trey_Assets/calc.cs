using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class calc : MonoBehaviour {

    public static bool HitTest (Vector3 obj,Vector3 mouse)
    {
        mouse.z = 10f;
        //Debug.Log(Camera.main.ScreenToWorldPoint(mouse));
        //Debug.Log(distanceAtoB(obj, Camera.main.ScreenToWorldPoint(mouse)));
        if (distanceAtoB(obj,Camera.main.ScreenToWorldPoint(mouse)) < 0.3) return true;
        //if (distanceAtoB(obj, mouse) < 0.0000002f) return true;
        else return false;
    }

    public static Vector3 directionAtoB (Vector3 obj, Vector3 mouse, Vector3 dir)
    {
        /*
        dir.x = mouse.x - obj.x;
        dir.y = mouse.y - obj.y;
        dir.z = mouse.z - obj.z;
        */
        dir.x = obj.x - mouse.x;
        dir.y = obj.y - mouse.y;
        dir.z = obj.z - mouse.z;
        return dir;
    }

    public static double distanceAtoB (Vector3 obj, Vector3 mouse)
    {
        Vector3 dir = new Vector3(0,0,0);
        dir = directionAtoB(obj, mouse, dir);
        return VectorNorm(dir);
    }

    public static double VectorNorm(Vector3 dir)
    {
        //Debug.Log("dir" + dir);
        double Sqrt = 0;
        Sqrt = System.Math.Sqrt(dir.x * dir.x + dir.y * dir.y + dir.z * dir.z);
        //Debug.Log("Sqrt" + Sqrt);
        return Sqrt;
    }

    //フェードイン・アウト関数   CanvasRenderer   インかアウト  何秒で  obj専用のcurrentTime型
    public static void FadeIO (CanvasRenderer obj, bool flag, float time, float currentTime)
    {
        //フェードイン
        if (flag)
        {
            if (obj.GetAlpha() < 1)
            {
                currentTime += Time.deltaTime;
                obj.SetAlpha(time / currentTime);
                //フェードが完了後、currentTimeを初期化
                if(obj.GetAlpha() >= 1)
                {
                    currentTime = 0;
                }
            }
        }
        //フェードアウト
        else
        {
            if (obj.GetAlpha() > 0)
            {
                currentTime += Time.deltaTime;
                obj.SetAlpha(1 - (time / currentTime));
                //フェードが完了後、currentTimeを初期化
                if (obj.GetAlpha() <= 0)
                {
                    currentTime = 0;
                }
            }
        }
    }
}
