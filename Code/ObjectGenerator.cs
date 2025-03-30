using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectPrefab;

    public GameObject ButtonPrefab;

    public GameObject Arrows;

    public GameObject CloneFolder1;
    public GameObject CloneFolder2;
    GameObject CloneFolder3;

    GameObject Parent1;
    GameObject Parent2;
    GameObject Parent3;

    GameObject button;
    GameObject ArrowC;
    GameObject ColorList;
    GameObject Des;

    public string funiture;

    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public Quaternion q;

    public bool t = false;
    public bool i = false;
    public bool mov = false;
    public bool Lock = false;
    float s ;


    public void Up()
    {
        Parent1 = transform.parent.gameObject;
        Parent2 = Parent1.transform.parent.gameObject;
        Parent3 = Parent2.transform.parent.gameObject;
                    
        Parent3.transform.position = Parent3.transform.position + new Vector3(0.2f, 0 ,0 );
    }

    public void Down()
    {
        Parent1 = transform.parent.gameObject;
        Parent2 = Parent1.transform.parent.gameObject;
        Parent3 = Parent2.transform.parent.gameObject;
                    
        Parent3.transform.position = Parent3.transform.position + new Vector3(-0.2f, 0 ,0 );
    }

    public void UpUp()
    {
        Parent1 = transform.parent.gameObject;
        Parent2 = Parent1.transform.parent.gameObject;
        Parent3 = Parent2.transform.parent.gameObject;
                    
        Parent3.transform.position = Parent3.transform.position + new Vector3(0, 0.2f ,0 );
    }

    public void DownDown()
    {
        Parent1 = transform.parent.gameObject;
        Parent2 = Parent1.transform.parent.gameObject;
        Parent3 = Parent2.transform.parent.gameObject;
                    
        Parent3.transform.position = Parent3.transform.position + new Vector3(0, -0.2f ,0 );
    }

    public void Left()
    {
        Parent1 = transform.parent.gameObject;
        Parent2 = Parent1.transform.parent.gameObject;
        Parent3 = Parent2.transform.parent.gameObject;
                    
        Parent3.transform.position = Parent3.transform.position + new Vector3(0, 0 ,0.2f );
    }

    public void Right()
    {
        Parent1 = transform.parent.gameObject;
        Parent2 = Parent1.transform.parent.gameObject;
        Parent3 = Parent2.transform.parent.gameObject;
                    
        Parent3.transform.position = Parent3.transform.position + new Vector3(0, 0 ,-0.2f );
    }

    public void GenerateObject()
    {
        Position = GlobalValue.Position + new Vector3(0, 0, 0);

        SetScale();

        q = new Quaternion(0, 0, 0, 0);
        q = Quaternion.Euler(Rotation);

        GameObject obj = Instantiate(objectPrefab, Position, q, CloneFolder2.transform);

        GlobalValue.Position = Position;

        obj.SetActive(true);

        obj.transform.localScale = Scale;
        //button = Instantiate(ButtonPrefab, Position, q);
        //button.transform.parent = obj.transform;
        //button.transform.localScale = Scale;
        //button.SetActive(false);
    }

    public void GenerateButton()
    {
        CloneFolder3 = this.transform.gameObject;
       

        if (t == true)
        {
            t = false;
            GlobalValue.Lock1 = false;
            int childcount;
            childcount = gameObject.transform.childCount;
            button = gameObject.transform.GetChild(childcount-1).gameObject;
            Destroy(button);
            // if(CloneFolder3.tag != "WiFi")
            // {
            //     ArrowC = gameObject.transform.GetChild(childcount-2).gameObject;
            //     Destroy(ArrowC);
            // }
            ArrowC = gameObject.transform.GetChild(childcount-2).gameObject;
            Destroy(ArrowC);
        }
        else
        {
            t = true;

            if(GlobalValue.Lock1 == true)
            {
                Des = GlobalValue.funiture;
                Des.GetComponent<ObjectGenerator>().t = false;
                int childcount;
                childcount = Des.gameObject.transform.childCount;
                button = Des.gameObject.transform.GetChild(childcount-1).gameObject;
                Destroy(button);
                
                // if(Des.tag != "WiFi")
                // {
                //     ArrowC = Des.gameObject.transform.GetChild(childcount-2).gameObject;
                //     Destroy(ArrowC);
                // }
                ArrowC = Des.gameObject.transform.GetChild(childcount-2).gameObject;
                Destroy(ArrowC);
            } 

            GlobalValue.funiture = this.transform.gameObject;
            GlobalValue.Lock1 = true;
            
            //Position = CloneFolder3.transform.position;
            Position = new Vector3(100,100,100);

            button = Instantiate(ButtonPrefab, Position, q);
            //button.transform.parent = CloneFolder3.transform;
            button.transform.parent = this.gameObject.transform;
            button.transform.localScale = Scale;
            button.SetActive(true);

            Position = gameObject.transform.position;
            Position.y = Position.y + 1f;
            Rotation = new Vector3(0, 0, 0);
            q = Quaternion.Euler(Rotation);

            
            if(CloneFolder3.tag != "WiFi")
            {
                ColorList = button.transform.GetChild(2).gameObject;
                ColorList.SetActive(false);

                ColorList = button.transform.GetChild(5).gameObject;
                ColorList.SetActive(false);


                // ArrowC = Instantiate(Arrows, Position, q);
                // ArrowC.transform.parent = this.gameObject.transform;
                // Scale = new Vector3(0.005f, 0.005f , 0.005f);
                // ArrowC.transform.localScale = Scale;
                // ArrowC.transform.position = Position;
                // ArrowC.SetActive(true);
            }
            else
            {
                if(Lock == false)
                {
                    ColorList = button.transform.GetChild(2).gameObject;
                    ColorList.SetActive(false);
                    ColorList = button.transform.GetChild(4).gameObject;
                    ColorList.SetActive(false);
                    ColorList = button.transform.GetChild(5).gameObject;
                    ColorList.SetActive(false);
                    ColorList = button.transform.GetChild(6).gameObject;
                    ColorList.SetActive(false);
                }
                else
                {
                    for(int i = 1;i<=5 ;i++)
                    {
                        ColorList = button.transform.GetChild(i).gameObject;
                        ColorList.SetActive(false);
                    }
                }   
            }
            ArrowC = Instantiate(Arrows, Position, q);
            ArrowC.transform.parent = this.gameObject.transform;
            Scale = new Vector3(0.005f, 0.005f , 0.005f);
            ArrowC.transform.localScale = Scale;
            ArrowC.transform.position = Position;
            ArrowC.SetActive(true);
        }

    }

    public void Rotate()
    {
        CloneFolder3 = transform.parent.gameObject;
        CloneFolder3 = CloneFolder3.transform.parent.gameObject;

        var angles = CloneFolder3.transform.rotation.eulerAngles;
        angles.y += 15;
        CloneFolder3.transform.rotation = Quaternion.Euler(angles);

    }

    void SetScale()
    {
        float mult;

        s = GlobalValue.factor.x;

        if(s < GlobalValue.factor.y)
        {
            s = GlobalValue.factor.y;
        }
        if(s < GlobalValue.factor.z)
        {
            s = GlobalValue.factor.z;
        }
        
        switch(objectPrefab.tag)
        {
            case "cabinet_1": 
            case "cabinet_2":
            case "cabinet_3":
            case "chair_1":
            case "chair_2":
            case "kitchen_chair_1":
            case "kitchen_chair_2":
            {
                mult = 1.5f;
                break;
            }
            case "single_1":
            case "single_2":
            case "single_3":
            case "mirror_3":
            case "double_4":
            case "double_5":
            case "double_6":
            case "double_7":
            case "double_8":
            case "L_sofa_1":
            case "L_sofa_2":
            case "L_sofa_3":
            case "L_sofa_4":
            case "kitchen_table_1":
            case "kitchen_table_2":
            case "table_1":
            case "table_2":
            case "tv_table_1":
            case "tv_table_2":
            case "tv_table_3":
            case "shower":
            case "brush":
            case "drying_with_towel":
            {
                mult = 1.25f;
                break;
            }
            case "torchere_1":
            case "torchere_2":
            case "torchere_3":
            case "torchere_4":
            {
                mult = 0.625f;
                break;
            }
            case "wall_lighter":
            {
                mult =2.5f;
                break;
            }
            case "double_1":
            {
                mult =0.5f;
                break;
            }
            case "double_2":
            {
                mult =0.25f;
                break;
            }
            case "double_3":
            {
                mult =1.375f;
                break;
            }
            case "coffee_table_1":
            case "coffee_table_3":
            case "coffee_table_5":
            {
                mult =1.875f;
                break;
            }
            case "Window2":
            {
                mult =0.01f;
                break;
            }
            case "flasket":
            {
                mult =-0.125f;
                break;
            }
            default:
            {
                mult = 1;
                break;
            }
        }
        Scale = new Vector3(1f * s * mult, 1f * s * mult ,1f * s * mult);

        switch(objectPrefab.tag)
        {
            case "WiFi":
            case "Door1":
            case "Door2":
            case "Door3":
            case "Window1":
            case "double_7":
            case "L_sofa_2":
            {
                Rotation = new Vector3(0, 0, 0);
                break;
            }
            case "L_sofa_4":
            case "L_sofa_1":
            case "double_1":
            case "double_2":
            case "double_8":
            case "single_1":
            {
                Rotation = new Vector3(0, 180, 0);
                break;
            }
            default:
            {
                Rotation = new Vector3(-90, 0, 0);
                break;
            }
        }
    }
}
