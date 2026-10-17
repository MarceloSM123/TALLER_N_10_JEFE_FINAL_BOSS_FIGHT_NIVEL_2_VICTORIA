using UnityEngine;

public class CameraFlow : MonoBehaviour
{
       public Transform target;
    public float smoothSpeed=5f;
    public Vector3 offSet=new Vector3(0,1,-10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate(){
if(target != null){
    Vector3 desirePosition = target.position + offSet;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position,desirePosition,smoothSpeed * Time.deltaTime);
    transform.position = smoothedPosition;
       }
     }
}
