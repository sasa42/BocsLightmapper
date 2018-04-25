namespace BOCSLIGHTMAPPER
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class BocsNodeBase
    {
        private List<BocsSlotBase> slots = new List<BocsSlotBase>();
        private Rect nodeRect = new Rect(0, 0, 130, 0);
        private string nodeTitle = "Node";
        private string nodeName = "Node";
        private int state = 0;

        public List<BocsSlotBase> Slots
        {
            get { return slots; }
            set { slots = value; }
        }

        public Rect NodeRect
        {
            get { return nodeRect; }
            set { nodeRect = value; }
        }

        public string NodeTitle
        {
            get { return nodeTitle; }
            set { nodeTitle = value; }
        }

        public string NodeName
        {
            get { return nodeName; }
            set { nodeName = value; }
        }

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public virtual void DrawNode(int id)
        {
#if UNITY_EDITOR
            //this.NodeRect = GUILayout.Window(id, this.NodeRect, this.Window, this.NodeTitle + id);
            this.NodeRect = GUILayout.Window(id, this.NodeRect, this.Window, this.NodeTitle);

            //float w = 64;
            //_nodeRect = GUILayout.Window(id,_nodeRect,Window, _nodeTitle,GUILayout.MaxWidth(w),GUILayout.MinWidth(w),GUILayout.Width(w));
            //_nodeRect.width = w;

            //Snap to grid...
            //float x = (int)(_nodeRect.xMin/8.0f) * 8;
            //float y = (int)(_nodeRect.yMin/8.0f) * 8;
            //_nodeRect = new Rect(x,y,_nodeRect.width,_nodeRect.height);
#endif
        }

        public virtual void Window(int id)
        {
#if UNITY_EDITOR
            if (this.State == 0)
            {
	            if (GUI.Button(new Rect(0, 0, 20, 16), "^"))
                {
                    //Debug.Log("Do Open");
                    this.State = 1;
                }
            }
            else
            {
	            if (GUI.Button(new Rect(0, 0, 20, 16), "."))
                {
                    //Debug.Log("Do Collapse");
                    this.State = 0;
                }
            }

            foreach (BocsSlotBase s in this.Slots)
            {
                s.DrawSlotGUI();
                s.AddSocket();
            }
            GUI.DragWindow();
            if (Event.current.GetTypeForControl(id) == EventType.Used)
            {
                //BocsNodeEditor._SelectedNode = id;
                BocsCyclesNodeManager.SelectedNode = id;
            }
#endif
        }

        public virtual void DrawSockets()
        {
#if UNITY_EDITOR
            foreach (BocsSlotBase s in this.Slots)
            {
                s.DrawSlotSocket();
            }
#endif
        }

        public virtual void DrawConnections()
        {
#if UNITY_EDITOR
            foreach (BocsSlotBase s in this.Slots)
            {
                foreach (BocsSlotBase c in s.OutputSlots)
                {
                    this.DrawNodeCurve(s.SlotRect, c.SlotRect);
                }
            }
#endif
        }

        public virtual void DrawNodeCurve(Rect start, Rect end)
        {
#if UNITY_EDITOR
            Vector3 startPos = new Vector3(start.x + (start.width / 2), start.y + (start.height / 2), 0);
            Vector3 endPos = new Vector3(end.x + (end.width / 2), end.y + (end.height / 2), 0);
            Vector3 startTan = startPos + (Vector3.right * 50);
            Vector3 endTan = endPos + (Vector3.left * 50);
            //Color shadowCol = new Color(0, 0, 0, 0.06f);
            UnityEditor.Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 3);
#endif
        }
    }
}