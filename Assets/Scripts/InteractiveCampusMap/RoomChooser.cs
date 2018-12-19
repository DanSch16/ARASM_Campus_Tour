using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomChooser : MonoBehaviour {
    public POIList POIList;
    //Create a List of new Dropdown options
    private List<string> m_DropOptions;
    //This is the Dropdown
    private Dropdown m_Dropdown;

    void Start()
    {
        m_DropOptions = new List<string> {};
        UpdateEntries();
    }

    private void UpdateEntries()
    {
        foreach (POI t in POIList.ListPOI)
        {
            m_DropOptions.Add(t.Name);
        }       
        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
    }
}
