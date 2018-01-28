using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jext;

public class GearManager : MonoBehaviour
{
    public static GearManager self;

    [SerializeField]
    private KeyCode buttonSelectGear;
    [SerializeField]
    private float gearSpeed, gearMoveSpeed;
    [SerializeField]
    private ChangeGear chosenGear;
    private List<Gear> gears, childs;
    private Gear child;
    [SerializeField]
    private bool inverseRotation;
    [SerializeField]
    private Color color;

    private void Awake()
    {
        self = this;
    }

    private void Start()
    {
        GearEditor.InitGears();
        gears = GearEditor.self.gears;

        gears.ForEach(gear => gear.childs = new List<Gear>());
        foreach (Gear gear in gears)
            if (gear.parent != null)
                gear.parent.childs.Add(gear);
        
        chosenGear.gear.right = inverseRotation;
        childs = new List<Gear>();

        foreach (Gear gear in chosenGear.gear.childs)
        {
            gear.right = !chosenGear.gear.right;
            childs.Add(gear);
        }

        while(childs.Count > 0)
        {
            child = childs[0];
            childs.RemoveAt(0);

            foreach (Gear gear in child.childs)
            {
                gear.right = !child.right;
                childs.Add(gear);
            }
        }

        SetupGearRotation();
    }

    [SerializeField]
    private string axisRotation = "Horizontal";
    private float axisValue;
    private Vector3 speed;
    private List<Gear> open, closed, fitList;
    private Gear current;
    private void Update()
    {
        // "The thing people don't realize about the Gear Wars ..."
        axisValue = Input.GetAxis(axisRotation);
        if (Mathf.Approximately(0, axisValue))
            return;

        speed = chosenGear.transform.forward * axisValue * Time.deltaTime * gearSpeed;

        open = new List<Gear>() { chosenGear.gear };
        closed = new List<Gear>();
        fitList = new List<Gear>();

        while(open.Count > 0)
        {
            current = open[0];
            open.RemoveAt(0);
            closed.Add(current);
            fitList.Add(current);

            if (current.parent != null)
                if (!closed.Contains(current.parent) && !open.Contains(current.parent))
                    open.Add(current.parent);

            foreach (Gear gear in current.childs)
                if (!closed.Contains(gear) && !open.Contains(gear))
                    open.Add(gear);
        }

        foreach (Gear gear in fitList)
            gear.transform.Rotate(speed * (gear.right ? 1 : -1));
    }

    public void ChangeToNewGear(ChangeGear cGear)
    {
        chosenGear.SetColor(Color.white);
        chosenGear = cGear;
        SetupGearRotation();
    }

    private void SetupGearRotation()
    {
        chosenGear.SetColor(color);

        if(!chosenGear.gear.right)
            foreach (Gear gear in gears)
                gear.right = !gear.right;
    }

    #region Add / Remove gears

    public void Insert(Gear gear, Gear parent)
    {
        gear.parent = parent;
        parent.childs.Add(gear);
        gear.right = !parent.right;
    }

    public void Extrude(Gear gear)
    {
        if(gear.parent != null)
        {
            gear.parent.childs.Remove(gear);
            gear.parent = null;
        }

        foreach(Gear child in gear.childs)
            child.parent = null;
        gear.childs.Clear();
    }

    #endregion

    #region Move Gears

    private IEnumerator MoveGearToPos(Gear gear, Vector3 pos)
    {
        while(Vector3.Distance(gear.transform.position, pos) > 0.25f)
        {
            gear.transform.position = Vector3.MoveTowards(gear.transform.position, pos, gearMoveSpeed * Time.deltaTime);
            yield return null;
        }
        gear.transform.position = pos;
    }

    #endregion
}