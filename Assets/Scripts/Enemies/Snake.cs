using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy {

    public float mindistance = 0.25f;

    public int beginsize;

    public float _Angle;
    public float _Period;
    private float _Time;

    public Sprite head;
    public GameObject bodyprefab;

    private float dis;
    private Transform CurBodyPart;
    private Transform PrevBodyPart;

    private void Start()
    {
        _Angle = Random.Range(_Angle, _Angle - 20);

        beginsize += (int)(GameController.gameStats["Score"] * 0.02f);

        Transform firstpart = (Instantiate(bodyprefab, transform.position, transform.rotation, transform) as GameObject).transform;
        firstpart.GetComponent<SpriteRenderer>().sprite = head;

        for (int i = 0; i < beginsize - 1; i++)
        {
            AddBodyPart();
        }

        if (transform.position.x > 1f)
            transform.position = new Vector3(1f,5f);
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (transform.childCount == 0)
            Destroy(gameObject);

        if (transform.childCount > 0)
        {
            // Move body part 0 down
            transform.GetChild(0).Translate((transform.GetChild(0).up * -1) * Speed * Time.deltaTime, Space.World);

            // Rotate body part 0
            _Time = _Time + Time.deltaTime;
            float phase = Mathf.Sin(_Time / _Period);
            transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, phase * _Angle));
        }
        
        for (int i = 1; i < transform.childCount; i++)
        {
            CurBodyPart = transform.GetChild(i);
            PrevBodyPart = transform.GetChild(i - 1);

            dis = Vector3.Distance(PrevBodyPart.position, CurBodyPart.position);

            Vector3 newpos = PrevBodyPart.position;

            newpos.z = transform.GetChild(0).position.z;

            float T = Time.deltaTime * dis / mindistance * Speed;

            if (T > 1f)
                T = 1f;

            CurBodyPart.position = Vector3.Slerp(CurBodyPart.position, newpos, T);
            CurBodyPart.rotation = Quaternion.Slerp(CurBodyPart.rotation, PrevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        Transform newpart = (Instantiate(
            original: bodyprefab, 
            position: transform.GetChild(transform.childCount - 1).position, 
            rotation: transform.GetChild(transform.childCount - 1).rotation,
            parent: transform) 
            as GameObject).transform;

        newpart.gameObject.name = "Snake(Clone)";
    }
}
