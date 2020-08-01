using UnityEngine;
using UnityEditor;


namespace DddShooter.Editor
{
    public class DddToolWindow : EditorWindow
    {
        #region PrivateData

        private enum Alignment { Min, Centre, Max };

        #endregion


        #region Fields

        public static GameObject ObjectToInstantiate;
        public static Transform CornerArea1;
        public static Transform CornerArea2;

        private float _stepX = 1.0f;
        private float _stepZ = 1.0f;

        private Alignment _xAlignment;
        private Alignment _zAlignment;

        private const int MIN_QUANTITY = 1;
        private const int MAX_QUANTITY = 1024;
        private const float HEIGHT_RAYCAST_START = 20.0f;
        private const float LENGHT_RAYCAST = 30.0f;

        #endregion


        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Space(20);

            ObjectToInstantiate = EditorGUILayout.ObjectField("Объект для копирования: ",
                ObjectToInstantiate, typeof(GameObject), true) as GameObject;
            CornerArea1 = EditorGUILayout.ObjectField("Первый угол области размещения: ",
                CornerArea1, typeof(Transform), true) as Transform;
            CornerArea2 = EditorGUILayout.ObjectField("Второй угол области размещения: ",
                CornerArea2, typeof(Transform), true) as Transform;

            _stepX = EditorGUILayout.FloatField("Шаг по X: ", _stepX);
            _stepZ = EditorGUILayout.FloatField("Шаг по Z: ", _stepZ);

            _xAlignment = (Alignment)EditorGUILayout.EnumPopup("Выравнивание по Х: ",
                _xAlignment);
            _zAlignment = (Alignment)EditorGUILayout.EnumPopup("Выравнивание по Z: ",
                _zAlignment);

            GUILayout.Space(20);

            if (GUILayout.Button("Разместить объекты"))
            {
                if (!CheckDataIsCorrect())
                {
                    Debug.LogWarning("Инструмент размещения объектов: некорректный ввод данных.");
                }
                else
                {
                    CreateObjects();
                }
            }
        }

        #endregion


        #region Methods

        [MenuItem("DddTools/Инструмент размещения объектов")]
        public static void ShowToolWidow()
        {
            EditorWindow.GetWindow(typeof(DddToolWindow));
        }

        private bool CheckDataIsCorrect()
        {
            if (ObjectToInstantiate == null)
            {
                return false;
            }
            if (CornerArea1 == null)
            {
                return false;
            }
            if (CornerArea2 == null)
            {
                return false;
            }
            if (_stepX < 0 || _stepZ < 0)
            {
                return false;
            }
            return true;
        }

        private void CreateObjects()
        {
            Vector3 corner1 = CornerArea1.position;
            Vector3 corner2 = CornerArea2.position;

            int counter = 0;

            float xStart = Mathf.Min(corner1.x, corner2.x);
            float xEnd = Mathf.Max(corner1.x, corner2.x);

            if (_xAlignment == Alignment.Max)
            {
                xStart += (xEnd - xStart) % _stepX;
            }
            else if (_xAlignment == Alignment.Centre)
            {
                xStart += _stepX / 2.0f;
            }

            float zStart = Mathf.Min(corner1.z, corner2.z);
            float zEnd = Mathf.Max(corner1.z, corner2.z);

            if (_zAlignment == Alignment.Max)
            {
                zStart += (zEnd - zStart) % _stepZ;
            }
            else if (_zAlignment == Alignment.Centre)
            {
                zStart += _stepZ / 2.0f;
            }

            float x = xStart;
            float z = zStart;

            GameObject root = new GameObject("Root");

            while (x <= xEnd)
            {
                z = zStart;
                while (z <= zEnd)
                {
                    CreateObject(x, z, root.transform);

                    if ( ++counter >= MAX_QUANTITY)
                    {
                        x = xEnd;
                        z = zEnd;
                    }

                    z += _stepZ;
                }
                x += _stepX;
            }
        }

        private void CreateObject(float x, float z, Transform root)
        {
            Vector3 startRayPosition = new Vector3(x, HEIGHT_RAYCAST_START, z);

            RaycastHit hit;

            if (Physics.Raycast(startRayPosition, Vector3.down, out hit, LENGHT_RAYCAST))
            {
                //GameObject temp = 
                    Instantiate(ObjectToInstantiate, hit.point, 
                    Quaternion.identity, root);
                // с установленным объектом делать ничего не надо.
            }
        }

        #endregion
    }
}