using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : MonoBehaviour
{
    float speed = 0.01f;
    string moveDir = "left"; // ��������� ����������� ��������
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (moveDir)
        {
            case "forward":
                transform.position += new Vector3(0, 0, speed);
                break;
            case "left":
                transform.position += new Vector3(-speed, 0, 0);
                break;
            case "right":
                transform.position += new Vector3(speed, 0, 0);
                break;
            default:
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        string fileContents = "";
        string filePath = @"C:\Users\aklin\Desktop\results3.txt";
        if (File.Exists(filePath))
        {
            // ������ ���� � ������� ��� ���������� � �������
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
            {
                fileContents = reader.ReadToEnd();
            }
            Debug.Log(fileContents);
        }

        if (fileContents == "")
        {
            switch (other.gameObject.tag)
            {
                case "sensor1":
                    speed = 0.0005f;
                    break;
                case "sensor2":
                    speed = 0.01f;
                    break;
            }
        }
        if (fileContents == "1")
        {
            switch (other.gameObject.tag)
            {
                case "sensor1":
                    speed = 0.0005f;
                    break;
                case "sensor2":
                    speed = 0.01f;
                    break;
                case "sensor3":
                    speed = 0.00001f;
                    StartCoroutine(TurnAndMove());
                    break;
                case "sensor4":
                    speed = 0.0f; // ������������� ���
                    moveDir = ""; // �������� ����������� ��������
                    break;
                default:
                    break;
            }
        }


        if (fileContents == "2")
        {
            switch (other.gameObject.tag)
            {
                case "sensor1":
                    speed = 0.0005f;
                    break;
                case "sensor2":
                    speed = 0.01f;
                    break;
                case "sensor5":
                    speed = 0.00001f;
                    StartCoroutine(TurnAndMove());
                    break;
                case "sensor6":
                    speed = 0.0f; // ������������� ���
                    moveDir = ""; // �������� ����������� ��������
                    break;
                default:
                    break;
            }
        }

        if (fileContents == "3")
        {
            switch (other.gameObject.tag)
            {
                case "sensor1":
                    speed = 0.0005f;
                    break;
                case "sensor2":
                    speed = 0.01f;
                    break;
                case "sensor7":
                    speed = 0.00001f;
                    StartCoroutine(TurnAndMove());
                    break;
                case "sensor8":
                    speed = 0.0f; // ������������� ���
                    moveDir = ""; // �������� ����������� ��������
                    break;
                default:
                    break;
            }
        }

    }
    IEnumerator TurnAndMove()
    {
        transform.rotation *= Quaternion.Euler(0, 90, 0); // ������������ �� 90 �������� �� ��� Y
        yield return new WaitForSeconds(0.5f); // ������ ��������� �����
        speed = 0.01f; // ������ ��������
                       // ������������� ����� ����������� ��������
        if (moveDir == "forward")
        {
            moveDir = "left";
        }
        else if (moveDir == "left")
        {
            moveDir = "forward";
        }
    }
}