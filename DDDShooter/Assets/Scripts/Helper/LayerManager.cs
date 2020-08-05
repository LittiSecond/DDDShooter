using UnityEngine;


namespace DddShooter
{
    public sealed class LayerManager
    {
        #region PublicData

        public enum Layer
        {
            Default =          0,
            Ground =          10,
            MoveableObjects = 11,
            Player =          12,
            Interactable =    14,
            Enemy =           15
        }

        #endregion


        #region Fields

        public const string DEFOULT_LAYER_NAME = "Default";
        public const string GROUND_LAYER_NAME = "Ground";
        public const string MOVEABLEOBJECTS_LAYER_NAME = "MoveableObjects";
        public const string PLAYER_LAYER_NAME = "Player";
        public const string INTERACTABLE_LAYER_NAME = "Interactable"; 
        public const string ENEMY_LAYER_NAME = "Enemy";

        #endregion


        #region Methods

        public static LayerMask GetLayerMask(params Layer[] layers )
        {
            LayerMask layerMask = 0;

            if (layers.Length > 0)
            {
                foreach (Layer l in layers)
                {
                    layerMask += 1 << (int)l;
                }
            }

            return layerMask;
        }

        #endregion

    }
}
