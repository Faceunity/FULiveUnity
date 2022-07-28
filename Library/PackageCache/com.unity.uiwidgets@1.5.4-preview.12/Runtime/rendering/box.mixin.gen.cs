using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIWidgets.gestures;
using Unity.UIWidgets.ui;

namespace Unity.UIWidgets.rendering {

 
    public abstract class
        RenderBoxContainerDefaultsMixinContainerRenderObjectMixinRenderBox<ChildType, ParentDataType>
        : ContainerRenderObjectMixinRenderBox<ChildType, ParentDataType>
        where ChildType : RenderBox
        where ParentDataType : ContainerParentDataMixinBoxParentData<ChildType> {
        
        public float? defaultComputeDistanceToFirstActualBaseline(TextBaseline baseline) {
            var child = this.firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                float? result = child.getDistanceToActualBaseline(baseline);
                if (result != null) {
                    return result.Value + childParentData.offset.dy;
                }

                child = childParentData.nextSibling;
            }

            return null;
        }

        public float? defaultComputeDistanceToHighestActualBaseline(TextBaseline baseline) {
            float? result = null;
            var child = this.firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                float? candidate = child.getDistanceToActualBaseline(baseline);
                if (candidate != null) {
                    candidate += childParentData.offset.dy;
                    if (result != null) {
                        result = Mathf.Min(result.Value, candidate.Value);
                    } else {
                        result = candidate;
                    }
                }

                child = childParentData.nextSibling;
            }

            return result;
        }

        public void defaultPaint(PaintingContext context, Offset offset) {
            var child = this.firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                context.paintChild(child, childParentData.offset + offset);
                child = childParentData.nextSibling;
            }
        }

       public bool defaultHitTestChildren(HitTestResult result, Offset position) {
            // the x, y parameters have the top left of the node's box as the origin
            ChildType child = this.lastChild;
            while (child != null)
            {
                ParentDataType childParentData = (ParentDataType) child.parentData;
                if (child.hitTest(result, position: position - childParentData.offset)) {
                    return true;
                }
                child = childParentData.previousSibling;
            }
            return false;
        }

        public List<ChildType> getChildrenAsList() {
            var result = new List<ChildType>();
            var child = this.firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                result.Add(child);
                child = childParentData.nextSibling;
            }

            return result;
        }
    }


}