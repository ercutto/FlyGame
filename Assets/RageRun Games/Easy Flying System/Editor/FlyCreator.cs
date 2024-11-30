using UnityEditor;
using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public static class FlyCreator
    {
        public static GameObject flyObject;
        public static DroneController DroneController;

        [MenuItem("Tools/Easy Flying System/Create Flyable Object For Keyboard")]
        [MenuItem("GameObject/Easy Flying System/Create Flyable Object For Keyboard")]
        public static void CreateFlyableKeyboardObject()
        {
            MobileInputHandler inputHandler = GameObject.FindFirstObjectByType<MobileInputHandler>();

            if (inputHandler != null)
            {
                GameObject.DestroyImmediate(inputHandler);
            }

            GameObject mobileUIScreen = GameObject.Find("Mobile Controls UI Holder");

            if (mobileUIScreen != null)
            {
                GameObject.DestroyImmediate(mobileUIScreen);
            }

            CreateDrone(InputType.Keyboard);

            if (flyObject.GetComponent<KeyboardInputHandler>() == null)
            {
                flyObject.AddComponent<KeyboardInputHandler>();
            }
        }

        [MenuItem("Tools/Easy Flying System/Create Flyable Object For Mobile")]
        [MenuItem("GameObject/Easy Flying System/Create Flyable Object For Mobile")]
        public static void CreateFlyableMobileObject()
        {
            CreateDrone(InputType.Mobile);
            
            GameObject mobileControls = GameObject.Find("Mobile Controls UI Holder");

            if (mobileControls == null)
            {
                mobileControls = GameObject.Instantiate(Resources.Load<GameObject>("Mobile Controls UI Holder"), flyObject.transform);
                mobileControls.name = "Mobile Controls UI Holder";
            }

            if (flyObject.GetComponent<MobileInputHandler>() == null)
            {
                MobileController[] controllers = flyObject.GetComponentsInChildren<MobileController>();;
                flyObject.AddComponent<MobileInputHandler>().SetMobileInputControllers(controllers);
            }
        }
        
        [MenuItem("Tools/Easy Flying System/Create Flyable Object For Mouse")]
        [MenuItem("GameObject/Easy Flying System/Create Flyable Object For Mouse")]
        public static void CreateFlyableMouseObject()
        {
            MobileInputHandler inputHandler = GameObject.FindFirstObjectByType<MobileInputHandler>();

            if (inputHandler != null)
            {
                GameObject.DestroyImmediate(inputHandler);
            }

            GameObject mobileUIScreen = GameObject.Find("Mobile Controls UI Holder");

            if (mobileUIScreen != null)
            {
                GameObject.DestroyImmediate(mobileUIScreen);
            }

            CreateDrone(InputType.Mouse);

            if (flyObject.GetComponent<MouseInputHandler>() == null)
            {
                flyObject.AddComponent<MouseInputHandler>();
            }
        }

        private static void CreateDrone(InputType inputType)
        {
            AddOrCreateModelObject();
            AddOrGetPhysicsComponents();
            AddOrGetFlyController(inputType);
            AddOrGetCamera();
        }

        private static void AddOrCreateModelObject()
        {
            DroneController = GameObject.FindFirstObjectByType<DroneController>();

            if (DroneController != null)
            {
                GameObject.DestroyImmediate(DroneController.gameObject);
            }

            if (flyObject == null)
            {
                flyObject = new GameObject("Flying Object");
                GameObject newModel = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newModel.transform.parent = flyObject.transform;
            }

            flyObject.transform.position = new Vector3(0f, 1.25f, 0f);
        }

        private static void AddOrGetPhysicsComponents()
        {
            if (flyObject.TryGetComponent(out Collider collider))
            {
                if (collider == null)
                {
                    flyObject.AddComponent<BoxCollider>();
                }
            }

            Rigidbody rigidbody = flyObject.GetComponent<Rigidbody>();

            if (rigidbody == null)
            {
                rigidbody = flyObject.AddComponent<Rigidbody>();
            }

            rigidbody.freezeRotation = true;
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            rigidbody.angularDamping = 0.05f;
            rigidbody.linearDamping = 0.6f;
        }

        private static void AddOrGetFlyController(InputType inputType)
        {
            if (DroneController == null)
            {
                DroneController = flyObject.AddComponent<DroneController>();
                DroneController.inputType = inputType;
            }
        }

        private static void AddOrGetCamera()
        {
            Camera mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogWarning("No main camera found in the scene");
                return;
            }

            if (mainCamera.GetComponent<CameraController>() == null)
            {
                mainCamera.gameObject.AddComponent<CameraController>().SetTarget(flyObject.transform)
                    .SetOffset(new Vector3(0f, 8f, -12f));
            }
            else
            {
                mainCamera.GetComponent<CameraController>().SetTarget(flyObject.transform)
                    .SetOffset(new Vector3(0f, 8f, -12f));
            }
        }
    }
}