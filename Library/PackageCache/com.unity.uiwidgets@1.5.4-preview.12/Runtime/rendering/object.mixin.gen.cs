// AUTO-GENERATED, DO NOT EDIT BY HAND

using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using UnityEngine;

namespace Unity.UIWidgets.rendering {

    public abstract class RenderObjectWithChildMixinRenderObject<ChildType> : RenderObject, RenderObjectWithChildMixin<ChildType>, RenderObjectWithChildMixin where ChildType : RenderObject {
        public virtual bool debugValidateChild(RenderObject child) {
            D.assert(() => {
                if (!(child is ChildType)) {
                    throw new UIWidgetsError(
                        "A " + this.GetType() + " expected a child of type " + typeof(ChildType) + " but received a " +
                        "child of type " + child.GetType() + ".\n" +
                        "RenderObjects expect specific types of children because they " +
                        "coordinate with their children during layout and paint. For " +
                        "example, a RenderSliver cannot be the child of a RenderBox because " +
                        "a RenderSliver does not understand the RenderBox layout protocol.\n" +
                        "\n" +
                        "The " + this.GetType() + " that expected a " + typeof(ChildType) + " child was created by:\n" +
                        "  " + this.debugCreator + "\n" +
                        "\n" +
                        "The " + child.GetType() + " that did not match the expected child type " +
                        "was created by:\n" +
                        "  " + child.debugCreator + "\n"
                    );
                }

                return true;
            });
            return true;
        }

        internal ChildType _child;

        public ChildType child {
            get { return this._child; }
            set {
                if (this._child != null) {
                    this.dropChild(this._child);
                }

                this._child = value;
                if (this._child != null) {
                    this.adoptChild(this._child);
                }
            }
        }

        RenderObject RenderObjectWithChildMixin.child {
            get { return this.child; }
            set { this.child = (ChildType) value; }
        }

        public override void attach(object owner) {
            base.attach(owner);
            if (this._child != null) {
                this._child.attach(owner);
            }
        }

        public override void detach() {
            base.detach();
            if (this._child != null) {
                this._child.detach();
            }
        }

        public override void redepthChildren() {
            if (this._child != null) {
                this.redepthChild(this._child);
            }
        }

        public override void visitChildren(RenderObjectVisitor visitor) {
            if (this._child != null) {
                visitor(this._child);
            }
        }

        public override List<DiagnosticsNode> debugDescribeChildren() {
            return this.child != null
                ? new List<DiagnosticsNode>{this.child.toDiagnosticsNode(name: "child")}
                : new List<DiagnosticsNode>();
        }
    }


    public abstract class RenderObjectWithChildMixinRenderBox<ChildType> : RenderBox, RenderObjectWithChildMixin<ChildType>, RenderObjectWithChildMixin where ChildType : RenderObject {
        public virtual bool debugValidateChild(RenderObject child) {
            D.assert(() => {
                if (!(child is ChildType)) {
                    throw new UIWidgetsError(
                        "A " + this.GetType() + " expected a child of type " + typeof(ChildType) + " but received a " +
                        "child of type " + child.GetType() + ".\n" +
                        "RenderObjects expect specific types of children because they " +
                        "coordinate with their children during layout and paint. For " +
                        "example, a RenderSliver cannot be the child of a RenderBox because " +
                        "a RenderSliver does not understand the RenderBox layout protocol.\n" +
                        "\n" +
                        "The " + this.GetType() + " that expected a " + typeof(ChildType) + " child was created by:\n" +
                        "  " + this.debugCreator + "\n" +
                        "\n" +
                        "The " + child.GetType() + " that did not match the expected child type " +
                        "was created by:\n" +
                        "  " + child.debugCreator + "\n"
                    );
                }

                return true;
            });
            return true;
        }

        internal ChildType _child;

        public ChildType child {
            get { return this._child; }
            set {
                if (this._child != null) {
                    this.dropChild(this._child);
                }

                this._child = value;
                if (this._child != null) {
                    this.adoptChild(this._child);
                }
            }
        }

        RenderObject RenderObjectWithChildMixin.child {
            get { return this.child; }
            set { this.child = (ChildType) value; }
        }

        public override void attach(object owner) {
            base.attach(owner);
            if (this._child != null) {
                this._child.attach(owner);
            }
        }

        public override void detach() {
            base.detach();
            if (this._child != null) {
                this._child.detach();
            }
        }

        public override void redepthChildren() {
            if (this._child != null) {
                this.redepthChild(this._child);
            }
        }

        public override void visitChildren(RenderObjectVisitor visitor) {
            if (this._child != null) {
                visitor(this._child);
            }
        }

        public override List<DiagnosticsNode> debugDescribeChildren() {
            return this.child != null
                ? new List<DiagnosticsNode>{this.child.toDiagnosticsNode(name: "child")}
                : new List<DiagnosticsNode>();
        }
    }


    public abstract class RenderObjectWithChildMixinRenderSliver<ChildType> : RenderSliver, RenderObjectWithChildMixin<ChildType>, RenderObjectWithChildMixin where ChildType : RenderObject {
        public bool debugValidateChild(RenderObject child) {
            D.assert(() => {
                if (!(child is ChildType)) {
                    throw new UIWidgetsError(
                        "A " + this.GetType() + " expected a child of type " + typeof(ChildType) + " but received a " +
                        "child of type " + child.GetType() + ".\n" +
                        "RenderObjects expect specific types of children because they " +
                        "coordinate with their children during layout and paint. For " +
                        "example, a RenderSliver cannot be the child of a RenderBox because " +
                        "a RenderSliver does not understand the RenderBox layout protocol.\n" +
                        "\n" +
                        "The " + this.GetType() + " that expected a " + typeof(ChildType) + " child was created by:\n" +
                        "  " + this.debugCreator + "\n" +
                        "\n" +
                        "The " + child.GetType() + " that did not match the expected child type " +
                        "was created by:\n" +
                        "  " + child.debugCreator + "\n"
                    );
                }

                return true;
            });
            return true;
        }

        internal ChildType _child;

        public ChildType child {
            get { return this._child; }
            set {
                if (this._child != null) {
                    this.dropChild(this._child);
                }

                this._child = value;
                if (this._child != null) {
                    this.adoptChild(this._child);
                }
            }
        }

        RenderObject RenderObjectWithChildMixin.child {
            get { return this.child; }
            set { this.child = (ChildType) value; }
        }

        public override void attach(object owner) {
            base.attach(owner);
            if (this._child != null) {
                this._child.attach(owner);
            }
        }

        public override void detach() {
            base.detach();
            if (this._child != null) {
                this._child.detach();
            }
        }

        public override void redepthChildren() {
            if (this._child != null) {
                this.redepthChild(this._child);
            }
        }

        public override void visitChildren(RenderObjectVisitor visitor) {
            if (this._child != null) {
                visitor(this._child);
            }
        }

        public override List<DiagnosticsNode> debugDescribeChildren() {
            return this.child != null
                ? new List<DiagnosticsNode>{this.child.toDiagnosticsNode(name: "child")}
                : new List<DiagnosticsNode>();
        }
    }



    public abstract class ContainerParentDataMixinParentData<ChildType> : ParentData, ContainerParentDataMixin<ChildType> where ChildType : RenderObject {
        public ChildType previousSibling { get; set; }

        public ChildType nextSibling { get; set; }

        public override void detach() {
            base.detach();

            D.assert(this.previousSibling == null);
            D.assert(this.nextSibling == null);

            // if (this.previousSibling != null) {
            //     var previousSiblingParentData = (ContainerParentDataMixin<ChildType>) this.previousSibling.parentData;
            //     previousSiblingParentData.nextSibling = this.nextSibling;
            // }

            // if (this.nextSibling != null) {
            //     var nextSiblingParentData = (ContainerParentDataMixin<ChildType>) this.nextSibling.parentData;
            //     nextSiblingParentData.previousSibling = this.previousSibling;
            // }

            // this.previousSibling = null;
            // this.nextSibling = null;
        }
    }



    public abstract class ContainerParentDataMixinBoxParentData<ChildType> : BoxParentData, ContainerParentDataMixin<ChildType> where ChildType : RenderObject {
        public ChildType previousSibling { get; set; }

        public ChildType nextSibling { get; set; }

        public override void detach() {
            base.detach();

            D.assert(this.previousSibling == null);
            D.assert(this.nextSibling == null);

            // if (this.previousSibling != null) {
            //     var previousSiblingParentData = (ContainerParentDataMixin<ChildType>) this.previousSibling.parentData;
            //     previousSiblingParentData.nextSibling = this.nextSibling;
            // }

            // if (this.nextSibling != null) {
            //     var nextSiblingParentData = (ContainerParentDataMixin<ChildType>) this.nextSibling.parentData;
            //     nextSiblingParentData.previousSibling = this.previousSibling;
            // }

            // this.previousSibling = null;
            // this.nextSibling = null;
        }
    }



    public abstract class ContainerParentDataMixinSliverPhysicalParentData<ChildType> : SliverPhysicalParentData, ContainerParentDataMixin<ChildType> where ChildType : RenderObject {
        public ChildType previousSibling { get; set; }

        public ChildType nextSibling { get; set; }

        public override void detach() {
            base.detach();

            D.assert(this.previousSibling == null);
            D.assert(this.nextSibling == null);

            // if (this.previousSibling != null) {
            //     var previousSiblingParentData = (ContainerParentDataMixin<ChildType>) this.previousSibling.parentData;
            //     previousSiblingParentData.nextSibling = this.nextSibling;
            // }

            // if (this.nextSibling != null) {
            //     var nextSiblingParentData = (ContainerParentDataMixin<ChildType>) this.nextSibling.parentData;
            //     nextSiblingParentData.previousSibling = this.previousSibling;
            // }

            // this.previousSibling = null;
            // this.nextSibling = null;
        }
    }



    public abstract class ContainerParentDataMixinSliverLogicalParentData<ChildType> : SliverLogicalParentData, ContainerParentDataMixin<ChildType> where ChildType : RenderObject {
        public ChildType previousSibling { get; set; }

        public ChildType nextSibling { get; set; }

        public override void detach() {
            base.detach();

            D.assert(this.previousSibling == null);
            D.assert(this.nextSibling == null);

            // if (this.previousSibling != null) {
            //     var previousSiblingParentData = (ContainerParentDataMixin<ChildType>) this.previousSibling.parentData;
            //     previousSiblingParentData.nextSibling = this.nextSibling;
            // }

            // if (this.nextSibling != null) {
            //     var nextSiblingParentData = (ContainerParentDataMixin<ChildType>) this.nextSibling.parentData;
            //     nextSiblingParentData.previousSibling = this.previousSibling;
            // }

            // this.previousSibling = null;
            // this.nextSibling = null;
        }
    }





    public abstract class ContainerRenderObjectMixinRenderBox<ChildType, ParentDataType> : RenderBox, ContainerRenderObjectMixin
        where ChildType : RenderObject
        where ParentDataType : ParentData, ContainerParentDataMixin<ChildType> {

        bool _debugUltimatePreviousSiblingOf(ChildType child, ChildType equals = null) {
            ParentDataType childParentData = (ParentDataType) child.parentData;
            while (childParentData.previousSibling != null) {
                D.assert(childParentData.previousSibling != child);
                child = childParentData.previousSibling;
                childParentData = (ParentDataType) child.parentData;
            }

            return child == equals;
        }

        bool _debugUltimateNextSiblingOf(ChildType child, ChildType equals = null) {
            ParentDataType childParentData = (ParentDataType) child.parentData;
            while (childParentData.nextSibling != null) {
                D.assert(childParentData.nextSibling != child);
                child = childParentData.nextSibling;
                childParentData = (ParentDataType) child.parentData;
            }

            return child == equals;
        }

        int _childCount = 0;

        public int childCount {
            get { return this._childCount; }
        }

        public bool debugValidateChild(RenderObject child) {
            D.assert(() => {
                if (!(child is ChildType)) {
                    throw new UIWidgetsError(
                        "A " + this.GetType() + " expected a child of type " + typeof(ChildType) + " but received a " +
                        "child of type " + child.GetType() + ".\n" +
                        "RenderObjects expect specific types of children because they " +
                        "coordinate with their children during layout and paint. For " +
                        "example, a RenderSliver cannot be the child of a RenderBox because " +
                        "a RenderSliver does not understand the RenderBox layout protocol.\n" +
                        "\n" +
                        "The " + this.GetType() + " that expected a " + typeof(ChildType) + " child was created by:\n" +
                        "  " + this.debugCreator + "\n" +
                        "\n" +
                        "The " + child.GetType() + " that did not match the expected child type " +
                        "was created by:\n" +
                        "  " + child.debugCreator + "\n"
                    );
                }

                return true;
            });
            return true;
        }

        ChildType _firstChild;

        ChildType _lastChild;

        void _insertIntoChildList(ChildType child, ChildType after = null) {
            var childParentData = (ParentDataType) child.parentData;
            D.assert(childParentData.nextSibling == null);
            D.assert(childParentData.previousSibling == null);

            this._childCount++;
            D.assert(this._childCount > 0);

            if (after == null) {
                childParentData.nextSibling = this._firstChild;
                if (this._firstChild != null) {
                    var firstChildParentData = (ParentDataType) this._firstChild.parentData;
                    firstChildParentData.previousSibling = child;
                }

                this._firstChild = child;
                this._lastChild = this._lastChild ?? child;
            } else {
                D.assert(this._firstChild != null);
                D.assert(this._lastChild != null);
                D.assert(this._debugUltimatePreviousSiblingOf(after, equals: this._firstChild));
                D.assert(this._debugUltimateNextSiblingOf(after, equals: this._lastChild));
                var afterParentData = (ParentDataType) after.parentData;
                if (afterParentData.nextSibling == null) {
                    D.assert(after == this._lastChild);
                    childParentData.previousSibling = after;
                    afterParentData.nextSibling = child;
                    this._lastChild = child;
                } else {
                    childParentData.nextSibling = afterParentData.nextSibling;
                    childParentData.previousSibling = after;
                    var childPreviousSiblingParentData = (ParentDataType) childParentData.previousSibling.parentData;
                    var childNextSiblingParentData = (ParentDataType) childParentData.nextSibling.parentData;
                    childPreviousSiblingParentData.nextSibling = child;
                    childNextSiblingParentData.previousSibling = child;
                    D.assert(afterParentData.nextSibling == child);
                }
            }
        }

        public virtual void insert(ChildType child, ChildType after = null) {
            D.assert(child != this, () => "A RenderObject cannot be inserted into itself.");
            D.assert(after != this,
                () => "A RenderObject cannot simultaneously be both the parent and the sibling of another RenderObject.");
            D.assert(child != after, () => "A RenderObject cannot be inserted after itself.");
            D.assert(child != this._firstChild);
            D.assert(child != this._lastChild);

            this.adoptChild(child);
            this._insertIntoChildList(child, after);
        }

        public virtual void add(ChildType child) {
            this.insert(child, this._lastChild);
        }

        public virtual void addAll(List<ChildType> children) {
            if (children != null) {
                children.ForEach(this.add);
            }
        }

        public void _removeFromChildList(ChildType child) {
            var childParentData = (ParentDataType) child.parentData;
            D.assert(this._debugUltimatePreviousSiblingOf(child, equals: this._firstChild));
            D.assert(this._debugUltimateNextSiblingOf(child, equals: this._lastChild));
            D.assert(this._childCount >= 0);

            if (childParentData.previousSibling == null) {
                D.assert(this._firstChild == child);
                this._firstChild = childParentData.nextSibling;
            } else {
                var childPreviousSiblingParentData = (ParentDataType) childParentData.previousSibling.parentData;
                childPreviousSiblingParentData.nextSibling = childParentData.nextSibling;
            }

            if (childParentData.nextSibling == null) {
                D.assert(this._lastChild == child);
                this._lastChild = childParentData.previousSibling;
            } else {
                var childNextSiblingParentData = (ParentDataType) childParentData.nextSibling.parentData;
                childNextSiblingParentData.previousSibling = childParentData.previousSibling;
            }

            childParentData.previousSibling = null;
            childParentData.nextSibling = null;
            this._childCount--;
        }

        public virtual void remove(ChildType child) {
            this._removeFromChildList(child);
            this.dropChild(child);
        }

        public virtual void removeAll() {
            ChildType child = this._firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                var next = childParentData.nextSibling;
                childParentData.previousSibling = null;
                childParentData.nextSibling = null;
                this.dropChild(child);
                child = next;
            }

            this._firstChild = null;
            this._lastChild = null;
            this._childCount = 0;
        }

        public void move(ChildType child, ChildType after = null) {
            D.assert(child != this);
            D.assert(after != this);
            D.assert(child != after);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            if (childParentData.previousSibling == after) {
                return;
            }

            this._removeFromChildList(child);
            this._insertIntoChildList(child, after);
            this.markNeedsLayout();
        }

        public override void attach(object owner) {
            base.attach(owner);
            ChildType child = this._firstChild;
            while (child != null) {
                child.attach(owner);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void detach() {
            base.detach();
            ChildType child = this._firstChild;
            while (child != null) {
                child.detach();
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void redepthChildren() {
            ChildType child = this._firstChild;
            while (child != null) {
                this.redepthChild(child);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void visitChildren(RenderObjectVisitor visitor) {
            ChildType child = this._firstChild;
            while (child != null) {
                visitor(child);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public ChildType firstChild {
            get { return this._firstChild; }
        }

        public ChildType lastChild {
            get { return this._lastChild; }
        }

        public ChildType childBefore(ChildType child) {
            D.assert(child != null);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            return childParentData.previousSibling;
        }

        public ChildType childAfter(ChildType child) {
            D.assert(child != null);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            return childParentData.nextSibling;
        }

        public override List<DiagnosticsNode> debugDescribeChildren() {
            var children = new List<DiagnosticsNode>();
            if (this.firstChild != null) {
                ChildType child = this.firstChild;
                int count = 1;
                while (true) {
                    children.Add(child.toDiagnosticsNode(name: "child " + count));
                    if (child == this.lastChild) {
                        break;
                    }

                    count += 1;
                    var childParentData = (ParentDataType) child.parentData;
                    child = childParentData.nextSibling;
                }
            }

            return children;
        }

        void ContainerRenderObjectMixin.insert(RenderObject child, RenderObject after) {
            this.insert((ChildType) child, (ChildType) after);
        }

        void ContainerRenderObjectMixin.remove(RenderObject child) {
            this.remove((ChildType) child);
        }

        void ContainerRenderObjectMixin.move(RenderObject child, RenderObject after) {
            this.move((ChildType) child, (ChildType) after);
        }

        RenderObject ContainerRenderObjectMixin.firstChild {
            get { return this.firstChild; }
        }

        RenderObject ContainerRenderObjectMixin.lastChild {
            get { return this.lastChild; }
        }

        RenderObject ContainerRenderObjectMixin.childBefore(RenderObject child) {
            return this.childBefore((ChildType) child);
        }

        RenderObject ContainerRenderObjectMixin.childAfter(RenderObject child) {
            return this.childAfter((ChildType) child);
        }
    }


    public abstract class ContainerRenderObjectMixinRenderSliver<ChildType, ParentDataType> : RenderSliver, ContainerRenderObjectMixin
        where ChildType : RenderObject
        where ParentDataType : ParentData, ContainerParentDataMixin<ChildType> {

        bool _debugUltimatePreviousSiblingOf(ChildType child, ChildType equals = null) {
            ParentDataType childParentData = (ParentDataType) child.parentData;
            while (childParentData.previousSibling != null) {
                D.assert(childParentData.previousSibling != child);
                child = childParentData.previousSibling;
                childParentData = (ParentDataType) child.parentData;
            }

            return child == equals;
        }

        bool _debugUltimateNextSiblingOf(ChildType child, ChildType equals = null) {
            ParentDataType childParentData = (ParentDataType) child.parentData;
            while (childParentData.nextSibling != null) {
                D.assert(childParentData.nextSibling != child);
                child = childParentData.nextSibling;
                childParentData = (ParentDataType) child.parentData;
            }

            return child == equals;
        }

        int _childCount = 0;

        public int childCount {
            get { return this._childCount; }
        }

        public bool debugValidateChild(RenderObject child) {
            D.assert(() => {
                if (!(child is ChildType)) {
                    throw new UIWidgetsError(
                        "A " + this.GetType() + " expected a child of type " + typeof(ChildType) + " but received a " +
                        "child of type " + child.GetType() + ".\n" +
                        "RenderObjects expect specific types of children because they " +
                        "coordinate with their children during layout and paint. For " +
                        "example, a RenderSliver cannot be the child of a RenderBox because " +
                        "a RenderSliver does not understand the RenderBox layout protocol.\n" +
                        "\n" +
                        "The " + this.GetType() + " that expected a " + typeof(ChildType) + " child was created by:\n" +
                        "  " + this.debugCreator + "\n" +
                        "\n" +
                        "The " + child.GetType() + " that did not match the expected child type " +
                        "was created by:\n" +
                        "  " + child.debugCreator + "\n"
                    );
                }

                return true;
            });
            return true;
        }

        ChildType _firstChild;

        ChildType _lastChild;

        void _insertIntoChildList(ChildType child, ChildType after = null) {
            var childParentData = (ParentDataType) child.parentData;
            D.assert(childParentData.nextSibling == null);
            D.assert(childParentData.previousSibling == null);

            this._childCount++;
            D.assert(this._childCount > 0);

            if (after == null) {
                childParentData.nextSibling = this._firstChild;
                if (this._firstChild != null) {
                    var firstChildParentData = (ParentDataType) this._firstChild.parentData;
                    firstChildParentData.previousSibling = child;
                }

                this._firstChild = child;
                this._lastChild = this._lastChild ?? child;
            } else {
                D.assert(this._firstChild != null);
                D.assert(this._lastChild != null);
                D.assert(this._debugUltimatePreviousSiblingOf(after, equals: this._firstChild));
                D.assert(this._debugUltimateNextSiblingOf(after, equals: this._lastChild));
                var afterParentData = (ParentDataType) after.parentData;
                if (afterParentData.nextSibling == null) {
                    D.assert(after == this._lastChild);
                    childParentData.previousSibling = after;
                    afterParentData.nextSibling = child;
                    this._lastChild = child;
                } else {
                    childParentData.nextSibling = afterParentData.nextSibling;
                    childParentData.previousSibling = after;
                    var childPreviousSiblingParentData = (ParentDataType) childParentData.previousSibling.parentData;
                    var childNextSiblingParentData = (ParentDataType) childParentData.nextSibling.parentData;
                    childPreviousSiblingParentData.nextSibling = child;
                    childNextSiblingParentData.previousSibling = child;
                    D.assert(afterParentData.nextSibling == child);
                }
            }
        }

        public virtual void insert(ChildType child, ChildType after = null) {
            D.assert(child != this, () => "A RenderObject cannot be inserted into itself.");
            D.assert(after != this,
                () => "A RenderObject cannot simultaneously be both the parent and the sibling of another RenderObject.");
            D.assert(child != after, () => "A RenderObject cannot be inserted after itself.");
            D.assert(child != this._firstChild);
            D.assert(child != this._lastChild);

            this.adoptChild(child);
            this._insertIntoChildList(child, after);
        }

        public virtual void add(ChildType child) {
            this.insert(child, this._lastChild);
        }

        public virtual void addAll(List<ChildType> children) {
            if (children != null) {
                children.ForEach(this.add);
            }
        }

        public void _removeFromChildList(ChildType child) {
            var childParentData = (ParentDataType) child.parentData;
            D.assert(this._debugUltimatePreviousSiblingOf(child, equals: this._firstChild));
            D.assert(this._debugUltimateNextSiblingOf(child, equals: this._lastChild));
            D.assert(this._childCount >= 0);

            if (childParentData.previousSibling == null) {
                D.assert(this._firstChild == child);
                this._firstChild = childParentData.nextSibling;
            } else {
                var childPreviousSiblingParentData = (ParentDataType) childParentData.previousSibling.parentData;
                childPreviousSiblingParentData.nextSibling = childParentData.nextSibling;
            }

            if (childParentData.nextSibling == null) {
                D.assert(this._lastChild == child);
                this._lastChild = childParentData.previousSibling;
            } else {
                var childNextSiblingParentData = (ParentDataType) childParentData.nextSibling.parentData;
                childNextSiblingParentData.previousSibling = childParentData.previousSibling;
            }

            childParentData.previousSibling = null;
            childParentData.nextSibling = null;
            this._childCount--;
        }

        public virtual void remove(ChildType child) {
            this._removeFromChildList(child);
            this.dropChild(child);
        }

        public virtual void removeAll() {
            ChildType child = this._firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                var next = childParentData.nextSibling;
                childParentData.previousSibling = null;
                childParentData.nextSibling = null;
                this.dropChild(child);
                child = next;
            }

            this._firstChild = null;
            this._lastChild = null;
            this._childCount = 0;
        }

        public void move(ChildType child, ChildType after = null) {
            D.assert(child != this);
            D.assert(after != this);
            D.assert(child != after);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            if (childParentData.previousSibling == after) {
                return;
            }

            this._removeFromChildList(child);
            this._insertIntoChildList(child, after);
            this.markNeedsLayout();
        }

        public override void attach(object owner) {
            base.attach(owner);
            ChildType child = this._firstChild;
            while (child != null) {
                child.attach(owner);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void detach() {
            base.detach();
            ChildType child = this._firstChild;
            while (child != null) {
                child.detach();
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void redepthChildren() {
            ChildType child = this._firstChild;
            while (child != null) {
                this.redepthChild(child);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void visitChildren(RenderObjectVisitor visitor) {
            ChildType child = this._firstChild;
            while (child != null) {
                visitor(child);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public ChildType firstChild {
            get { return this._firstChild; }
        }

        public ChildType lastChild {
            get { return this._lastChild; }
        }

        public ChildType childBefore(ChildType child) {
            D.assert(child != null);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            return childParentData.previousSibling;
        }

        public ChildType childAfter(ChildType child) {
            D.assert(child != null);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            return childParentData.nextSibling;
        }

        public override List<DiagnosticsNode> debugDescribeChildren() {
            var children = new List<DiagnosticsNode>();
            if (this.firstChild != null) {
                ChildType child = this.firstChild;
                int count = 1;
                while (true) {
                    children.Add(child.toDiagnosticsNode(name: "child " + count));
                    if (child == this.lastChild) {
                        break;
                    }

                    count += 1;
                    var childParentData = (ParentDataType) child.parentData;
                    child = childParentData.nextSibling;
                }
            }

            return children;
        }

        void ContainerRenderObjectMixin.insert(RenderObject child, RenderObject after) {
            this.insert((ChildType) child, (ChildType) after);
        }

        void ContainerRenderObjectMixin.remove(RenderObject child) {
            this.remove((ChildType) child);
        }

        void ContainerRenderObjectMixin.move(RenderObject child, RenderObject after) {
            this.move((ChildType) child, (ChildType) after);
        }

        RenderObject ContainerRenderObjectMixin.firstChild {
            get { return this.firstChild; }
        }

        RenderObject ContainerRenderObjectMixin.lastChild {
            get { return this.lastChild; }
        }

        RenderObject ContainerRenderObjectMixin.childBefore(RenderObject child) {
            return this.childBefore((ChildType) child);
        }

        RenderObject ContainerRenderObjectMixin.childAfter(RenderObject child) {
            return this.childAfter((ChildType) child);
        }
    }


    public abstract class ContainerRenderObjectMixinRenderProxyBoxMixinRenderObjectWithChildMixinRenderBoxRenderStack<ChildType, ParentDataType> : RenderProxyBoxMixinRenderObjectWithChildMixinRenderBoxRenderStack, ContainerRenderObjectMixin
        where ChildType : RenderObject
        where ParentDataType : ParentData, ContainerParentDataMixin<ChildType> {

        bool _debugUltimatePreviousSiblingOf(ChildType child, ChildType equals = null) {
            ParentDataType childParentData = (ParentDataType) child.parentData;
            while (childParentData.previousSibling != null) {
                D.assert(childParentData.previousSibling != child);
                child = childParentData.previousSibling;
                childParentData = (ParentDataType) child.parentData;
            }

            return child == equals;
        }

        bool _debugUltimateNextSiblingOf(ChildType child, ChildType equals = null) {
            ParentDataType childParentData = (ParentDataType) child.parentData;
            while (childParentData.nextSibling != null) {
                D.assert(childParentData.nextSibling != child);
                child = childParentData.nextSibling;
                childParentData = (ParentDataType) child.parentData;
            }

            return child == equals;
        }

        int _childCount = 0;

        public int childCount {
            get { return this._childCount; }
        }

        public override bool debugValidateChild(RenderObject child) {
            D.assert(() => {
                if (!(child is ChildType)) {
                    throw new UIWidgetsError(
                        "A " + this.GetType() + " expected a child of type " + typeof(ChildType) + " but received a " +
                        "child of type " + child.GetType() + ".\n" +
                        "RenderObjects expect specific types of children because they " +
                        "coordinate with their children during layout and paint. For " +
                        "example, a RenderSliver cannot be the child of a RenderBox because " +
                        "a RenderSliver does not understand the RenderBox layout protocol.\n" +
                        "\n" +
                        "The " + this.GetType() + " that expected a " + typeof(ChildType) + " child was created by:\n" +
                        "  " + this.debugCreator + "\n" +
                        "\n" +
                        "The " + child.GetType() + " that did not match the expected child type " +
                        "was created by:\n" +
                        "  " + child.debugCreator + "\n"
                    );
                }

                return true;
            });
            return true;
        }

        ChildType _firstChild;

        ChildType _lastChild;

        void _insertIntoChildList(ChildType child, ChildType after = null) {
            var childParentData = (ParentDataType) child.parentData;
            D.assert(childParentData.nextSibling == null);
            D.assert(childParentData.previousSibling == null);

            this._childCount++;
            D.assert(this._childCount > 0);

            if (after == null) {
                childParentData.nextSibling = this._firstChild;
                if (this._firstChild != null) {
                    var firstChildParentData = (ParentDataType) this._firstChild.parentData;
                    firstChildParentData.previousSibling = child;
                }

                this._firstChild = child;
                this._lastChild = this._lastChild ?? child;
            } else {
                D.assert(this._firstChild != null);
                D.assert(this._lastChild != null);
                D.assert(this._debugUltimatePreviousSiblingOf(after, equals: this._firstChild));
                D.assert(this._debugUltimateNextSiblingOf(after, equals: this._lastChild));
                var afterParentData = (ParentDataType) after.parentData;
                if (afterParentData.nextSibling == null) {
                    D.assert(after == this._lastChild);
                    childParentData.previousSibling = after;
                    afterParentData.nextSibling = child;
                    this._lastChild = child;
                } else {
                    childParentData.nextSibling = afterParentData.nextSibling;
                    childParentData.previousSibling = after;
                    var childPreviousSiblingParentData = (ParentDataType) childParentData.previousSibling.parentData;
                    var childNextSiblingParentData = (ParentDataType) childParentData.nextSibling.parentData;
                    childPreviousSiblingParentData.nextSibling = child;
                    childNextSiblingParentData.previousSibling = child;
                    D.assert(afterParentData.nextSibling == child);
                }
            }
        }

        public virtual void insert(ChildType child, ChildType after = null) {
            D.assert(child != this, () => "A RenderObject cannot be inserted into itself.");
            D.assert(after != this,
                () => "A RenderObject cannot simultaneously be both the parent and the sibling of another RenderObject.");
            D.assert(child != after, () => "A RenderObject cannot be inserted after itself.");
            D.assert(child != this._firstChild);
            D.assert(child != this._lastChild);

            this.adoptChild(child);
            this._insertIntoChildList(child, after);
        }

        public virtual void add(ChildType child) {
            this.insert(child, this._lastChild);
        }

        public virtual void addAll(List<ChildType> children) {
            if (children != null) {
                children.ForEach(this.add);
            }
        }

        public void _removeFromChildList(ChildType child) {
            var childParentData = (ParentDataType) child.parentData;
            D.assert(this._debugUltimatePreviousSiblingOf(child, equals: this._firstChild));
            D.assert(this._debugUltimateNextSiblingOf(child, equals: this._lastChild));
            D.assert(this._childCount >= 0);

            if (childParentData.previousSibling == null) {
                D.assert(this._firstChild == child);
                this._firstChild = childParentData.nextSibling;
            } else {
                var childPreviousSiblingParentData = (ParentDataType) childParentData.previousSibling.parentData;
                childPreviousSiblingParentData.nextSibling = childParentData.nextSibling;
            }

            if (childParentData.nextSibling == null) {
                D.assert(this._lastChild == child);
                this._lastChild = childParentData.previousSibling;
            } else {
                var childNextSiblingParentData = (ParentDataType) childParentData.nextSibling.parentData;
                childNextSiblingParentData.previousSibling = childParentData.previousSibling;
            }

            childParentData.previousSibling = null;
            childParentData.nextSibling = null;
            this._childCount--;
        }

        public virtual void remove(ChildType child) {
            this._removeFromChildList(child);
            this.dropChild(child);
        }

        public virtual void removeAll() {
            ChildType child = this._firstChild;
            while (child != null) {
                var childParentData = (ParentDataType) child.parentData;
                var next = childParentData.nextSibling;
                childParentData.previousSibling = null;
                childParentData.nextSibling = null;
                this.dropChild(child);
                child = next;
            }

            this._firstChild = null;
            this._lastChild = null;
            this._childCount = 0;
        }

        public void move(ChildType child, ChildType after = null) {
            D.assert(child != this);
            D.assert(after != this);
            D.assert(child != after);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            if (childParentData.previousSibling == after) {
                return;
            }

            this._removeFromChildList(child);
            this._insertIntoChildList(child, after);
            this.markNeedsLayout();
        }

        public override void attach(object owner) {
            base.attach(owner);
            ChildType child = this._firstChild;
            while (child != null) {
                child.attach(owner);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void detach() {
            base.detach();
            ChildType child = this._firstChild;
            while (child != null) {
                child.detach();
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void redepthChildren() {
            ChildType child = this._firstChild;
            while (child != null) {
                this.redepthChild(child);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public override void visitChildren(RenderObjectVisitor visitor) {
            ChildType child = this._firstChild;
            while (child != null) {
                visitor(child);
                var childParentData = (ParentDataType) child.parentData;
                child = childParentData.nextSibling;
            }
        }

        public ChildType firstChild {
            get { return this._firstChild; }
        }

        public ChildType lastChild {
            get { return this._lastChild; }
        }

        public ChildType childBefore(ChildType child) {
            D.assert(child != null);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            return childParentData.previousSibling;
        }

        public ChildType childAfter(ChildType child) {
            D.assert(child != null);
            D.assert(child.parent == this);

            var childParentData = (ParentDataType) child.parentData;
            return childParentData.nextSibling;
        }

        public override List<DiagnosticsNode> debugDescribeChildren() {
            var children = new List<DiagnosticsNode>();
            if (this.firstChild != null) {
                ChildType child = this.firstChild;
                int count = 1;
                while (true) {
                    children.Add(child.toDiagnosticsNode(name: "child " + count));
                    if (child == this.lastChild) {
                        break;
                    }

                    count += 1;
                    var childParentData = (ParentDataType) child.parentData;
                    child = childParentData.nextSibling;
                }
            }

            return children;
        }

        void ContainerRenderObjectMixin.insert(RenderObject child, RenderObject after) {
            this.insert((ChildType) child, (ChildType) after);
        }

        void ContainerRenderObjectMixin.remove(RenderObject child) {
            this.remove((ChildType) child);
        }

        void ContainerRenderObjectMixin.move(RenderObject child, RenderObject after) {
            this.move((ChildType) child, (ChildType) after);
        }

        RenderObject ContainerRenderObjectMixin.firstChild {
            get { return this.firstChild; }
        }

        RenderObject ContainerRenderObjectMixin.lastChild {
            get { return this.lastChild; }
        }

        RenderObject ContainerRenderObjectMixin.childBefore(RenderObject child) {
            return this.childBefore((ChildType) child);
        }

        RenderObject ContainerRenderObjectMixin.childAfter(RenderObject child) {
            return this.childAfter((ChildType) child);
        }
    }


}