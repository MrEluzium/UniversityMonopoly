using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    float x = 0.25f;
    float y = 0;
    float z = 0.07f;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            count++;
            switch (count)
            {
                case 2:
                    x = -0.3f;
                    z = -0.09f;
                    break;
                case 3:
                    x = 0.75f;
                    z = 0.3f;
                    break;
                case 4:
                    x = -0.75f;
                    z = -0.3f;
                    break;
                default:
                    break;
            }
            Debug.Log(x);
            Debug.Log(z);
            Instantiate(player, new Vector3(x, y, z), Quaternion.identity);

        }
    }
}
