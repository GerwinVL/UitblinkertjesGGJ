using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace RTS_Camera
{
    [CustomEditor(typeof(CamController))]
    public class CamController_Editor : Editor
    {
        private CamController Cam { get { return target as CamController; } }

        private TabsBlock tabs;

        private void OnEnable()
        {
            tabs = new TabsBlock(new Dictionary<string, System.Action>() 
            {
                {"Movement", MovementTab},
                {"Zooming", ZoomTab}
            });
            tabs.SetCurrentMethod(Cam.lastTab);
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            Undo.RecordObject(Cam, "RTS_Cam");
            tabs.Draw();
            if (GUI.changed)
            {
                Cam.lastTab = tabs.curMethodIndex;
            }
            EditorUtility.SetDirty(Cam);
        }

        private void MovementTab()
        {
            #region Keyboard Movement

                using (new HorizontalBlock())
                {
                    GUILayout.Label("Use keyboard input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                    Cam.useKeyboardInput = EditorGUILayout.Toggle( Cam.useKeyboardInput);
                }
                if(Cam.useKeyboardInput)
                {
                    Cam.horizontalAxis = EditorGUILayout.TextField("Horizontal axis name: ", Cam.horizontalAxis);
                    Cam.verticalAxis = EditorGUILayout.TextField("Vertical axis name: ", Cam.verticalAxis);
                    Cam.keyboardSpeed = EditorGUILayout.FloatField("Movement speed: ", Cam.keyboardSpeed);
                }

            #endregion

            #region Edge Movement

                using (new HorizontalBlock())
                {
                    GUILayout.Label("Screen edge input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                    Cam.useScreenEdgeInput = EditorGUILayout.Toggle( Cam.useScreenEdgeInput);
                }
                if(Cam.useScreenEdgeInput)
                {
                    EditorGUILayout.FloatField(new GUIContent("Screen Edge Border: ", "Width of scroll sensitive area in pixels"), Cam.screenEdgeBorder);
                    Cam.screenEdgeScrollSpeed = EditorGUILayout.FloatField("Screen edge movement speed: ", Cam.screenEdgeScrollSpeed);
                }

            #endregion

            #region Panning

                using (new HorizontalBlock())
                {
                    GUILayout.Label("Panning with mouse: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                    Cam.usePanning = EditorGUILayout.Toggle(Cam.usePanning);
                }
                if(Cam.usePanning)
                {
                    Cam.panningKey = (KeyCode)EditorGUILayout.EnumPopup("Panning when holding: ", Cam.panningKey);
                    Cam.panningSpeed = EditorGUILayout.FloatField("Panning speed: ", Cam.panningSpeed);
                }

            #endregion

            #region Limit movement

            using (new HorizontalBlock())
            {
                GUILayout.Label("Limit movement: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Cam.limitMovement = EditorGUILayout.Toggle(Cam.limitMovement);
            }
            if (Cam.limitMovement)
            {
                Cam.limitX = EditorGUILayout.FloatField("Limit X: ", Cam.limitX);
                Cam.limitY = EditorGUILayout.FloatField("Limit Y: ", Cam.limitY);
            }

            #endregion

        }

        private void ZoomTab()
        {
            #region Keyboard zooming

                using (new HorizontalBlock())
                {
                    GUILayout.Label("Keyboard zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                    Cam.useKeyboardZooming = EditorGUILayout.Toggle(Cam.useKeyboardZooming);
                }
                if(Cam.useKeyboardZooming)
                {
                    Cam.zoomInKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom In: ", Cam.zoomInKey);
                    Cam.zoomOutKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom Out: ", Cam.zoomOutKey);
                    Cam.keyboardZoomingSensitivity = EditorGUILayout.FloatField("Keyboard sensitivity: ", Cam.keyboardZoomingSensitivity);
                }

            #endregion

            #region Scrollwheel zooming

            using (new HorizontalBlock())
            {
                GUILayout.Label("Scrollwheel zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                Cam.useScrollwheelZooming = EditorGUILayout.Toggle(Cam.useScrollwheelZooming);
            }
            if (Cam.useScrollwheelZooming)
            {
                Cam.scrollWheelZoomingSensitivity = EditorGUILayout.FloatField("Scroll sensitivity: ", Cam.scrollWheelZoomingSensitivity);
                Cam.invertScrollwheel = EditorGUILayout.Toggle("Invert: ", Cam.invertScrollwheel);
            }

            EditorGUILayout.Space();

            #endregion

            if (Cam.useScrollwheelZooming || Cam.useKeyboardZooming)
            {
                using (new HorizontalBlock())
                {
                    //GUILayout.Label("Max Height: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                    Cam.minDistance = EditorGUILayout.FloatField("Min Distance: ", Cam.minDistance);
                    Cam.maxDistance = EditorGUILayout.FloatField("Max Distance: ", Cam.maxDistance);
                }
            }

        }

    }
}