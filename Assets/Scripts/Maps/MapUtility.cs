using ProjectYahtzee.Maps.Settings;
using UnityEngine;

namespace ProjectYahtzee.Maps
{
    public static class MapUtility
    {
        public static Vector3 NodeToWorldPosition(Vector2Int node)
        {
            float offset = (MapSettings.Settings.MapProperties.Size.x - 1) * MapSettings.Settings.NodeSpacing.x / 2f;
            if (node.y < 0)
            {
                return new Vector3(offset, 
                                   -(MapSettings.Settings.CapNodeOffset), 
                                   0);
            }
            
            if (node.y >= MapSettings.Settings.MapProperties.Size.y)
            {
                return new Vector3(offset, 
                                   (MapSettings.Settings.MapProperties.Size.y - 1) * MapSettings.Settings.NodeSpacing.y + MapSettings.Settings.CapNodeOffset, 
                                   offset);
            }

            return new Vector3(node.x * MapSettings.Settings.NodeSpacing.x,
                               node.y * MapSettings.Settings.NodeSpacing.y,
                               0f);
        }
    }
}