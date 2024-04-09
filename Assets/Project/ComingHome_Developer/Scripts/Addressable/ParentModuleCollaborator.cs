using AKRGS.Utilities;
using UnityEngine;

public class ParentModuleCollaborator : Singleton<ParentModuleCollaborator>
{
    public ChildModuleCollaborator childModuleCollaborator;

    public void AddActiveModule(ChildModuleCollaborator _childModuleCollaborator)
    {
        childModuleCollaborator = _childModuleCollaborator;
    }

    public void RemoveCurrentModule()
    {
        childModuleCollaborator = null;
    }
    //public 
    //public int GetWizzBux()
    //{
    //    if (childModuleCollaborator != null)
    //    {

    //        Debug.Log("called from parent modules GetWizzBux ");
    //        return 0;
    //    }
    //    else return -1;
    //}

    //public void SetTotalPoints(int _points)
    //{
    //    Debug.Log("called from parent modules total points");
        
    //}

    #region Gems/Coin Manager
    public int GetTotalGems()
    {
        Debug.Log("Get Total Gems");
        return 0;
    }

    public int GetCollectedGems()
    {
        Debug.Log("Get Collected Gems");
        return 0;
    }

    public void AddGems(int gemsToAdd)
    {
        Debug.Log("Add Gems to database");
    }

    public bool PayGems(int gemsToPay)
    {
        //Returns true if payable gems availabels 
        Debug.Log("Pay Gems");
        return true;
    }
    #endregion

    #region Component manager
    public void SetComponentOnDB(string compoName, bool status)
    {
        //If status true then add omponent to database otherwise remove it from database
    }

    public bool GetCompoStatus(string compoName)
    {
        //To check that components is available or not
        return true;
    }
    #endregion

    #region Color Manager

    public void InitialiseColorList(int listLength)
    {
        //listLength for number of parts of player which is using color
        //Initialize color to player when app opened first time or there is no color selected
    }

    public void StoreColor(Color color, int index)
    {
        //To store given color in the list using given part index
    }

    public Color GetColor(int index)
    {
        //To get color from list for given part index
        return Color.black;//For temporary use
    }

    public void StoreAllColorToDb()
    {
        //To store All color to data base
    }

    #endregion

    #region Unlock Level

    public void SetUnlockedLevelToDB(int levelCount)
    {
       
    }

    public int GetUnlockedLevel()
    {
        //To get total unlocked level
        return 0;
    }

    public void SetPreviousUnlockedLevel()
    {
        //To set previous unlocked using current unlocked level value, to move character icon from previous level
    }

    public int GetPreviousUnlockedLevel()
    {
        //To get previously unlocked level
        return 0;
    }

    #endregion

    #region Wizbux Manager

    public bool BuyWizbux(int requiredWizbux)
    {
        //To buy wizbux from available points
        return true;
    }

    public bool PayWizbux(int amount)
    {
        //To pay wizbux
        return true;
    }

    public int GetTotalWizbux()
    {
        //To get total wizbux
        return 0;
    }

    #endregion

    #region Time manager
    public void SetTimer(int minutes, int seconds)
    {
        //To set time for game play
    }

    public int GetMinuteValue()
    {
        //To get remaining minutes
        return 0;
    }

    public int GetSecondValue()
    {
        //To get remaining Seconds
        return 0;
    }
    #endregion
}
