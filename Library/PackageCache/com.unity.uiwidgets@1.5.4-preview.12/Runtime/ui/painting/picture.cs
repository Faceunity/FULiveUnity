﻿using System;
using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.Runtime.external;

namespace Unity.UIWidgets.ui {
    public class Picture {
        public Picture(List<DrawCmd> drawCmds,
            Rect paintBounds,
            bool isDynamic = false,
            BBoxHierarchy<IndexedRect> bbh = null,
            List<int> stateUpdatesIndices = null) {
            this.drawCmds = drawCmds;
            this.paintBounds = paintBounds;
            this._isDynamic = isDynamic;
            this.bbh = bbh;
            this.stateUpdatesIndices = stateUpdatesIndices;
        }

        public readonly List<DrawCmd> drawCmds;
        public readonly Rect paintBounds;
        public readonly BBoxHierarchy<IndexedRect> bbh;
        public readonly List<int> stateUpdatesIndices; 

        public bool isDynamic {
            get { return this._isDynamic; }
        }

        bool _isDynamic;
    }

    public class PictureRecorder {
        readonly List<DrawCmd> _drawCmds = new List<DrawCmd>();

        readonly List<CanvasState> _states = new List<CanvasState>();
        
        readonly BBoxHierarchy<IndexedRect> _bbh = new RTree<IndexedRect>();

        readonly List<int> _stateUpdateIndices = new List<int>();

        bool _isDynamic;

        public PictureRecorder() {
            this.reset();
        }

        CanvasState _getState() {
            D.assert(this._states.Count > 0);
            return this._states[this._states.Count - 1];
        }

        public Matrix3 getTotalMatrix() {
            return this._getState().xform;
        }

        public void reset() {
            this._drawCmds.Clear();
            this._isDynamic = false;
            this._states.Clear();
            this._states.Add(new CanvasState {
                xform = Matrix3.I(),
                scissor = null,
                saveLayer = false,
                layerOffset = null,
                paintBounds = Rect.zero,
            });
            this._bbh.Clear();
            this._stateUpdateIndices.Clear();
        }

        void restoreToCount(int count) {
            count = this._states.Count - count;
            while (count > 0) {
                this.addDrawCmd(new DrawRestore());
                count--;
            }
        }

        void restore() {
            var stateToRestore = this._getState();
            this._states.RemoveAt(this._states.Count - 1);
            var state = this._getState();

            if (!stateToRestore.saveLayer) {
                state.paintBounds = stateToRestore.paintBounds;
            }
            else {
                var paintBounds = stateToRestore.paintBounds.shift(stateToRestore.layerOffset);
                paintBounds = state.xform.mapRect(paintBounds);
                this._addPaintBounds(paintBounds);
            }
        }

        public Picture endRecording() {
            this.restoreToCount(1);
            
            if (this._states.Count > 1) {
                throw new Exception("unmatched save/restore commands");
            }

            var state = this._getState();
            return new Picture(
                new List<DrawCmd>(this._drawCmds),
                state.paintBounds,
                this._isDynamic,
                this._bbh,
                this._stateUpdateIndices);
        }

        public void addDrawCmd(DrawCmd drawCmd) {
            this._drawCmds.Add(drawCmd);

            switch (drawCmd) {
                case DrawSave _:
                    this._states.Add(this._getState().copy());
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                case DrawSaveLayer cmd: {
                    this._states.Add(new CanvasState {
                        xform = Matrix3.I(),
                        scissor = cmd.rect.shift(-cmd.rect.topLeft),
                        saveLayer = true,
                        layerOffset = cmd.rect.topLeft,
                        paintBounds = Rect.zero,
                    });
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawRestore _: {
                    //check for underflow
                    if (this._states.Count > 1) {
                        this.restore();
                    }
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawTranslate cmd: {
                    var state = this._getState();
                    state.xform = new Matrix3(state.xform);
                    state.xform.preTranslate(cmd.dx, cmd.dy);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawScale cmd: {
                    var state = this._getState();
                    state.xform = new Matrix3(state.xform);
                    state.xform.preScale(cmd.sx, (cmd.sy ?? cmd.sx));
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawRotate cmd: {
                    var state = this._getState();
                    state.xform = new Matrix3(state.xform);
                    if (cmd.offset == null) {
                        state.xform.preRotate(cmd.radians);
                    }
                    else {
                        state.xform.preRotate(cmd.radians,
                            cmd.offset.dx,
                            cmd.offset.dy);
                    }
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);

                    break;
                }

                case DrawSkew cmd: {
                    var state = this._getState();
                    state.xform = new Matrix3(state.xform);
                    state.xform.preSkew(cmd.sx, cmd.sy);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawConcat cmd: {
                    var state = this._getState();
                    state.xform = new Matrix3(state.xform);
                    state.xform.preConcat(cmd.matrix);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawResetMatrix _: {
                    var state = this._getState();
                    state.xform = Matrix3.I();
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawSetMatrix cmd: {
                    var state = this._getState();
                    state.xform = new Matrix3(cmd.matrix);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawClipRect cmd: {
                    var state = this._getState();

                    var rect = state.xform.mapRect(cmd.rect);
                    state.scissor = state.scissor == null ? rect : state.scissor.intersect(rect);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawClipRRect cmd: {
                    var state = this._getState();

                    var rect = state.xform.mapRect(cmd.rrect.outerRect);
                    state.scissor = state.scissor == null ? rect : state.scissor.intersect(rect);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawClipPath cmd: {
                    var state = this._getState();
                    var scale = XformUtils.getScale(state.xform);

                    var cache = cmd.path.flatten(
                        scale * Window.instance.devicePixelRatio
                    );
                    cache.computeFillMesh(0.0f, out _);
                    var rect = cache.fillMesh.transform(state.xform).bounds;
                    state.scissor = state.scissor == null ? rect : state.scissor.intersect(rect);
                    this._stateUpdateIndices.Add(this._drawCmds.Count - 1);
                    break;
                }

                case DrawPath cmd: {
                    var state = this._getState();
                    var scale = XformUtils.getScale(state.xform);
                    var path = cmd.path;
                    var paint = cmd.paint;
                    var devicePixelRatio = Window.instance.devicePixelRatio;

                    MeshMesh mesh;
                    if (paint.style == PaintingStyle.fill) {
                        var cache = path.flatten(scale * devicePixelRatio);
                        cache.computeFillMesh(0.0f, out _);
                        mesh = cache.fillMesh.transform(state.xform);
                    }
                    else {
                        float strokeWidth = (paint.strokeWidth * scale).clamp(0, 200.0f);
                        float fringeWidth = 1 / devicePixelRatio;

                        if (strokeWidth < fringeWidth) {
                            strokeWidth = fringeWidth;
                        }

                        var cache = path.flatten(scale * devicePixelRatio);
                        cache.computeStrokeMesh(
                            strokeWidth / scale * 0.5f,
                            0.0f,
                            paint.strokeCap,
                            paint.strokeJoin,
                            paint.strokeMiterLimit);
                        mesh = cache.strokeMesh.transform(state.xform);
                    }

                    if (paint.maskFilter != null && paint.maskFilter.sigma != 0) {
                        float sigma = scale * paint.maskFilter.sigma;
                        float sigma3 = 3 * sigma;
                        this._addPaintBounds(mesh.bounds.inflate(sigma3));
                        this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(mesh.bounds.inflate(sigma3 + 5)).Value,
                            this._drawCmds.Count - 1));
                    }
                    else {
                        this._addPaintBounds(mesh.bounds);
                        this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(mesh.bounds.inflate(5)).Value,
                            this._drawCmds.Count - 1));
                    }

                    break;
                }

                case DrawImage cmd: {
                    var state = this._getState();
                    var rect = Rect.fromLTWH(cmd.offset.dx, cmd.offset.dy,
                        cmd.image.width, cmd.image.height);
                    rect = state.xform.mapRect(rect);
                    this._addPaintBounds(rect);
                    this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(rect.inflate(5)).Value,
                        this._drawCmds.Count - 1));
                    if (cmd.image.isDynamic) {
                        this._isDynamic = true;
                    }

                    break;
                }

                case DrawImageRect cmd: {
                    var state = this._getState();
                    var rect = state.xform.mapRect(cmd.dst);
                    this._addPaintBounds(rect);
                    this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(rect.inflate(5)).Value,
                        this._drawCmds.Count - 1));
                    if (cmd.image.isDynamic) {
                        this._isDynamic = true;
                    }

                    break;
                }

                case DrawImageNine cmd: {
                    var state = this._getState();
                    var rect = state.xform.mapRect(cmd.dst);
                    this._addPaintBounds(rect);
                    this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(rect.inflate(5)).Value,
                        this._drawCmds.Count - 1));
                    if (cmd.image.isDynamic) {
                        this._isDynamic = true;
                    }

                    break;
                }

                case DrawPicture cmd: {
                    var state = this._getState();
                    var rect = state.xform.mapRect(cmd.picture.paintBounds);
                    this._addPaintBounds(rect);
                    this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(rect.inflate(5)).Value,
                        this._drawCmds.Count - 1));
                    if (cmd.picture.isDynamic) {
                        this._isDynamic = true;
                    }

                    break;
                }

                case DrawTextBlob cmd: {
                    var state = this._getState();
                    var scale = XformUtils.getScale(state.xform);
                    var rect = cmd.textBlob.Value.shiftedBoundsInText(cmd.offset.dx, cmd.offset.dy);
                    rect = state.xform.mapRect(rect);

                    var paint = cmd.paint;
                    if (paint.maskFilter != null && paint.maskFilter.sigma != 0) {
                        float sigma = scale * paint.maskFilter.sigma;
                        float sigma3 = 3 * sigma;
                        this._addPaintBounds(rect.inflate(sigma3));
                        this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(rect.inflate(sigma3 + 5)).Value,
                            this._drawCmds.Count - 1));
                    }
                    else {
                        this._addPaintBounds(rect);
                        this._bbh.Insert(new IndexedRect(uiRectHelper.fromRect(rect.inflate(5)).Value,
                            this._drawCmds.Count - 1));
                    }

                    break;
                }

                default:
                    throw new Exception("unknown drawCmd: " + drawCmd);
            }
        }

        void _addPaintBounds(Rect paintBounds) {
            var state = this._getState();
            if (state.scissor != null) {
                paintBounds = paintBounds.intersect(state.scissor);
            }

            if (paintBounds == null || paintBounds.isEmpty) {
                return;
            }

            if (state.paintBounds.isEmpty) {
                state.paintBounds = paintBounds;
            }
            else {
                state.paintBounds = state.paintBounds.expandToInclude(paintBounds);
            }
        }

        class CanvasState {
            public Matrix3 xform;
            public Rect scissor;
            public bool saveLayer;
            public Offset layerOffset;
            public Rect paintBounds;

            public CanvasState copy() {
                return new CanvasState {
                    xform = this.xform,
                    scissor = this.scissor,
                    saveLayer = false,
                    layerOffset = null,
                    paintBounds = this.paintBounds,
                };
            }
        }
    }
}