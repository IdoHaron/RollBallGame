using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

interface IFall
{
    //Vector3 Place { set; get; }
    void Fall();
    void Distance_platform();
}
interface IPrintable
{
    void Print();
    void Un_print();
}
interface IDistance
{
    float Distance(Vector3 to);
}