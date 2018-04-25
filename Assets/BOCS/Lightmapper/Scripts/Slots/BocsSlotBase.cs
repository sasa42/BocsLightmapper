namespace BOCSLIGHTMAPPER
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class BocsSlotBase
    {
        private BocsNodeBase node;

        private BocsSlotType slotType = BocsSlotType.Value;
        private string slotTitle = string.Empty;
        private string slotName = string.Empty;

        private BocsSlotBase inputSlot = null;
        private List<BocsSlotBase> outputSlots = new List<BocsSlotBase>();

        private Rect slotRect = new Rect(0, 0, 0, 0);
        private Color slotColor = Color.white;

        public BocsSlotBase(BocsNodeBase n, string title, string name, BocsSlotType type)
        {
            Node = n;
            SlotType = type;
            SlotTitle = title;
            SlotName = name;
            InputSlot = null;
            OutputSlots = new List<BocsSlotBase>();
        }

        public enum BocsSlotType
        {
            Value, Input, Output
        }

        public BocsNodeBase Node
        {
            get { return node; }
            set { node = value; }
        }

        public BocsSlotType SlotType
        {
            get { return slotType; }
            set { slotType = value; }
        }

        public string SlotTitle
        {
            get { return slotTitle; }
            set { slotTitle = value; }
        }

        public string SlotName
        {
            get { return slotName; }
            set { slotName = value; }
        }

        public BocsSlotBase InputSlot
        {
            get { return inputSlot; }
            set { inputSlot = value; }
        }

        public List<BocsSlotBase> OutputSlots
        {
            get { return outputSlots; }
            set { outputSlots = value; }
        }

        public Rect SlotRect
        {
            get { return slotRect; }
            set { slotRect = value; }
        }

        public Color SlotColor
        {
            get { return slotColor; }
            set { slotColor = value; }
        }

        public void AddConnection(BocsSlotBase slot)
        {
            if (slot == this) return;
            if (Node == slot.Node) return;

            slot.InputSlot = this;
            foreach (BocsSlotBase s in OutputSlots)
            {
                //if (s._slotType ==	BocsSlotType.)
                if (s == slot) return;
            }
            OutputSlots.Add(slot);
        }

        public void RemoveConnection()
        {
            if (InputSlot == null) return;

            foreach (BocsSlotBase s in InputSlot.OutputSlots)
            {
                if (s == this)
                {
                    //Debug.Log("Before: " + _inputSlot._outputSlots.Count);
                    InputSlot.OutputSlots.Remove(this);
                    //Debug.Log("After: " + _inputSlot._outputSlots.Count);
                    InputSlot = null;
                    return;
                }
            }
            InputSlot = null;
        }

        public abstract void DrawSlotGUI();

        public void AddSocket()
        {
            //Rect last = GUILayoutUtility.GetLastRect();
            //if (_slotType == BocsSlotType.Input) _slotRect = new Rect(last.xMin + _node._nodeRect.xMin - 22, last.yMin + _node._nodeRect.yMin, 16, 16);
            //if (_slotType == BocsSlotType.Output) _slotRect = new Rect(last.xMax + _node._nodeRect.xMin + 7, last.yMin + _node._nodeRect.yMin, 16, 16);

            if (Event.current.type == EventType.Repaint && SlotType != BocsSlotType.Value)
            {
                if (Node.State == 1)
                {
                    if (InputSlot != null || OutputSlots.Count > 0)
                    {
                        Rect last = GUILayoutUtility.GetLastRect();
                        if (SlotType == BocsSlotType.Input) SlotRect = new Rect(last.xMin + Node.NodeRect.xMin - 22, last.yMin + Node.NodeRect.yMin, 16, 16);
                        if (SlotType == BocsSlotType.Output) SlotRect = new Rect(last.xMax + Node.NodeRect.xMin + 7, last.yMin + Node.NodeRect.yMin, 16, 16);

                        BocsCyclesNodeManager.Slots.Add(this);
                    }
                }
                else
                {
                    Rect last = GUILayoutUtility.GetLastRect();
                    if (SlotType == BocsSlotType.Input) SlotRect = new Rect(last.xMin + Node.NodeRect.xMin - 22, last.yMin + Node.NodeRect.yMin, 16, 16);
                    if (SlotType == BocsSlotType.Output) SlotRect = new Rect(last.xMax + Node.NodeRect.xMin + 7, last.yMin + Node.NodeRect.yMin, 16, 16);

                    BocsCyclesNodeManager.Slots.Add(this);
                }
            }
        }

        public virtual void DrawSlotSocket()
        {
            GUI.color = SlotColor;
            GUI.Box(SlotRect, string.Empty);
        }

        public virtual bool HasValue()
        {
            return false;
        }

        public virtual void SetString(string val)
        {
        }

        public virtual string GetString()
        {
            return string.Empty;
        }

        public virtual string GetXML()
        {
            return this.GetString();
        }

        public bool CanDrawSlot()
        {
            if (Node.State == 1)
            {
                if (InputSlot != null || OutputSlots.Count > 0)
                {
                    GUILayout.Label(SlotTitle);
                }
                return false;
            }

            return true;
        }
    }
}