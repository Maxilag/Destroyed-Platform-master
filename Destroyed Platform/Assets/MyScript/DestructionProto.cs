using UnityEngine;
using System.Collections;

public class DestructionProto : MonoBehaviour
{
    private GameObject surfaceDetruite1;
    private GameObject surfaceDetruite2;
    private GameObject surfaceDetruite3;
    private GameObject surfaceDetruite4;
    private bool changerPlateforme;
    private float destruction1, destruction2, destruction3;

    void Start()
    {
        surfaceDetruite1 = GameObject.Find("surfaceDetruite1");
        surfaceDetruite2 = GameObject.Find("surfaceDetruite2");
        surfaceDetruite2.SetActive(false);
        surfaceDetruite3 = GameObject.Find("surfaceDetruite3");
        surfaceDetruite3.SetActive(false);
        surfaceDetruite4 = GameObject.Find("surfaceDetruite4");
        surfaceDetruite4.SetActive(false);
        changerPlateforme = true;
        destruction1 = 5.0f;
        destruction2 = 10.0f;
        destruction3 = 15.0f;
    }

    void Update()
    {
        if (Time.time > destruction1)
        {
            surfaceDetruite1.SetActive(false);
            surfaceDetruite2.SetActive(true);
        }

        if (Time.time > destruction2)
        {
            surfaceDetruite2.SetActive(false);
            surfaceDetruite3.SetActive(true);
        }

        if (Time.time > destruction3)
        {
            surfaceDetruite3.SetActive(false);
            surfaceDetruite4.SetActive(true);
        }
    }

    //== destruction si le personnage se trouve sur la case

    /*    void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.name == "Box001")
            {
                changerPlateforme = true;
            }
            if (col.gameObject.name == "surfaceDetruite1" && changerPlateforme)
            {
                surfaceDetruite1.SetActive(false);
                surfaceDetruite2.SetActive(true);
                changerPlateforme = false;
            }
            if (col.gameObject.name == "surfaceDetruite2" && changerPlateforme)
            {
                surfaceDetruite2.SetActive(false);
                surfaceDetruite3.SetActive(true);
                changerPlateforme = false;
            }
            if (col.gameObject.name == "surfaceDetruite3" && changerPlateforme)
            {
                surfaceDetruite3.SetActive(false);
                surfaceDetruite4.SetActive(true);
                changerPlateforme = false;
            }
        }  */
}
