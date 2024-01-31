using System.Collections.Generic;
using _Game.Scripts.Controllers;
using UnityEngine;

namespace _Game.Scripts.Level
{
    public class LevelParent : MonoBehaviour
    {
    	#region Variables
	    [SerializeField] private List<Obstacle> _obstacles = new();
	    [SerializeField] private List<BaseDoor> _doors = new();
	    [SerializeField] private Transform _endPoint;
	    #endregion

	    [Header("For Level Editor")] 
	    [SerializeField] private Transform _levelPlane;
	    [SerializeField] private float _levelZLength = 100;

	    public void InitilazeLevel()
	    {
		    gameObject.SetActive(true);
		    foreach(var obstacle in _obstacles)
		    {
			    obstacle.ResetObstacle();
		    }

		    foreach (var door in _doors)
		    {
			    door.ResetDoor();
		    }
		    
		    EventManager.TriggerEvent(EventManager.OnLevelInitialized);
	    }

	    public void CloseLevel()
	    {
		    //We can add another things to here so i will do it as a methode.
		    gameObject.SetActive(false);
	    }
		//Functions to have faster level generation
	    #region Editor Utilitys
	    public void SetLengthOfLevel()
	    {
		    _levelPlane.transform.localScale = new Vector3(_levelPlane.transform.localScale.x, _levelPlane.transform.localScale.y, _levelZLength);
		    _levelPlane.transform.localPosition = new Vector3(0, -2.25f, _levelZLength / 2);
		    DistributeDoors();
	    }
	    public void GetAllObstacles()
	    {
		    _obstacles.Clear();
		    var obstacles = transform.GetComponentsInChildren<Obstacle>();
		    foreach (var obstacle in obstacles)
		    {
			    _obstacles.Add(obstacle);
		    }
	    }

	    public void GetAllDoors()
	    {
		    _doors.Clear();
		    var doors = transform.GetComponentsInChildren<BaseDoor>();
		    foreach (var door in doors)
		    {
			    _doors.Add(door);
		    }
	    }

	    public void DistributeDoors()
	    {
		    GetAllDoors();
		    LevelCompleteDoor completeDoor = null;
		    var upgradeDoorsToDistrubute = new List<BaseDoor>();
		    foreach (var door in _doors)
		    {
			    if (!completeDoor)
			    {
				    door.TryGetComponent(out completeDoor);
			    }
			    if (door.TryGetComponent(out UpgradeDoor upgradeDoor))
			    {
				    upgradeDoorsToDistrubute.Add(upgradeDoor);
			    }
		    }

		    if (!completeDoor)
		    {
			    Debug.LogError("You should add complete door as a child of level parent!",transform);
			    return;
		    }

		    if (upgradeDoorsToDistrubute.Count != Constants.TOTAL_COUNT_OF_UPGRADE)
		    {
			    Debug.LogError($"There should be {Constants.TOTAL_COUNT_OF_UPGRADE} of upgrade doors");
			    return;
		    }
		    
		    completeDoor.transform.position = new Vector3(0,0,_endPoint.position.z);
		    
		    //Assume that all levels are starting from 0,0,0
		    //we calcule x points between a and b. So we will seperate this distance to (x - 1) part.
		    //and distribute upgrade doors on these points.
		    var levelLength = _endPoint.position.z;
		    var startZ = levelLength * Constants.UPGRADE_START_PERCENTAGE;
		    var endZ = levelLength * Constants.UPGRADE_END_PERCENTAGE;
		    var distanceBetweenUpgrades = (endZ - startZ) / (Constants.TOTAL_COUNT_OF_UPGRADE - 1);
            

		    for (var i = 0; i < Constants.TOTAL_COUNT_OF_UPGRADE; i++)
		    {
			    var doorPosition = new Vector3(Random.Range(-1.75f, 1.75f), 0, startZ + distanceBetweenUpgrades * i);
			    upgradeDoorsToDistrubute[i].transform.position = doorPosition;
		    }
	    }
	    #endregion
    }
}

