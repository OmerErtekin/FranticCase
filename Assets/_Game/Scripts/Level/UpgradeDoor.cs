using _Game.Scripts.Controllers;
using _Game.Scripts.Level;
using _Game.Scripts.Player;

namespace _Game.Scripts
{
    public class UpgradeDoor : BaseDoor
    {
        protected override void OnContactedWithPlayer(PlayerController player)
        {
	        base.OnContactedWithPlayer(player);
	        GameController.Instance.UIController.UpgradeUI.ShowUpgradeUI();
        }
    }
}

