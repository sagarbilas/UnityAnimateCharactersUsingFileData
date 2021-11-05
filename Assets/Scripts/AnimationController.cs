using System;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class Player1 : MonoBehaviour
{
    //public GameObject cubeTest;
    public Animator anim;
    public TextAsset csvFile;

    int N = 10400;

    private float[] xArr;
    private float[] zArr;

    private int index = 0;
    //private int frameRate = 0;

    public int playerIndex = 0;

    public float[] xDifference;
    public float[] zDifference;

    public float prevZ = 0.0f;
    public float prevX = 0.0f;

    //for rotation
    float angle;
    Quaternion targetRotation;
    Transform cam;
    public float rotationSpeed = 3.0f;

    public Vector3 last_pos = new Vector3(0, 0, 0);
    public double vel = 0;
    double timeGap = 0;
    void Start()
    {
        cam = Camera.main.transform;

        anim = GetComponent<Animator>();

        readCSV();
    }

    void Update()
    {
        //if (frameRate % 100 == 0)
        //{

        zDifference = new float[10400];
        xDifference = new float[10400];

        if (index < N)
        {
            float x = xArr[index] / 10.0f;
            float z = zArr[index] / 10.0f;
            if (index > 0)
            {
                angle = Mathf.Atan2((xArr[index] - xArr[index - 1]) / 10.0f, (zArr[index] - zArr[index - 1]) / 10.0f);
                angle = Mathf.Rad2Deg * angle;
            }
            //angle = cam.eulerAngles.y + angle;
            targetRotation = Quaternion.Euler(0, angle, 0);
            //rotate the characters so that the face on the direction of their movement
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //update the positions of the characters
            transform.position = new Vector3(xArr[index] / 10.0f, 0, zArr[index] / 10.0f);


            //if(index > 4)// && index % 5 == 0)
            if (index > 4 && index % 5 == 0)
            {
                float diffZ = Math.Abs((zArr[index] - zArr[index-5]));
                float diffX = Math.Abs((xArr[index] - xArr[index - 5]));
                float velZ = (float)(diffZ / timeGap);
                float velX = (float)(diffX / timeGap);
                vel = Math.Sqrt(velZ * velZ + velX * velX);
                print("index " + index + " velocity of player " + playerIndex + " is : " + vel);
                anim.SetFloat("Velocity", (float)vel, 0.05f, Time.deltaTime);
                //anim.SetFloat("Velocity", (float)vel);

                timeGap = 0;
            }

            timeGap += Time.deltaTime;

             //zDifference[index] = Math.Abs((zArr[index] - prevZ));
             //xDifference[index] = Math.Abs((xArr[index] - prevX));

             //float velZ = zDifference[index] / Time.deltaTime;
             //float velX = xDifference[index] / Time.deltaTime;
             //vel = Math.Sqrt(velZ * velZ + velX * velX);

            //prevZ = zArr[index];
            //prevX = xArr[index];
            //print("index " + index + " velocity of player " + playerIndex + " is : " + vel);
            //anim.SetFloat("Velocity", (float)vel);
            //anim.SetFloat("Velocity", (float)vel, 0.05f, Time.deltaTime);
            //anim.SetFloat("Velocity", (float)vel, 0.5f, Time.deltaTime * 10);

            /*  
              Vector3 current_pos = transform.position;
              float vel = (current_pos - last_pos).magnitude / Time.deltaTime;
              last_pos = current_pos;
              anim.SetFloat("Velocity", (float)vel);
            */
            index++;
        }
        else
        {
            anim.SetFloat("Velocity", 0.0f, 0.00f, Time.deltaTime);

        }
        //frameRate++;
        //print("Time.deltaTime: " + Time.deltaTime);
    }

    void readCSV()
    {
        //print("my index " + playerIndex);
        xArr = new float[N];
        zArr = new float[N];
        var dataset = Resources.Load<TextAsset>("test5");
        var splitDataset = dataset.text.Split('\n'); 
       
        for (int i = 0; i < splitDataset.Length; i++)
        {
            var data = splitDataset[i].Split(',');
            
            var x = data[playerIndex*2 + 0];
            var z = data[playerIndex*2 + 1];

            float x1 = float.Parse(x);
            float z1 = float.Parse(z);

            zArr[i] = z1;
            xArr[i] = x1;
        }
    }
}