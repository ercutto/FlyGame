using System.Collections;
using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class BatteryController : MonoBehaviour
    {

        void Start()
        {

            InvokeRepeating(nameof(CalculateBattery), 4, 3f);
        }

        void CalculateBattery()
        {

            StatsManager.instance.BatteryUsage(1);
        }

    }
}
